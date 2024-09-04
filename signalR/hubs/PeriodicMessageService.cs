using Microsoft.AspNetCore.SignalR;

namespace signalR.hubs
{
    public class PeriodicMessageService : IHostedService, IDisposable
    {
        private readonly IHubContext<SignalHub> _hubContext;
        private Timer _timer;

        public PeriodicMessageService(IHubContext<SignalHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(SendMessage, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
            return Task.CompletedTask;
        }

        private void SendMessage(object state)
        {
            _hubContext.Clients.All.SendAsync("ReceiveMessage", "Server", $"Current time: {DateTime.Now}");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }

}
