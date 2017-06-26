# UserOnlineCounterMVCSignalR
Display user online with SignalR

#1: Install SignalR to your project
Run nuget command
Install-Package Microsoft.AspNet.SignalR -Version 2.2.2

#2: Create folder Hubs and a class CounterHub.cs inside
using Microsoft.AspNet.SignalR;

namespace thuyvk.com.Hubs
{
    public class CounterHub : Hub
    {
        private static int counter = 0;

        public override System.Threading.Tasks.Task OnConnected()
        {
            counter = counter + 1;
            Clients.All.UpdateCount(counter);
            return base.OnConnected();
        }

        public override System.Threading.Tasks.Task OnReconnected()
        {
            counter = counter + 1;
            Clients.All.UpdateCount(counter);
            return base.OnReconnected();
        }

        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            counter = counter - 1;
            Clients.All.UpdateCount(counter);
            return base.OnDisconnected(stopCalled);
        }


    }
}

#3: Register SignalR javascript in _Layout.cshtml page
<script src="@Url.Content("~/Scripts/jquery.signalR-2.2.2.js")"></script>
    <script src="@Url.Content("~/signalr/hubs")"></script>
    <script>
        $(function () {
            //setup hubs
            var counterHub = $.connection.counterHub;
            $.connection.hub.start().done(function () {
                //do something after hub connected
            });
            //function receive data from server. server call in on code behind
            counterHub.client.UpdateCount = function (count) {
                $('#UserOnline').text(count);
            }
        });
</script>
