using Godot;
using System;
public class DirectionalGenerator : Generator
{
	[Export] protected float directionRandom = 0.0f;
	[Export] protected float horizontalOffsetRandom = 0.0f;
	[Export] protected float verticalOffsetRandom = 0.0f;

	protected Vector2 direction;
	[Export] public Vector2 Direction
    {
		set { direction = value.Normalized(); }
		get { return direction; }
    }
	[Export] public float Angle
    {
		set { Direction = Vector2.Right.Rotated(value); }
		get { return Direction.Angle(); }
    }
	protected override void Shoot()
    {
		RandomNumberGenerator rand = new RandomNumberGenerator();
		rand.Randomize();
		Vector2 _dir = direction.Rotated(rand.RandfRange(-directionRandom, directionRandom));
		Vector2 position = position = GlobalPosition +
			_dir * rand.RandfRange(-verticalOffsetRandom, verticalOffsetRandom) +
			_dir.Rotated(Mathf.Pi / 2.0f) * rand.RandfRange(-horizontalOffsetRandom, horizontalOffsetRandom);
		AddBullet(_dir * speed, position);
	}
}
