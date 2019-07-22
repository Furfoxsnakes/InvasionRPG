using Godot;
using System;

public class Projectile : Area2D
{
    public int speed = 5;
    private Vector2 _velocity;

    public object Owner;
    
    public override void _Ready()
    {
        SetAsToplevel(true);
        var target = GetGlobalMousePosition();
        _velocity = -(Position - target).Normalized() * speed;
    }

    public override void _Process(float delta)
    {
        Position += _velocity;
    }

    private void _on_Timer_timeout()
    {
        QueueFree();
    }

    private void _on_Projectile_area_entered()
    {
    }
    
    private void _on_Projectile_body_entered(object body)
    {
        if (body == Owner) return;

        switch (body)
        {
            case Monster _ when Owner is Monster:
            case Player _ when Owner is Player:
                return;
            case Battler battler:
            {
                battler.TakeDamage(2);
                break;
            }
        }

        QueueFree();

    }

}




