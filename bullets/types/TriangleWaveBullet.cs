using Godot;
using System;

public class TriangleWaveBullet : GraphBullet
{
    public TriangleWaveBullet() { }
    public TriangleWaveBullet(float[] additionalArgs) : base(additionalArgs) 
    {

    }
    public override float getFunctionResult()
    {
        return Mathf.Abs(((currentTime*frequency + phase) % 2.0f)-1.0f)-0.5f;
    }
}