using System;

namespace SimpleContainer.Tests
{
    public class Calculator : ICalculator, IDisposable
    {
        public int Add(int x, int y)
        {
            throw new NotImplementedException();
        }

        public int Subtract(int x, int y)
        {
            throw new NotImplementedException();
        }

        public bool Disposed { get; set; }

        public void Dispose()
        {
            Disposed = true;
        }
    }
}