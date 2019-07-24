using Godot;
using System;

public class HealthLabel : Label
{
    public override void _Ready()
    {
        this.AddObserver(OnValueUpdated, Stats.DidChangeNotification(StatTypes.HP));
    }

    private void OnValueUpdated(object arg1, object arg2)
    {
        UpdateText();
    }

    private void UpdateText()
    {
        Text = Game.Player.Stats[StatTypes.HP].ToString();
    }
}
