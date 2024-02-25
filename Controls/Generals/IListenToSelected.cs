using System;

namespace HeavenAbandoned.Framework.UserInterfaces.Controls.General
{
    public interface IListenToSelected
    {
        public void SelectedItemChanged(object sender, EventArgs e);
    }
}