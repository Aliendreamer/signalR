namespace signalR.hubs
{
    using Microsoft.AspNetCore.SignalR;
  
    public class SignalHub:Hub
    {
        private readonly ILogger<SignalHub> _logger;

        public SignalHub(ILogger<SignalHub> logger)
        {
            _logger = logger;
        }
    }
}