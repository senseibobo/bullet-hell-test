using Godot;
using System;

public class Player : Character
{
	[Export] float movement_speed = 100.0f;
	[Export] float invulnerabilityTime = 1.0f;
	public float hitTimer = 0.0f;

    public override void _Ready()
    {
        base._Ready();
		Game.player = this;
	}
    public override void _Process(float delta)
	{
		base._Process(delta);
		Vector2 move_vector = new Vector2();
		move_vector.x = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left");
		move_vector.y = Input.GetActionStrength("move_down") - Input.GetActionStrength("move_up");
		move_vector = move_vector.Normalized();
		Translate(move_vector * delta * movement_speed);
		hitTimer = Mathf.MoveToward(hitTimer,0.0f,delta);
	}
	public override void Hit()
	{
		base.Hit();
		hitTimer = invulnerabilityTime;
	}
}
