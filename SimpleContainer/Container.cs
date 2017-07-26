using System;
using System.Collections.Generic;
using System.Linq;
using SimpleContainer.Exceptions;
using SimpleContainer.Resources;

namespace SimpleContainer
{
    public sealed class Container : IDisposable
    {
        private readonly Dictionary<Type, List<TypeRegistration>> _registrations =
            new Dictionary<Type, List<TypeRegistration>>();
        private readonly LifecycleManager _lifecycleManager;
        private bool _isCompleted;

        public Container()
        {
            _lifecycleManager = new LifecycleManager(this);
        }

        public void Register<TInterface, TService>()
        {
            Register<TInterface, TService>(LifecycleType.Transient);
        }

        public void Register<TInterface, TService>(LifecycleType lifecycleType)
        {
            if (_isCompleted)
                throw new ContainerCompletedException(Strings.RegistrationCompleted);

            if (!typeof(TInterface).IsInterface)
                throw new TypeNotInterfaceException(string.Format(Strings.TypeNotInterface, typeof(TInterface).Name));

            if (!typeof(TInterface).IsAssignableFrom(typeof(TService)))
                throw new InterfaceNotImplementedException(string.Format(Strings.InterfaceNotImplemented, typeof(TInterface).Name, typeof(TService).Name));

            if (!_registrations.TryGetValue(typeof(TInterface), out List<TypeRegistration> registrationList))
            {
                registrationList = new List<TypeRegistration>();
                _registrations.Add(typeof(TInterface), registrationList);
            }

            if (!registrationList.Any(reg => reg.LifecycleType == lifecycleType && reg.Type == typeof(TService)))
                registrationList.Add(new TypeRegistration(typeof(TService), lifecycleType));
        }

        public T Resolve<T>()
        {
            return (T) Resolve(typeof(T));
        }

        public object Resolve(Type type)
        {
            if (!_isCompleted)
                throw new ContainerNotCompletedException(Strings.RegistrationNotCompleted);

            var registration = ResolveRegistration(type);
            if (registration == null)
                throw new RegistrationNotFoundException(string.Format(Strings.RegistrationNotFound, type.Name));
            
            return _lifecycleManager.CreateInstance(type, registration);
        }

        public IEnumerable<object> ResolveAll<T>()
        {
            return ResolveAll(typeof(T));
        }

        public IEnumerable<object> ResolveAll(Type type)
        {
            if (!_isCompleted)
                throw new ContainerNotCompletedException(Strings.RegistrationNotCompleted);

            return CreateInstances(type);
        }

        public void Complete()
        {
            if (_isCompleted) return;
            if (_registrations.Count == 0)
                throw new ContainerEmptyException(Strings.ContainerEmpty);

            _isCompleted = true;
        }

        private IEnumerable<object> CreateInstances(Type type)
        {
            var registrations = ResolveRegistrations(type);

            foreach (var registration in registrations)
            {
                yield return _lifecycleManager.CreateInstance(type, registration);
            }
        }

        internal TypeRegistration ResolveRegistration(Type type)
        {
            var registrations = ResolveRegistrations(type);
            return registrations?.FirstOrDefault();
        }

        private IEnumerable<TypeRegistration> ResolveRegistrations(Type type)
        {
            if (_registrations.TryGetValue(type, out List<TypeRegistration> registrationList))
            {
                foreach (var registration in registrationList)
                    yield return registration;
            }
        }
        
        public void Dispose()
        {
            _lifecycleManager.Dispose();
        }
    }
}
