using System;
using Godot;

namespace Valossy.Inputs;

public class InputJustReleased
{
    public static void HandleInput(string inputName, Action action)
    {
        if (Input.IsActionJustReleased(inputName) == true)
        {
            action?.Invoke();
        }
    }
}