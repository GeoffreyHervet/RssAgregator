using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy.Bootstrappers.StructureMap;
using StructureMap;
using StructureMap.Graph;

namespace NancyFxApp
{
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
                    // scanner.AddAllTypesOf<IRepository>();
                });
            });
            base.ConfigureApplicationContainer(container);
        }
    }
}