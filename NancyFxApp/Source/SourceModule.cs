using System;
using System.Collections.Generic;
using Nancy;
using Nancy.ModelBinding;
using NancyFxApp.Repository;
using Nancy.Validation;
using Nancy.Responses.Negotiation;

namespace NancyFxApp.Source
{
    public class SourceModule : NancyModule
    {
        enum Action { CREATE, READ, UPDATE, DELETE};
        protected Repository.Repository _repository;
        public SourceModule()
            : base("/sources")
        {
            StaticConfiguration.DisableErrorTraces = false;
            _repository = Repository.Repository.getInstance();

            Get["/list/{page?:int)"] = parameters =>
            {
                int page = Math.Max(parameters.page, 1);

                return Negotiate
                    .WithStatusCode(HttpStatusCode.OK)
                    .WithModel(_repository.findAll<Source>(page));
            };

            Get["/validators"] = parameters =>
            {
                var validator =  this.ValidatorLocator.GetValidatorForType(typeof(Source));

                return this.Response.AsJson(validator.Description);
            };

            Post["/"] = _ =>
            {
                return processEntity(0, Action.CREATE);
            };

            Put["/{id:int}"] = parameters =>
            {
                return processEntity(parameters.id, Action.UPDATE);
            };

            Get["/{id:int}"] = parameters =>
            {
                return processEntity(parameters.id, Action.READ);
            };

            Delete["/{id:int}"] = parameters =>
            {
                return processEntity(parameters.id, Action.DELETE);
            };
        }

        protected Source _find(int id)
        {
            return _repository.find<Source>(id);
        }

        protected Negotiator _reponseModel(Source model)
        {
            return Negotiate
                .WithStatusCode(HttpStatusCode.OK)
                .WithModel(model);
        }

        protected Negotiator _notFoundWithId(int id)
        {
            return Negotiate
                .WithStatusCode(HttpStatusCode.NotFound)
                .WithModel(String.Format("Item with id {0} not found.", id))
            ;
        }

        private Negotiator processEntity(int id, Action action)
        {
            Source model = null;

            if (action == Action.CREATE)
            {
                model = this.Bind();
            }
            else
            {
                model = _find(id);
                if (model == null)
                {
                    return _notFoundWithId(id);
                }
            }

            if (action == Action.UPDATE)
            {
                model = this.BindTo(model);
            }

            if (action == Action.UPDATE || action == Action.CREATE)
            {
                model = this.BindTo(model);
                var result = this.Validate(model);

                if (!result.IsValid)
                {
                    return Negotiate
                        .WithStatusCode(HttpStatusCode.UnprocessableEntity)
                        .WithModel(result);
                }

                if (action == Action.UPDATE)
                {
                    _repository.update(model);
                }
                else if (action == Action.CREATE)
                {
                    _repository.insert(model);
                }
            }
            else if (action == Action.DELETE)
            {
                _repository.delete<Source>(id);
            }

            return _reponseModel(model);
        }
    }
}