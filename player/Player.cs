using Godot;
using System;

public class Player : Character
{
	[Export] float movement_speed = 100.0f;

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
	}
}
