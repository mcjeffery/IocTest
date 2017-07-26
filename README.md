# Simple IoC Container

This project is a basic IoC container written as an educational exercise. It includes basic registration, lifecycle management, and integration with ASP.Net MVC.

## Usage

The basic workflow is to create a Container instance, register service instances, call container.Complete(), and then use the container to resolve instances. The container is write-only before Complete() is called and read-only afterwards.

### Lifecycle Management

Transient is the default lifecycle when no other lifecycle is specified. Singleton lifcycles are also supported. For example, to register a logger service as a transient use the following:

```csharp
Container container = new Container();
container.Register<ILogger, Logger>();
container.Complete();
```

To register the same service as a singleton, specify the lifecycle as part of the Register method call:

```csharp
Container container = new Container();
container.Register<ILogger, Logger>(LifecycleType.Singleton);
container.Complete();
```

### Resolving Instances

Instances can be resolved using the Resolve method as follows:

```csharp
var logger = container.Resolve<ILogger>();
```

## Dependency Injection

The container supports constructor injection for all registered types.

## ASP.Net MVC Integration

ASP.Net MVC integration is provided by the SimpleDependencyResolver class. SimpleDependencyResolver is an implementation of IDependencyResolver.

### Sample MVC application

The sample MVC application is an implementation of one of Microsoft's ASP.Net MVC samples modified to use constructor injection on the controllers using SimpleContainer.