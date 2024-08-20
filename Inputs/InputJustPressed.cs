using System;
using Godot;

namespace Valossy.Inputs;

public class InputJustPressed
{
    public static void HandleInput(string inputName, Action action)
    {
        if (Input.IsActionJustPressed(inputName) == true)
        {
            action?.Invoke();
        }
    }
}