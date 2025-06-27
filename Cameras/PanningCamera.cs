using Godot;
using NightSwarm.Frameworks.Statics;
using Valossy.Inputs;

namespace Valossy.Cameras;

[GlobalClass]
public partial class PanningCamera : Camera2D
{
    [Signal]
    public delegate void MoveToTargetEventHandler(Vector2 target);

    [Export]
    [ExportCategory("ZoomOptions")]
    public float MinZoom { get; set; } = 0.1f;

    [Export] public float MaxZoom { get; set; } = 2.0f;

    [Export] public float ZoomIncrement { get; set; } = 0.1f;

    private Vector2 mousePosition;

    private Vector2 lastOffset;

    private float targetZoom;

    private Viewport viewport;


    public override void _Ready()
    {
        this.viewport = GetViewport();

        targetZoom = Zoom.X;

        lastOffset = Vector2.Zero;

        viewport.CanvasCullMask = 1;
    }

    public override void _PhysicsProcess(double delta)
    {
        InputPressed.HandleInput(InputControl.SecondaryAction, CameraDrag);

        InputJustReleased.HandleInput(InputControl.SecondaryAction,
            () => { this.lastOffset = this.Offset; });

        InputJustPressed.HandleInput(InputControl.ScrollUpAction, () => CameraZoom(1f));

        InputJustPressed.HandleInput(InputControl.ScrollDownAction, () => CameraZoom(-1f));
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

    private void CameraDrag()
    {
        float zoomOffset = 1f;

        if (this.targetZoom < 1)
        {
            zoomOffset = 1 + ((1 - this.targetZoom) * 10 * 0.5f);
        }

        if (this.targetZoom > 1)
        {
            zoomOffset = 1 - ((1 - this.targetZoom) * 10 * 0.05f);
        }

        Vector2 mouseCurrentPosition = this.viewport.GetMousePosition() * zoomOffset;

        InputJustPressed.HandleInput(InputControl.SecondaryAction,
            () => { mousePosition = mouseCurrentPosition; });

        if (mousePosition.Equals(mouseCurrentPosition))
        {
            return;
        }

        this.Offset = this.lastOffset + mousePosition - mouseCurrentPosition;
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
