using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace SimpleContainer
{
    internal class SingletonLifecyclePolicy : LifecyclePolicy
    {
        private readonly ConcurrentDictionary<Type, List<object>> _singletons = new ConcurrentDictionary<Type, List<object>>();

        public override object CreateOrRetrieveInstance(Container container, Type interfaceType, TypeRegistration registration)
        {
            var instanceList = _singletons.GetOrAdd(interfaceType, new List<object>());
            lock (instanceList)
            {
                var singleton = instanceList.FirstOrDefault((instance) => instance.GetType() == registration.Type);
                if (singleton != null) return singleton;
                var creator = new TypeCreator(container);
                singleton = creator.CreateInstance(registration.Type);
                instanceList.Add(singleton);
                return singleton;
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (!disposing) return;

            var list = _singletons.ToList();
            _singletons.Clear();
            foreach (var kvp in list)
            {
                var instanceList = kvp.Value;
                foreach (var singleton in instanceList)
                {
                    var idisposable = singleton as IDisposable;
                    idisposable?.Dispose();
                }
            }
        }
    }
}