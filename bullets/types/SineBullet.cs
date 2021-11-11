using Godot;
using System;

public class SineBullet : Bullet
{
    private float amplitude = 100.0f;
    private float frequency = 10.0f;
    private float phase = 0.0f;
    private bool textureRotating = true;

    private Vector2 startPos;
    private Vector2 startDir;
    private float speed;
    public SineBullet() { }
    public SineBullet(float[] additionalArgs) : base(additionalArgs)
    {
        amplitude = additionalArgs[0];
        frequency = additionalArgs[1];
        phase = additionalArgs[2];
        textureRotating = Convert.ToBoolean(additionalArgs[3]);
    }
    public override void Process(float delta)
    {
        Vector2 oldPosition = position;
        position = startPos + speed * startDir * currentTime + startDir.Rotated(Mathf.Pi/2)*amplitude*(Mathf.Sin(phase+frequency*currentTime));
        if (textureRotating) rotation = (position-oldPosition).Angle();
    }
    public override void Ready()
    {
        base.Ready();
        startPos = position;
        startDir = velocity.Normalized();
        speed = velocity.Length();
    }
}