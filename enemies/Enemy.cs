using Godot;
using System;

public class Enemy : Character
{
    public override void _Ready()
    {
        base._Ready();
        Game.enemy = this;
    }
}
