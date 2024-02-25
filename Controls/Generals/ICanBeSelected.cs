using System;

namespace Valossy.Controls.Generals;

public interface ICanBeSelected
{
    public event EventHandler ControlSelected;
}