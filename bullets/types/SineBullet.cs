using Godot;
using System;

public class SineBullet : GraphBullet
{
    public SineBullet() { }
    public SineBullet(float[] additionalArgs) : base(additionalArgs) 
    {

    }
    public override float getFunctionResult()
    {
        return Mathf.Sin(phase+frequency*currentTime);
    }
}