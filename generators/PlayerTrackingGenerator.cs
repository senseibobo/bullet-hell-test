using Godot;
using System;
public class PlayerTrackingGenerator : Generator
{
    [Export] protected float rotationSpeed = 0.5f;
    [Export] protected float currentRotation = 0.0f;
    [Export] protected float horizontalOffsetRandom = 0.0f;
    [Export] protected float verticalOffsetRandom = 0.0f;
    protected float directionRandom;
	public override void _Process(float delta)
	{
		base._Process(delta);
        currentRotation = Mathf.MoveToward(
            currentRotation,
            currentRotation + Game.AngleToAngle(currentRotation,GlobalPosition.AngleToPoint(Game.player.GlobalPosition)),
            rotationSpeed*delta
        );
	}
	protected override void Shoot()
    {
		RandomNumberGenerator rand = new RandomNumberGenerator();
		rand.Randomize();
		Vector2 _dir = Vector2.Right.Rotated(currentRotation + rand.RandfRange(-directionRandom, directionRandom));
		Vector2 position = position = GlobalPosition +
			_dir * rand.RandfRange(-verticalOffsetRandom, verticalOffsetRandom) +
			_dir.Rotated(Mathf.Pi / 2.0f) * rand.RandfRange(-horizontalOffsetRandom, horizontalOffsetRandom);
		AddBullet(_dir * speed, position);
	}
}
