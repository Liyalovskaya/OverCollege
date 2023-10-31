using OC.ConfigData;
using OC.Core.Operations;

namespace OC.Core.Locations
{
    public sealed class School_C2 : Location
    {
        public override void Initialize()
        {
            base.Initialize();
            Activities.Add(new GotoClass());
        }
    }
}
