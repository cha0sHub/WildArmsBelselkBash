namespace WildArmsRandomizer.Features.Randomizers
{
    internal interface IItemRandomizer
    {
        byte GetMutatedItemId(byte oldItemId, double mutationRate, double standardDeviation);
        void RandomizeEquipment();
    }
}