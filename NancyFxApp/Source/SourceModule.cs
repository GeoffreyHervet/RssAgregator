using System;
using System.Collections.Generic;
using Nancy;
using NancyFxApp.Repository;

namespace NancyFxApp.Source
{
    public class SourceModule : NancyModule
    {
        public SourceModule()
            : base("/sources")
        {
            StaticConfiguration.DisableErrorTraces = false;
            var repository = Repository.Repository.getInstance();

            Get["/list/{page?:int)"] = parameters =>
            {
                var page = parameters.page;
                int pageNb = 0;
                if (page != null)
                {
                    pageNb = (int)page;
                }
                pageNb = Math.Max(pageNb, 1);

                Console.WriteLine(pageNb);

                var listOfSources = repository.findAll<Source>(pageNb);

                return Negotiate
                    .WithStatusCode(HttpStatusCode.OK)
                    .WithModel(listOfSources);
            };

            Get["/validators"] = parameters =>
            {
                var validator =
                    this.ValidatorLocator.GetValidatorForType(typeof(Source));

                return this.Response.AsJson(validator.Description);
            };
            Post["/"] = _ =>
            {
                return HttpStatusCode.NotImplemented;
            };

            Put["/{id:int}"] = parameters =>
            {
                return HttpStatusCode.NotImplemented;
            };

            Get["/{id:int}"] = parameters =>
            {
                int id = parameters.id;
                var SourceModel = repository.find<Source>(id);

                return Negotiate
                    .WithStatusCode(HttpStatusCode.OK)
                    .WithModel(SourceModel);
            };

            Delete["/{id:int}"] = parameters =>
            {
                return HttpStatusCode.NotImplemented;
            };
        }
    }
}