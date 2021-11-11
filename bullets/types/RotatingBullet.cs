using Godot;
using System;

public class RotatingBullet : Bullet
{
    private float rotationSpeed = 3.5f;
    private bool textureRotating = true;

    public RotatingBullet() { }
    public RotatingBullet (float[] additionalArgs) : base(additionalArgs) 
    {
        rotationSpeed = additionalArgs[0];
        textureRotating = Convert.ToBoolean(additionalArgs[1]);
    }
	public override void Process(float delta)
    {
        velocity = velocity.Rotated(rotationSpeed * delta);
        if (textureRotating) rotation = velocity.Angle();
        base.Process(delta);
    }
}
