using System.Collections.Generic;
using DiscDataManipulation.Model;

namespace WildArmsModel.Model.ShoppingLists
{
    public class ShoppingListCollection : ParentMappedCollection<ShoppingListObject>, IShoppingListCollection
    {
        public IReadOnlyCollection<ShoppingListObject> ShoppingLists => MappedObjectReadOnly;
    }
}
