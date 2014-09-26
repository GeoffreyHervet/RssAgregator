namespace NancyFxApp.Modules
{
    using NancyFxApp.Generics;
    public class ItemInformationModule : Generics.Module<Models.ItemInformation>
    {
        public ItemInformationModule()
            : base("/item-infos")
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