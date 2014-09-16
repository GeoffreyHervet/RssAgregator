namespace NancyFxApp.Generics
{
    using System;
    using Nancy;
    using Nancy.Responses.Negotiation;
    using Nancy.ModelBinding;
    using Nancy.Validation;
    public enum Action { CREATE, READ, UPDATE, DELETE, LIST, VALIDATORS };
    public abstract class Module<T> : NancyModule
    {

        protected Repository.Repository _repository;

        public Module(String baseUri)
            : base(baseUri)
        {
            StaticConfiguration.DisableErrorTraces = false;
            _repository = Repository.Repository.getInstance();


            if (!isDisabledAction(Action.LIST))
            {
                Get["/list/{page?:int)"] = parameters =>
                {
                    int page = Math.Max(parameters.page, 1);

                    return Negotiate
                        .WithStatusCode(HttpStatusCode.OK)
                        .WithModel(_repository.findAll<T>(page));
                };
            }

            if (!isDisabledAction(Action.VALIDATORS))
            {
                Get["/validators"] = parameters =>
                {
                    var validator = this.ValidatorLocator.GetValidatorForType(typeof(T));

                    return this.Response.AsJson(validator.Description);
                };
            }


            if (!isDisabledAction(Action.CREATE))
            {
                Post["/"] = _ =>
                {
                    return processEntity(0, Action.CREATE);
                };
            }

            if (!isDisabledAction(Action.UPDATE))
            {
                Put["/{id:int}"] = parameters =>
                {
                    return processEntity(parameters.id, Action.UPDATE);
                };
            }

            if (!isDisabledAction(Action.READ))
            {
                Get["/{id:int}"] = parameters =>
                {
                    return processEntity(parameters.id, Action.READ);
                };
            }

            if (!isDisabledAction(Action.DELETE))
            {
                Delete["/{id:int}"] = parameters =>
                {
                    return processEntity(parameters.id, Action.DELETE);
                };
            }

        }

        public virtual bool isDisabledAction(Action a)
        {
            return false;
        }

        protected T _find(int id)
        {
            return _repository.find<T>(id);
        }

        protected Negotiator _reponseModel(T model)
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
            T model;

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
                _repository.delete<T>(id);
            }

            return _reponseModel(model);
        }

    }
}