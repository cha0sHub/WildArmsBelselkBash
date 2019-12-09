using WildArmsModel.Model.ShoppingLists;

namespace WildArmsRandomizer.Features.Randomizers
{
    internal interface IShoppingListRandomizer
    {
        void RandomizeShoppingLists(IShoppingListCollection shoppingListCollection);
    }
}