using Godot;
using System;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;

public class Player : Battler
{
    [Export] private int MoveSpeed = 100;

    private Vector2 _movement;

    private PackedScene _projectile;

    private Timer _fireRateTimer;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _projectile = ResourceLoader.Load("res://Scenes/Projectile.tscn") as PackedScene;
        //_fireRateTimer = GetNode<Timer>("FireRate");
        Game.Player = this;
        Stats[StatTypes.HP] = 100;
    }

    public override void _PhysicsProcess(float delta)
    {
        HandleKeyboardMove(delta);
        HandleMouseMove();
        HandleMouseClick();
    }

    private void HandleKeyboardMove(float delta)
    {
        _movement = Vector2.Zero;

        if (Input.IsActionPressed("MoveLeft"))
            _movement.x = -1;
        else if (Input.IsActionPressed("MoveRight"))
            _movement.x = 1;

        if (Input.IsActionPressed("MoveUp"))
            _movement.y = -1;
        else if (Input.IsActionPressed("MoveDown"))
            _movement.y = 1;

        MoveAndCollide(_movement.Normalized() * MoveSpeed * delta);
        
    }

    private void HandleMouseMove()
    {
        //var angle = GlobalPosition.AngleToPoint(GetGlobalMousePosition());
        var radAngle = GetLocalMousePosition().Angle();
        var degAngle = Mathf.Rad2Deg(radAngle);

        string animName = "";

        if (degAngle < 22.5 && degAngle > -22.5)
        {
            // Looking Left
            animName = "WalkRight";
        }
        else if (degAngle > 22.5 && degAngle < 67.5)
        {
             // looking down right
             animName = "WalkDownRight";
        }
        else if (degAngle > 67.5 && degAngle < 112.5)
        {
            // looking down
            animName = "WalkDown";
        } 
        else if (degAngle > 112.5 && degAngle < 157.5)
        {
            // looking down left
            animName = "WalkDownLeft";
        }
        else if (degAngle > 157.5 || degAngle < -157.5)
        {
            // looking right
            animName = "WalkLeft";
        } 
        else if (degAngle > -157.5 && degAngle < -112.5)
        {
            // looking up left
            animName = "WalkUpLeft";
        }
        else if (degAngle > -112.5 && degAngle < -67.5)
        {
            // looking up
            animName = "WalkUp";
        }
        else if (degAngle > -67.5 && degAngle < -22.5)
        {
            // looking up right
            animName = "WalkUpRight";
        }

        if (_anim.CurrentAnimation != animName)
        {
            _anim.Play(animName);
        }
    }

    private void HandleMouseClick()
    {
        if (!_attackTimer.IsStopped()) return;

        if (Input.IsActionPressed("Fire"))
        {
            Fire();
            _attackTimer.Start();
        }
    }

    public void Fire()
    {
        var instance = _projectile.Instance() as Projectile;
        instance.Position = GetGlobalPosition();
        instance.Owner = this;
        AddChild(instance);
    }
    
}
