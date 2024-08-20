using System;
using Godot;

namespace Valossy.Inputs;

public class InputPressed
{
    public static void HandleInput(string inputName, Action action)
    {
        if (Input.IsActionPressed(inputName) == true)
        {
            action?.Invoke();
        }
    }
}