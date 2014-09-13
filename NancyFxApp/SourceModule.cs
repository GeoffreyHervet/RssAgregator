using System;
using System.Collections.Generic;
using Nancy;
using Nancy.ModelBinding;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure;


namespace NancyFxApp
{
    public class SourceModule : NancyModule
    {
        private readonly ISourceRepository _repository;
        public SourceModule(ISourceRepository repository)
            : base("/sources")
        {
            _repository = repository;

            Get["/hello"] = _ =>
            {
                var db = new PetaPoco.Database("TestConnection");
        
                // Show all articles    
                foreach (var a in db.Query<Source>("SELECT * FROM Source"))
                {
                    Console.WriteLine("{0} - {1}", a.Id, a.Url);
                }

                return "Hello";
            };

            Get["/insert"] = _ =>
            {
                var source = new Source();
                source.Url = "http://google.com";

                var db = new PetaPoco.Database("TestConnection");
                db.Insert(source);
                return Negotiate
                    .WithStatusCode(HttpStatusCode.OK)
                    .WithModel(source);
            };
            Get["/"] = x =>
            {
                return HttpStatusCode.NotImplemented;
            };

            Post["/"] = _ =>
            {
                return HttpStatusCode.NotImplemented;
            };
            Put["/{id:int}"] = parameters =>
            {
                return HttpStatusCode.NotImplemented;
            };

            Delete["/{id:int}"] = parameters =>
            {
                return HttpStatusCode.NotImplemented;
            };

            // TODO Remove me
            Get["/test/{id}"] = parameters =>
            {
                int id = parameters.id;
                var SourceModel = _repository.GetById(id);

                return Negotiate
                    .WithStatusCode(HttpStatusCode.OK)
                    .WithModel(SourceModel);
            };

            Get["/all"] = parameters =>
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
            if (id == 10)
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

    [PetaPoco.TableName("Source")]
    [PetaPoco.PrimaryKey("Id")]
    [PetaPoco.ExplicitColumns]
    public class Source
    {
        public Source()
        {
            this.CreatedAt = DateTime.Now;
        }
        [PetaPoco.Column("id")] public int Id { get; set; }
        [PetaPoco.Column("url")] public string Url { get; set; }
        [PetaPoco.Column("created_at")] public DateTime CreatedAt { get; set; }
        [PetaPoco.Column("updated_at")] public DateTime UpdatedAt { get; set; }
    }

    public class MyContext : DbContext, IMyContext
    {
        public MyContext() { }
        public MyContext(string cnxStr) : base(cnxStr) { }

        public IDbSet<Source> Sources { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Source>().Property(d => d.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Source>().HasKey(d => d.Id);
        }
    }

    public interface IMyContext
    {
        IDbSet<Source> Sources { get; set; }

        DbEntityEntry Entry(object entity);
        int SaveChanges();
    }
}