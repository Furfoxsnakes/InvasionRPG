using Godot;
using System;
using System.Collections.Generic;

public class Game : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    public static Player Player;
    
    public static Arena Arena;

    // Called when the node enters the scene tree for the first time.
    public override void _EnterTree()
    {
        Arena = GetNode<Arena>("Arena");
        this.AddObserver(OnWaveComplete, Arena.WaveCompleteNotification);
    }

    private void OnWaveComplete(object sender, object args)
    {
        GetNode<WaveCompleteDialog>("WaveCompleteDialog").Show();
        Player.IsActive = false;
    }

    public override void _Ready()
    {
        
    }
}
