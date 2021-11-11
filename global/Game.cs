using Godot;
using System;

public class Game : Node2D
{
	public static Player player;
	public static Enemy enemy;
	public override void _Ready()
	{
		
	}
	public static float AngleToAngle(float from, float to) {
		return Mathf.PosMod(to - from + Mathf.Pi * 2.0f, Mathf.Pi * 2.0f) - Mathf.Pi;
	}
}
