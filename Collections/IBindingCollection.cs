using System.Collections.Generic;

namespace Valossy.Collections
{
    public interface IBindingCollection
    {
        public List<object> GetItemsSafe();
    }
}