namespace NancyFxApp.Modules
{
    public class ItemModule : Generics.Module<Models.Item>
    {
        public ItemModule() : base("/item")
        {
        }
    }
}