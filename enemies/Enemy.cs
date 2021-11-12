using Godot;
using System;

public class Enemy : Character
{
    [Export] public float radius = 16.0f;
    protected int phase = 1;
    public override void _Ready()
    {
        base._Ready();
        Game.enemy = this;
    }
}
