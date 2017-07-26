using System;

namespace SimpleContainer
{
    public class TypeRegistration
    {
        public TypeRegistration(Type type, LifecycleType lifecycleType)
        {
            Type = type;
            LifecycleType = lifecycleType;
        }

        public Type Type { get; }

        public LifecycleType LifecycleType { get; }
    }
}