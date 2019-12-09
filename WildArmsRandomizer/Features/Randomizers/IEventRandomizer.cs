using WildArmsModel.Model.Events;

namespace WildArmsRandomizer.Features.Randomizers
{
    internal interface IEventRandomizer
    {
        void RandomizeEventCollection(IEventCollection eventCollection);
    }
}