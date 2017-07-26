using Xunit;
using System.Linq;
using SimpleContainer.Exceptions;

namespace SimpleContainer.Tests
{
    public class ContainerTests
    {
        [Fact]
        public void Dispose_DisposableSingleton_DisposeCalled()
        {
            var container = new Container();

            container.Register<ICalculator, Calculator>(LifecycleType.Singleton);
            container.Complete();

            var calc = container.Resolve<ICalculator>() as Calculator;
            Assert.False(calc.Disposed);
            container.Dispose();
            Assert.True(calc.Disposed);
        }

        [Fact]
        public void Resolve_Singleton_ResolvesSameObject()
        {
            var container = new Container();
            
            container.Register<ICalculator, Calculator>(LifecycleType.Singleton);
            container.Complete();

            var calc1 = container.Resolve<ICalculator>();
            var calc2 = container.Resolve<ICalculator>();
            Assert.Same(calc1, calc2);
        }

        [Fact]
        public void Resolve_Transient_ResolvesUniqueObjects()
        {
            var container = new Container();
            
            container.Register<ICalculator, Calculator>();
            container.Complete();

            var calc1 = container.Resolve<ICalculator>();
            var calc2 = container.Resolve<ICalculator>();
            Assert.NotSame(calc1, calc2);
        }

        [Fact]
        public void Resolve_DependencyInjection_FullGraphResolved()
        {
            var container = new Container();
            container.Register<ICalculator, Calculator>();
            container.Register<IUserInfo, UserInfo>();
            container.Register<ILogger, Logger>();
            container.Register<IGraphRoot, GraphRoot>();
            container.Complete();

            var root = container.Resolve<IGraphRoot>();
            Assert.IsType<Calculator>(root.Calculator);
            Assert.IsType<UserInfo>(root.UserInfo);
            Assert.IsType<Logger>(root.UserInfo.Logger);
        }

        [Fact]
        public void Resolve_UnknownType_ResolutionError()
        {
            var container = new Container();
            container.Register<ICalculator, Calculator>();
            container.Complete();

            Assert.Throws<RegistrationNotFoundException>(() => container.Resolve<ILogger>());
        }

        [Fact]
        public void Resolve_CompleteNotCalled_NotCompleteError()
        {
            var container = new Container();

            Assert.Throws<ContainerNotCompletedException>(() => container.Resolve<ILogger>());
        }

        [Fact]
        public void Resolve_DIMissingRegistration_ResolutionError()
        {
            var container = new Container();
            container.Register<IGraphRoot, GraphRoot>();
            container.Complete();

            Assert.Throws<RegistrationNotFoundException>(() => container.Resolve<IGraphRoot>());
        }

        [Fact]
        public void Register_NotInterface_RegistrationError()
        {
            var container = new Container();

            Assert.Throws<TypeNotInterfaceException>(() => container.Register<Logger, Calculator>());
        }

        [Fact]
        public void Register_Completed_CompletedError()
        {
            var container = new Container();
            container.Register<ICalculator, Calculator>();
            container.Complete();

            Assert.Throws<ContainerCompletedException>(() => container.Register<ILogger, Logger>());
        }

        [Fact]
        public void Register_InterfaceNotImplemented_RegistrationError()
        {
            var container = new Container();

            Assert.Throws<InterfaceNotImplementedException>(() => container.Register<ILogger, Calculator>());
        }

        [Fact]
        public void Register_MultipleInstances_AllInstancesResolved()
        {
            var container = new Container();
            container.Register<ILogger, Logger>();
            container.Register<ILogger, ConsoleLogger>();
            container.Complete();

            var instances = container.ResolveAll<ILogger>();
            Assert.Collection(instances, instance => { Assert.IsType<Logger>(instance); },
                instance => Assert.IsType<ConsoleLogger>(instance));
        }

        [Fact]
        public void Register_MultipleInstancesMixedLifecycle_CorrectLifecycle()
        {
            var container = new Container();
            container.Register<ILogger, Logger>();
            container.Register<ILogger, ConsoleLogger>(LifecycleType.Singleton);
            container.Complete();

            var instances = container.ResolveAll<ILogger>().ToList();
            var instances2 = container.ResolveAll<ILogger>().ToList();
            Assert.NotSame(instances[0], instances2[0]);
            Assert.Same(instances[1], instances2[1]);
        }
    }
}