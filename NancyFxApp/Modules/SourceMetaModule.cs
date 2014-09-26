namespace NancyFxApp.Modules
{
    using NancyFxApp.Generics;

    public class SourceMetaModule : Generics.Module<Models.SourceMeta>
    {
        public SourceMetaModule() : base("/sources-meta")
        {
        }
        public override bool isDisabledAction(Action a)
        {
            switch (a)
            {
//                case Action.CREATE:
                case Action.READ:
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