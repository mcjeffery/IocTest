namespace SimpleContainer.Tests
{
    public class GraphRoot : IGraphRoot
    {
        public GraphRoot(ICalculator calculator, IUserInfo userInfo)
        {
            Calculator = calculator;
            UserInfo = userInfo;
        }

        public ICalculator Calculator { get; }
        public IUserInfo UserInfo { get; }
    }
}