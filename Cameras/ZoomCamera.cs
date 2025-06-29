using Godot;

namespace Valossy.Cameras;

[GlobalClass]
public partial class ZoomCamera : Camera2D
{
    [Signal]
    public delegate void MoveToTargetEventHandler(Vector2 target);

    [ExportCategory("ZoomOptions")]
    [Export]
    public float MinZoom { get; set; } = 0.1f;

    [Export] public float MaxZoom { get; set; } = 2.0f;
    [Export] public float ZoomIncrement { get; set; } = 0.1f;

    [ExportCategory("Shortcuts")]
    [Export()]
    public Shortcut ShortcutZoomIn { get; set; }
    [Export()] public Shortcut ShortcutZoomOut { get; set; }

    private float targetZoom;

    public override void _Ready()
    {
        targetZoom = Zoom.X;
    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);

        if (ShortcutZoomOut.MatchesEvent(@event) == true)
        {
            CameraZoom(1f);
        }

        if (ShortcutZoomIn.MatchesEvent(@event) == true)
        {
            CameraZoom(-1f);
        }
    }

    public override void _EnterTree()
    {
        base._EnterTree();

        MoveToTarget += OnMoveToTarget;
    }

    public override void _ExitTree()
    {
        base._ExitTree();

        MoveToTarget -= OnMoveToTarget;
    }

    private void OnMoveToTarget(Vector2 target)
    {
        Offset = target;
    }

    private void CameraZoom(float positive)
    {
        float newZoom = this.targetZoom + (positive * ZoomIncrement);

        if (newZoom <= MinZoom) return;
        if (newZoom >= MaxZoom) return;

        this.targetZoom = newZoom;

        Tween tween = CreateTween();

        tween.TweenProperty(this, Camera2D.PropertyName.Zoom.ToString(), new Vector2(this.targetZoom, this.targetZoom),
            0.5f);

        tween.Play();
    }
}