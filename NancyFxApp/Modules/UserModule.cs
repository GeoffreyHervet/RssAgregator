namespace NancyFxApp.Modules
{
    using NancyFxApp.Generics;
    using Nancy;
    using Nancy.Security;

    public class UserModule : Generics.Module<Models.User>
    {
        public UserModule() : base("/")
        {
        }
        public override bool isDisabledAction(Action a)
        {
            switch (a)
            {
                case Action.CREATE:
//                case Action.READ:
//                case Action.UPDATE:
//                case Action.DELETE:
                case Action.LIST:
                case Action.VALIDATORS:
                    return true;
            }
            return false;
        }
    }
}