using Godot;
using System;

public class GravityBullet : Bullet
{
    private float gravity_x = 0f;
    private float gravity_y = 400f;
    private bool textureRotating = true;

    public GravityBullet() { }
    public GravityBullet(float[] additionalArgs) : base(additionalArgs)
    {
        gravity_x = additionalArgs[0];
        gravity_y = additionalArgs[1];
        textureRotating = Convert.ToBoolean(additionalArgs[2]);
    }
    public override void Process(float delta)
    {
        velocity += new Vector2(gravity_x, gravity_y) * delta;
        if (textureRotating) rotation = velocity.Angle();
        base.Process(delta);
    }
}