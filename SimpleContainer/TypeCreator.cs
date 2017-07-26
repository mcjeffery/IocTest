using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using SimpleContainer.Exceptions;
using SimpleContainer.Resources;

namespace SimpleContainer
{
    internal class TypeCreator
    {
        private readonly Container _container;

        public TypeCreator(Container container)
        {
            _container = container;
        }

        public object CreateInstance(Type type)
        {
            var constructorParameters = CreateConstructorParameters(type);

            var instance = Activator.CreateInstance(type,
                BindingFlags.CreateInstance |
                BindingFlags.Public |
                BindingFlags.Instance |
                BindingFlags.OptionalParamBinding,
                null,
                constructorParameters,
                CultureInfo.CurrentCulture);

            return instance;
        }

        private object[] CreateConstructorParameters(Type type)
        {
            var constructorsInfo = type.GetConstructors(BindingFlags.Instance | BindingFlags.Public);
            if (constructorsInfo.Length == 0)
                throw new ConstructorException(string.Format(Strings.NoConstructors, type.Name));

            var constructor = constructorsInfo[0];
            var result = new List<object>();
            foreach (var paramInfo in constructor.GetParameters())
            {
                if (paramInfo.IsOptional)
                {
                    result.Add(Type.Missing);
                    continue;
                }

                var registration = _container.ResolveRegistration(paramInfo.ParameterType);
                if (registration == null)
                    throw new RegistrationNotFoundException(string.Format(Strings.ParameterNotFound, paramInfo.Name, type.Name));
                var paramInstance = CreateInstance(registration.Type);
                result.Add(paramInstance);
            }

            return result.ToArray();
        }
    }
}