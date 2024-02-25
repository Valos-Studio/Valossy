using System.Collections.Generic;

namespace HeavenAbandoned.Framework.UserInterfaces.Controls.Collections
{
    public interface IBindingCollection
    {
        public List<object> GetItemsSafe();
    }
}