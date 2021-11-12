using Godot;
using System;
using System.Collections.Generic;

public class Character : Node2D
{
    [Export] public float maxHealth;
    public float health;

    public Game gameNode;
    public override void _Ready()
    {
        health = maxHealth;
        gameNode = GetNode<Game>("/root/Game");
    }
    public virtual void Hit() 
    {
        health -= 1;
        if(health <= 0) {
            Death();
        }
    }
    public virtual void Death() 
    {
        QueueFree();
    }
}
