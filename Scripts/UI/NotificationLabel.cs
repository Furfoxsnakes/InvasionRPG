using Godot;
using System;

public class NotificationLabel : Label
{
    private Timer _timer;

    public static readonly string DidShowNotification = "Notification.DidShow";
    public static readonly string DidHideNotification = "Notifaction.DidHide";

    public static readonly string ShowNotification = "Notification.Show";

    private bool _pausePlayerActions;

    public override void _EnterTree()
    {
        this.AddObserver(OnShowNotification, ShowNotification);
    }

    private void OnShowNotification(object sender, object args)
    {
        Text = args.ToString();
        Visible = true;
        _timer.Start(3);
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _timer = GetNode<Timer>("Timer");
    }

    public virtual void Hide(bool unpausePlayerActions = true)
    {
        Visible = false;
    }
    
    private void _on_Timer_timeout()
    {
        Hide();
    }
}


