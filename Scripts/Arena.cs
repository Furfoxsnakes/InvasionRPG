using Godot;
using System;
using System.Collections.Generic;

public class Arena : TileMap
{
    private List<Battler> _spawnedBattlers = new List<Battler>();

    private List<Spawn> _spawnPoints = new List<Spawn>();

    private bool _onBossWave = false;

    public static readonly string WaveCompleteNotification = "Arena.WaveComplete";

    private int _currentWave;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        var spawnPoints = GetNode("Spawn Points").GetChildren();

        foreach (var point in spawnPoints)
        {
            _spawnPoints.Add(point as Spawn);
        }
    }

    public void StartWave(int wave)
    {
        _currentWave = wave;
        this.PostNotification(NotificationLabel.ShowNotification, $"WAVE {wave} STARTING!");
        GetNode<Timer>("StartDelay").Start();
    }

    public void Spawn(Battler battler)
    {
        CallDeferred("add_child", battler);
        AddBattler(battler);
    }

    public void RemoveBattler(Battler battler)
    {
        if (!_spawnedBattlers.Contains(battler))
        {
            GD.PrintErr($"{battler} is not currently spawned");
            return;
        }

        _spawnedBattlers.Remove(battler);

        if (_onBossWave)
        {
            this.PostNotification(WaveCompleteNotification);
            _onBossWave = false;
            return;
        }
        
        if (_spawnedBattlers.Count == 0)
            foreach (var point in _spawnPoints)
            {
                if (point.IsBoss)
                {
                    point.Start(1);
                    _onBossWave = true;
                }
            }
    }

    public void AddBattler(Battler battler)
    {
        if (_spawnedBattlers.Contains(battler))
        {
            GD.PrintErr($"{battler} has already been added to the spawn list");
            return;
        }

        _spawnedBattlers.Add(battler);
    }
    
    private void _on_StartDelay_timeout()
    {
        foreach (var spawn in _spawnPoints)
        {
            if (spawn.IsBoss) continue;
            
            spawn.Start(_currentWave);
        }
    }
}



