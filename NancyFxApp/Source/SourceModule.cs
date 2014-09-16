namespace NancyFxApp.Source
{
    public class SourceModule : Generics.Module<Source>
    {
        public SourceModule() : base("/sources")
        {
        }

        public override bool isDisabledAction(Generics.Action a)
        {
            return (a == Generics.Action.READ);
        }

    }
}