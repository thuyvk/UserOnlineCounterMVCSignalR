# User Online Counter MVC5 SignalR
Display user online with SignalR


## 1: Install SignalR to your project

Run nuget command

> Install-Package Microsoft.AspNet.SignalR -Version 2.2.2

## 2 : Create folder Hubs and a class CounterHub.cs inside
```
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
```
## 3: _Layout.cshtml page
```
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
```

## 4: Index.cshtml page
```
<h1 class="text-warning"><span id="UserOnline">0</span> Online</h1>
```

## 5: Startup.cs
```
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UserOnlineCounter.Startup))]
namespace UserOnlineCounter
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
```
