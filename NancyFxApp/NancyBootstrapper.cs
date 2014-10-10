/*namespace NancyFxApp
{
    using System;
    using Nancy.Bootstrappers.StructureMap;
    using StructureMap;
    using StructureMap.Graph;

    public class NancyBootstrapper : StructureMapNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(IContainer container)
        {
            container.Configure(x =>
            {
                x.Scan(scanner =>
                {
                    scanner.WithDefaultConventions();
                    scanner.TheCallingAssembly();
                    // scanner.eAddAllTypesOf<IRepository>();
                });
            });
            base.ConfigureApplicationContainer(container);
        }
    }
}*/