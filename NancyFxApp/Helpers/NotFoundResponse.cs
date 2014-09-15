using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using Nancy.Responses.Negotiation;

namespace NancyFxApp.Helpers
{
    public class NotFoundResponse
    {
        public static Negotiator NotFoundWithId(Negotiator negociator, int id)
        {
            return negociator
                .WithStatusCode(HttpStatusCode.NotFound)
                .WithModel(String.Format("Item with id {0} not found.", id))
            ;
        }
    }
}