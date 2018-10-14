namespace GroupProjectBackend.Config
{
    using Unity;

    public static class UnityConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();

            // default per request
            container.RegisterTypes(
                AllClasses.FromLoadedAssemblies(),
                WithMappings.FromMatchingInterface,
                WithName.Default,
                m => new HierarchicalLifetimeManager());

            // instances
            container.RegisterInstance(typeof(IDataProtectionProvider), dataProtectionProvider, new ContainerControlledLifetimeManager());

            // database and identity
            container.RegisterType<JusttDbContext, JusttDbContext>(new HierarchicalLifetimeManager());
            container.RegisterType<DbContext, ApplicationDbContext>(new HierarchicalLifetimeManager());
            // WebApi integration
            config.DependencyResolver = new UnityHierarchicalDependencyResolver(container);
        }
    }
}
