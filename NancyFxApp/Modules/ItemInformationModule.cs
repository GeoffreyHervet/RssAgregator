namespace NancyFxApp.Modules
{
    public class ItemInformationModule : Generics.Module<Models.ItemInformation>
    {
        public ItemInformationModule()
            : base("/item-infos")
        {
        }
    }
}