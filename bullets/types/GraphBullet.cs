using Godot;
using System;

public class GraphBullet : Bullet
{
    protected float amplitude = 100.0f;
    protected float frequency = 10.0f;
    protected float phase = 0.0f;
    private bool textureRotating = true;

    private Vector2 startPos;
    private Vector2 startDir;
    protected float speed;
    public GraphBullet() { }
    public GraphBullet(float[] additionalArgs) : base(additionalArgs)
    {
        amplitude = additionalArgs[0];
        frequency = additionalArgs[1];
        phase = additionalArgs[2];
        textureRotating = Convert.ToBoolean(additionalArgs[3]);
    }
    public override void Process(float delta)
    {
        Vector2 oldPosition = position;
        position = startPos + speed * startDir * currentTime + startDir.Rotated(Mathf.Pi/2)*amplitude*getFunctionResult();
        if (textureRotating) rotation = (position-oldPosition).Angle();
    }
    public virtual float getFunctionResult() {
        return 0.0f;
    }
    public override void Ready()
    {
        base.Ready();
        startPos = position;
        startDir = velocity.Normalized();
        speed = velocity.Length();
    }
}