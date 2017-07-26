namespace SimpleContainer.Tests
{
    public interface IGraphRoot
    {
        ICalculator Calculator { get; }
        
        IUserInfo UserInfo { get; }
    }
}