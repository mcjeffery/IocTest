using System;
using System.Collections.Concurrent;

namespace SimpleContainer
{
    internal class LifecycleManager : IDisposable
    {
        private readonly ConcurrentDictionary<LifecycleType, LifecyclePolicy> _lifecycleManagers =
            new ConcurrentDictionary<LifecycleType, LifecyclePolicy>();

        private readonly TypeCreator _creator;
        private readonly Container _container;

        public LifecycleManager(Container container)
        {
            _creator = new TypeCreator(container);
            _container = container;
        }

        public object CreateInstance(Type interfaceType, TypeRegistration registration)
        {
            var manager = GetLifecycleManager(registration.LifecycleType);
            return manager == null
                ? _creator.CreateInstance(registration.Type)
                : manager.CreateOrRetrieveInstance(_container, interfaceType, registration);
        }

        private LifecyclePolicy GetLifecycleManager(LifecycleType lifecycleType)
        {
            if (lifecycleType == LifecycleType.Transient) return null;
            return _lifecycleManagers.GetOrAdd(lifecycleType,
                (type) => type == LifecycleType.Singleton ? new SingletonLifecyclePolicy() : null);
        }

        public void Dispose()
        {
            foreach (var kvp in _lifecycleManagers)
            {
                var disposable = kvp.Value as IDisposable;
                disposable?.Dispose();
            }
        }
    }
}