using Godot;
using System;
using System.Collections.Generic;

public class Game : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    public static Player Player;
    
    private static List<Battler> _spawnedBattlers = new List<Battler>();

    private static List<Spawn> _spawnPoints = new List<Spawn>();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        var spawnPoints = GetNode("Spawn Points").GetChildren();

        foreach (var point in spawnPoints)
        {
            _spawnPoints.Add(point as Spawn);
        }

        foreach (var spawn in _spawnPoints)
        {
            spawn.Start();
        }
    }

    public static void RemoveBattler(Battler battler)
    {
        if (!_spawnedBattlers.Contains(battler))
        {
            GD.PrintErr($"{battler} is not currently spawned");
            return;
        }

        _spawnedBattlers.Remove(battler);
        
        if (_spawnedBattlers.Count == 0)
            GD.Print("Spawn the boss!");
        
    }

    public static void AddBattler(Battler battler)
    {
        if (_spawnedBattlers.Contains(battler))
        {
            GD.PrintErr($"{battler} has already been added to the spawn list");
            return;
        }

        _spawnedBattlers.Add(battler);
    }
}
