using System;

namespace Valossy.Controls.Generals;

public interface IListenToSelected
{
    public void SelectedItemChanged(object sender, EventArgs e);
}