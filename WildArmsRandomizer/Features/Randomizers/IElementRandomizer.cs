namespace WildArmsRandomizer.Features.Randomizers
{
    internal interface IElementRandomizer
    {
        byte MutateElementalAttribute(byte originalAttribute);
    }
}