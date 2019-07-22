using Godot;
using System;

public class Spawn : Position2D
{
    [Export]
    private PackedScene _monsterToSpawn;

    [Export] public int NumMonstersPerWave = 1;
    private int _numMonstersSpawned = 0;
    public bool IsDoneSpawning = false;

    [Export] public bool IsBoss = false;

    private Timer _respawnTimer;
    private Timer _spawnRateTimer;

    public override void _Ready()
    {
        _respawnTimer = GetNode<Timer>("Respawn");
        _spawnRateTimer = GetNode<Timer>("SpawnRate");
    }

    public void Start()
    {
        // do not spawn if this is a boss spawn point
        //if (IsBoss) return;
        
        SpawnMonster();
        //_respawnTimer.Start();
    }

    private void SpawnMonster()
    {
        Monster monster = _monsterToSpawn.Instance() as Monster;
        monster.Position = this.Position;

        Game.Arena.Spawn(monster);

        _numMonstersSpawned++;

        if (_spawnRateTimer.IsStopped())
            _spawnRateTimer.Start();
    }
    
    private void _on_SpawnRate_timeout()
    {
        if (_numMonstersSpawned >= NumMonstersPerWave)
        {
            _spawnRateTimer.Stop();
            IsDoneSpawning = true;
            return;
        }
        
        SpawnMonster();
    }
    
    private void _on_Respawn_timeout()
    {
        _numMonstersSpawned = 0;
        SpawnMonster();
    }
}






