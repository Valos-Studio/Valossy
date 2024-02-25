using System;

namespace HeavenAbandoned.Framework.UserInterfaces.Controls.General
{
    public interface ICanBeSelected 
    {
        public event EventHandler ControlSelected;
    }
}