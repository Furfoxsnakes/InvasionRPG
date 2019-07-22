using Godot;
using System;

public class Camera : Camera2D
{

    public override void _Process(float delta)
    {
        // interpolates the camera to "look" towards the mouse cursor
        var playerPosition = GetParent<Node2D>().GlobalPosition;
        var mousePosition = GetGlobalMousePosition();
        GlobalPosition = playerPosition.LinearInterpolate(mousePosition, 0.15f);
    }
}
