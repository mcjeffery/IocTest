using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SimpleContainer.Mvc
{
    public class SimpleDependencyResolver : IDependencyResolver
    {
        private readonly Container _container;
        private readonly IDependencyResolver _baseResolver;

        public SimpleDependencyResolver(Container container, IDependencyResolver baseResolver)
        {
            _container = container;
            _baseResolver = baseResolver;
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return typeof(IController).IsAssignableFrom(serviceType)
                    ? CreateController(serviceType)
                    : _container.Resolve(serviceType);
            }
            catch
            {
                return _baseResolver.GetService(serviceType);
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return _container.ResolveAll(serviceType);
            }
            catch
            {
                return _baseResolver.GetServices(serviceType);
            }
        }

        private object CreateController(Type serviceType)
        {
            var creator = new TypeCreator(_container);
            return creator.CreateInstance(serviceType);
        }
    }
}
