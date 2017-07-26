namespace SimpleContainer.Tests
{
    public interface IUserInfo
    {
        ILogger Logger { get; }

        string UserName { get; }
    }
}