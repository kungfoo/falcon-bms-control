namespace Common.SimSupport
{
    public interface ISimOutput
    {
        string FriendlyName { get; }
        bool HasListeners { get; }
        string Id { get; }
    }
}