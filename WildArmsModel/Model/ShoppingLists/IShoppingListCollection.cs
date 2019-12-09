using System.Collections.Generic;
using DiscDataManipulation.Model;

namespace WildArmsModel.Model.ShoppingLists
{
    public interface IShoppingListCollection : IParentMappedCollection<ShoppingListObject>
    {
        IReadOnlyCollection<ShoppingListObject> ShoppingLists { get; }
    }
}