namespace NancyFxApp.Modules
{
    using NancyFxApp.Generics;
    public class InformationNameModule : Generics.Module<Models.InformationName>
    {
        public InformationNameModule()
            : base("/info-name")
        {
        }
        public override bool isDisabledAction(Action a)
        {
            switch (a)
            {
                case Action.CREATE:
//                case Action.READ:
                case Action.UPDATE:
                case Action.DELETE:
//                case Action.LIST:
                case Action.VALIDATORS:
                    return true;
            }
            return false;
        }
    }
}