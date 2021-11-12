using Godot;
using System;

public class PlayerTrackingBullet : Bullet
{
    private float rotationSpeed = 1.0f;
    private bool textureRotating = true;
    private Player player;

    public PlayerTrackingBullet() { }
    public PlayerTrackingBullet(float[] additionalArgs) : base(additionalArgs)
    {
        player = Game.player;
        rotationSpeed = additionalArgs[0];
        textureRotating = Convert.ToBoolean(additionalArgs[1]);
    }
    public override void Process(float delta)
    {
        if(IsInstanceValid(player)) 
        {
            float angleToPlayer = position.AngleToPoint(player.GlobalPosition);
            float currentAngle = velocity.Angle();
            velocity = velocity.Rotated(rotationSpeed * Math.Sign(Math.Round(Game.AngleToAngle(currentAngle, angleToPlayer)))*delta);
            if (textureRotating) rotation = velocity.Angle();
        }
        base.Process(delta);
    }
}