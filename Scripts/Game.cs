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

    [Export]
    public int Wave
    {
        get => _wave;
        set
        {
            _wave = value;
            this.PostNotification("Game.WaveUpdated");
        }
    }

    private int _wave = 3;

    // Called when the node enters the scene tree for the first time.
    public override void _EnterTree()
    {
        Arena = GetNode<Arena>("Arena");
        this.AddObserver(OnWaveComplete, Arena.WaveCompleteNotification);
    }

    public override void _Ready()
    {
        Arena.StartWave(Wave);
    }

    private void OnWaveComplete(object sender, object args)
    {
        this.PostNotification(NotificationLabel.ShowNotification, $"WAVE {Wave} COMPLETED!!!");
        Wave++;
        GetNode<Timer>("TimeBetweenWaves").Start();
    }
    
    private void _on_TimeBetweenWaves_timeout()
    {
        Arena.StartWave(Wave);
    }
}