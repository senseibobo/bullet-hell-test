using Godot;
using System;
using System.Collections.Generic;

public class Character : Node2D
{
    [Export] public float max_health;
    public float health;

    public Game gameNode;
    public override void _Ready()
    {
        health = max_health;
        gameNode = GetNode<Game>("/root/Game");
    }
}
