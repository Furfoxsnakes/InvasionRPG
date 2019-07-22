using Godot;
using System;

public class Battler : KinematicBody2D
{
    public Stats Stats;

    protected AnimationPlayer _anim;
    protected Timer _attackTimer;

    public override void _EnterTree()
    {
        Stats = GetNode<Stats>("Stats");
        _anim = GetNode<AnimationPlayer>("Anim");
        _attackTimer = GetNode<Timer>("AttackTimer");
    }

    public virtual void TakeDamage(int amount)
    {
        Stats[StatTypes.HP] -= amount;
        
        GD.Print($"{Name} has been hit for {amount} damage.");
        // TODO: add floating text

        if (Stats[StatTypes.HP] <= 0)
        {
            Kill();
        }
    }

    public virtual void Attack(Battler target, int amount)
    {
        target.TakeDamage(amount);
    }

    public virtual void Kill()
    {
        // TODO: add floating text
        GD.Print($"{Name} has been killed");
    }
    
}
