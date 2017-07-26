using System;

namespace SimpleContainer
{
    internal abstract class LifecyclePolicy : IDisposable
    {
        public abstract object CreateOrRetrieveInstance(Container container, Type interfaceType, TypeRegistration registration);

        protected virtual void Dispose(bool disposing)
        {
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}