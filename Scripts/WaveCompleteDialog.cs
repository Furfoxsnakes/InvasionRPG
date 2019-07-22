using Godot;
using System;

public class WaveCompleteDialog : AcceptDialog
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    private Button _noButton;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _noButton = AddButton("No", true, "OnCancel");
    }

    private void _on_WaveCompleteDialog_confirmed()
    {
        Game.Player.IsActive = true;
    }
    
    private void _on_WaveCompleteDialog_custom_action(String action)
    {
        GetTree().Quit();
    }

}





