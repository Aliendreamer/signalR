namespace signalR.hubs
{
    using Microsoft.AspNetCore.SignalR;
    using System;
    using System.Security.Cryptography;

    public class PeriodicMessageService : IHostedService, IDisposable
    {
        private readonly IHubContext<SignalHub> _hubContext;
        private Timer? _timer;

        public PeriodicMessageService(IHubContext<SignalHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(SendMessage, new object(), TimeSpan.Zero, TimeSpan.FromSeconds(5));
            return Task.CompletedTask;
        }

        private void SendMessage(object? state)
        {
            Random rng = new Random();
            decimal min = 1.0m;  // Minimum value
            decimal max = 100.0m; // Maximum value

            List<HubMessage> data = [
                new() { Currency = "USD", Value = Math.Round((decimal)rng.NextDouble() * (max - min) + min, 2) },
                new () { Currency = "EUR", Value =Math.Round((decimal)rng.NextDouble() * (max - min) + min, 2) },
                new () { Currency = "JPY", Value = Math.Round((decimal) rng.NextDouble() *(max - min) + min, 2) },
                new() { Currency = "GBP", Value = Math.Round((decimal) rng.NextDouble() *(max - min) + min, 2) },
                new () { Currency = "AUD", Value = Math.Round((decimal) rng.NextDouble() *(max - min) + min, 2) },
                new() { Currency = "CAD", Value = Math.Round((decimal) rng.NextDouble() *(max - min) + min, 2) },
                new() { Currency = "CHF", Value = Math.Round((decimal) rng.NextDouble() *(max - min) + min, 2) },
                new() { Currency = "CNY", Value = Math.Round((decimal) rng.NextDouble() *(max - min) + min, 2) },
                new () { Currency = "SEK", Value = Math.Round((decimal) rng.NextDouble() *(max - min) + min, 2) },
                new () { Currency = "NZD", Value = Math.Round((decimal) rng.NextDouble() *(max - min) + min, 2) },

            ];
            _hubContext.Clients.All.SendAsync("ReceiveMessage", data);
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
