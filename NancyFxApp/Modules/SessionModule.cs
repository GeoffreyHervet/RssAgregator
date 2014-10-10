namespace NancyFxApp.Modules
{
    using System;
    using System;
    using System.Dynamic;
    using Nancy;
    using Nancy.Authentication.Forms;
    using Nancy.Extensions;
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
                    return this.Context.GetRedirect("/session?error=true&login=" + (string)this.Request.Form.email);
                }

                DateTime? expiry = null;
                if (this.Request.Form.RememberMe.HasValue)
                {
                    expiry = DateTime.Now.AddDays(7);
                }

                return this.LoginAndRedirect(user.getRealGuid(), expiry);
            };
        }

    }
}