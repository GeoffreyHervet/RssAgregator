using System;
using System.Collections.Generic;
using Nancy;
using Nancy.ModelBinding;


namespace NancyFxApp
{
    public class SourceModule : NancyModule
    {
        private readonly ISourceRepository _repository;
        public SourceModule(ISourceRepository repository)
        {
            _repository = repository;

            Get["/hello"] = _ => "Hello world";

            Get["/source/{id}"] = parameters =>
            {
                int id = parameters.id;
                var SourceModel = _repository.GetById(id);

                return Negotiate
                    .WithStatusCode(HttpStatusCode.OK)
                    .WithModel(SourceModel);
            };

            Get["/sources"] = parameters =>
            {
                var listOfSources = _repository.GetAll();

                return Negotiate
                    .WithStatusCode(HttpStatusCode.OK)
                    .WithModel(listOfSources);
            };

        }
    }

    public interface ISourceRepository
    {
        Source GetById(int id);
        IList<Source> GetAll();
    }

    public class SourceRepository : ISourceRepository
    {
        public Source GetById(int id)
        {
            if (id <= 0)
            {
                throw new SourceNotFoundException();
            }
            return new Source
            {
                Id = id,
                Url = "http://google.fr/"
            };
        }

        public IList<Source> GetAll()
        {
            return new List<Source>
            {
                new Source{
                    Id = 1,
                    Url = "http://google.com/"
                },
                new Source
                {
                    Id = 2,
                    Url = "http://epitech.eu/"
                }
            };
        }
    }

    public class SourceNotFoundException : Exception
    {
        
    }
    public class Source
    {
        public int Id { get; set; }
        public string Url { get; set; }
    }
}