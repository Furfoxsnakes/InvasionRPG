using Godot;
using System;

public class Spawn : Position2D
{
    [Export]
    private PackedScene _monsterToSpawn;

    private int _numMonstersPerWave;
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

    public void Start(int numToSpawn)
    {
        _numMonstersPerWave = numToSpawn;
        
        SpawnMonster();
    }

    private void SpawnMonster()
    {
        Monster monster = _monsterToSpawn.Instance() as Monster;
        monster.Position = Position;

        Game.Arena.Spawn(monster);

        _numMonstersSpawned++;
        
        GD.Print($"{_numMonstersSpawned}/{_numMonstersPerWave} spawned");

        if (_spawnRateTimer.IsStopped())
            _spawnRateTimer.Start();
    }
    
    private void _on_SpawnRate_timeout()
    {
        if (_numMonstersSpawned >= _numMonstersPerWave)
        {
            _spawnRateTimer.Stop();
            IsDoneSpawning = true;
            _numMonstersSpawned = 0;
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






