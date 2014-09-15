using System;
using System.Collections.Generic;
using Nancy;
using Nancy.ModelBinding;
using NancyFxApp.Repository;
using Nancy.Validation;

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
                int page = parameters.page;
                page = Math.Max(page != null ? page : 0, 1);

                return Negotiate
                    .WithStatusCode(HttpStatusCode.OK)
                    .WithModel(repository.findAll<Source>(page));
            };

            Get["/validators"] = parameters =>
            {
                var validator =
                    this.ValidatorLocator.GetValidatorForType(typeof(Source));

                return this.Response.AsJson(validator.Description);
            };

            Post["/"] = _ =>
            {
                Source model = this.Bind();
                var result = this.Validate(model);

                if (!result.IsValid)
                {
                    return Negotiate
                        .WithStatusCode(HttpStatusCode.UnprocessableEntity)
                        .WithModel(result);
                }

                repository.insert(model);

                return Negotiate
                    .WithStatusCode(HttpStatusCode.OK)
                    .WithModel(model);
            };

            Put["/{id:int}"] = parameters =>
            {
                int id = parameters.id;
                var model = repository.find<Source>(id);
                if (model == null)
                {
                    return Negotiate.WithStatusCode(HttpStatusCode.NotFound).WithModel(String.Format("Item with id {0} not found.", id));
                }

                model = this.BindTo(model);
                var result = this.Validate(model);

                if (!result.IsValid)
                {
                    return Negotiate
                        .WithStatusCode(HttpStatusCode.UnprocessableEntity)
                        .WithModel(result);
                }

                repository.update(model);

                return Negotiate
                    .WithStatusCode(HttpStatusCode.OK)
                    .WithModel(model);
            };

            Get["/{id:int}"] = parameters =>
            {
                int id = parameters.id;
                var model = repository.find<Source>(id);
                if (model == null)
                {
                    return Negotiate.WithStatusCode(HttpStatusCode.NotFound).WithModel(String.Format("Item with id {0} not found.", id));
                }

                return Negotiate
                    .WithStatusCode(HttpStatusCode.OK)
                    .WithModel(model);
            };

            Delete["/{id:int}"] = parameters =>
            {
                int id = parameters.id;
                var model = repository.find<Source>(id);
                if (model == null)
                {
                    return Negotiate.WithStatusCode(HttpStatusCode.NotFound).WithModel(String.Format("Item with id {0} not found.", id));
                }

                repository.delete<Source>(id);
                return Negotiate
                    .WithStatusCode(HttpStatusCode.OK)
                    .WithModel(model);
            };
        }
    }
}