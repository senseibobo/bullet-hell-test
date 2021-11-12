using Godot;
using System;

public class SquareWaveBullet : GraphBullet
{
    public SquareWaveBullet() { }
    public SquareWaveBullet(float[] additionalArgs) : base(additionalArgs) 
    {

    }
    public override float getFunctionResult()
    {
        return Mathf.Abs((float)Convert.ToInt32((currentTime * frequency+ phase) % 1.0f > 0.5f)) - 0.5f;
    }
}