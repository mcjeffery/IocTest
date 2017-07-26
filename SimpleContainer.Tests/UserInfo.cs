namespace SimpleContainer.Tests
{
    public class UserInfo : IUserInfo
    {
        public UserInfo(ILogger logger, string userName = "TestUser")
        {
            Logger = logger;
            UserName = userName;
        }

        public ILogger Logger { get; }
        public string UserName { get; }
    }
}