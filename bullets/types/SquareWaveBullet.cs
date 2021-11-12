using Godot;
using System;

public class SquareWaveBullet : Bullet
{
    private float amplitude = 100.0f;
    private float frequency = 10.0f;
    private float phase = 0.0f;
    private bool textureRotating = true;

    private Vector2 startPos;
    private Vector2 startDir;
    private float speed;
    public SquareWaveBullet() { }
    public SquareWaveBullet(float[] additionalArgs) : base(additionalArgs)
    {
        amplitude = additionalArgs[0];
        frequency = additionalArgs[1];
        phase = additionalArgs[2];
        textureRotating = Convert.ToBoolean(additionalArgs[3]);
    }
    public override void Process(float delta)
    {
        Vector2 oldPosition = position;
        float result = Mathf.Abs((float)Convert.ToInt32((currentTime * frequency+ phase) % 1.0f > 0.5f)) - 0.5f;
        position = startPos + speed * startDir * currentTime + startDir.Rotated(Mathf.Pi/2)*amplitude*result;
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