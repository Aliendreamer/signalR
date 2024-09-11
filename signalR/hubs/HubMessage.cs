namespace signalR.hubs
{
    public class HubMessage
    {
        public string Currency { get; init; } = string.Empty;
        public decimal Value { get; init; }
    }
}
