namespace NancyFxApp.Modules
{
    using System;
    using System;
    using System.Dynamic;
    using Nancy;
    using Nancy.Authentication.Forms;
    using Nancy.Extensions;
    using System.Collections.Generic;

    public class SessionModule : NancyModule
    {

        protected Generics.Repository _repository;

        public SessionModule()
            : base("/session")
        {
            StaticConfiguration.DisableErrorTraces = false;
            _repository = Generics.Repository.getInstance();

            Get["/"] = _ =>
            {
                dynamic model = new ExpandoObject();
                model.Errored = this.Request.Query.error.HasValue;

                return View["session/form", model];
            };

            Delete["/"] = _ =>
            {
                this.LogoutWithoutRedirect();
                if (Accept("application/json") || Accept("application/xml"))
                {
                    var ret = new Dictionary<string, bool>();
                    return this.LogoutWithoutRedirect();
                }
                return this.LogoutAndRedirect("~/");
            };

            Get["/logout"] = _ =>
            {
                return this.LogoutAndRedirect("~/");
            };


            Post["/"] = x =>
            {
                var user = UserDatabase.ValidateUser((string)this.Request.Form.email, (string)this.Request.Form.Password);

                if (user == null)
                {
                    if (Accept("application/json") || Accept("application/xml"))
                    {
                        var ret = new Dictionary<string, bool>();
                        ret["Success"] = false;
                        return Negotiate
                            .WithStatusCode(HttpStatusCode.UnprocessableEntity)
                            .WithModel(ret)
                        ;

                    }
                    return this.Context.GetRedirect("/session?error=true&username=" + (string)this.Request.Form.email);
                }

                DateTime? expiry = null;
                if (this.Request.Form.RememberMe.HasValue)
                {
                    expiry = DateTime.Now.AddDays(7);
                }

                if (Accept("application/json") || Accept("application/xml"))
                {
                    var ret = new Dictionary<string, bool>();
                    ret["Success"] = true;
                    return this.Login(user.getRealGuid(), expiry)
                        .WithStatusCode(HttpStatusCode.OK)
                    ;

                }

                return this.LoginAndRedirect(user.getRealGuid(), expiry);
            };
        }

        public bool Accept(string contentType)
        {
            IEnumerable<System.Tuple<string, decimal>> accept = Context.Request.Headers.Accept;
            IEnumerator<System.Tuple<string, decimal>> enumerator = accept.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Current.Item1.Contains(contentType))
                {
                    return true;
                }
            }
            return false;
        }

    }
}