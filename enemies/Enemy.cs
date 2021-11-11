using Godot;
using System;

public class Enemy : Character
{
    [Export] public float radius = 16.0f;
    public override void _Ready()
    {
        base._Ready();
        Game.enemy = this;
    }
}
