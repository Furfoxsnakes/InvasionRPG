using Godot;
using System;

public class Monster : Battler
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    [Export] public float MoveSpeed = 50;

    private Vector2 _target;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Stats[StatTypes.HP] = 10;
    }

    public override void _PhysicsProcess(float delta)
    {
        _target = Game.Player.GlobalPosition;

        if (_target == null)
        {
            _anim.Stop();
            return;
        }

        MoveToTarget(delta);
    }

    private void MoveToTarget(float delta)
    {
        var target = (_target - GlobalPosition).Normalized();
        var collision = MoveAndCollide(target * MoveSpeed * delta);
        
        if (collision != null && collision.Collider is Player)
        {
            var player = collision.Collider as Player;
            
            if (_attackTimer.IsStopped())
            {
                Attack(player, 5);
                _attackTimer.Start();
            }
        }

        if (_anim.IsPlaying()) return;

        _anim.Play("Walk");
        
    }

    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);
        _anim.Stop();
        _anim.Play("Damaged");
    }

    public override void Kill()
    {
        base.Kill();
        Game.RemoveBattler(this);
        QueueFree();
    }
}
