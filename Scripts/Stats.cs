using Godot;
using System;
using System.Collections.Generic;

public class Stats : Node
{
    public int this[StatTypes type]
    {
        get => _data[(int)type];
        set { SetValue(type, value); }
    }
    [Export] private int[] _data = new int[(int)StatTypes.Count];

    static Dictionary<StatTypes, string> _willChangeNotifications = new Dictionary<StatTypes, string>();
    static Dictionary<StatTypes, string> _didChangeNotifications = new Dictionary<StatTypes, string>();

    public static string WillChangeNotification(StatTypes type)
    {
        if (!_willChangeNotifications.ContainsKey(type))
            _willChangeNotifications.Add(type, $"Stats.{type}WillChange");

        return _willChangeNotifications[type];
    }

    public static string DidChangeNotification(StatTypes type)
    {
        if (!_didChangeNotifications.ContainsKey(type))
            _didChangeNotifications.Add(type, $"Stats.{type}DidChange");

        return _didChangeNotifications[type];
    }

    public void SetValue(StatTypes type, int value)
    {
        var oldValue = this[type];

        if (oldValue == value) return;

        _data[(int) type] = value;
        this.PostNotification(DidChangeNotification(type), oldValue);
    }
}
