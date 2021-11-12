using Godot;
using System;

public class CircleGenerator : Generator
{
    [Export] protected int count;
    [Export] protected float angle;
    [Export] protected float headStart = 0.0f;
    [Export] protected float incrementDegrees = 0.0f;
    [Export] protected float incrementDegreesRandom = 0.0f;

    float currentIncrement = 0.0f;
    protected override void Shoot()
    {
        base.Shoot();
        for(int i = 0; i < count; i++)
        {
            float _angle = angle + (float)Math.PI * 2 / count * i + Mathf.Deg2Rad(currentIncrement);
            Vector2 direction = Vector2.Right.Rotated(_angle);
            Vector2 velocity = speed * direction;
            Vector2 position = GlobalPosition + headStart * velocity;
            AddBullet(velocity, position);
        }
        RandomNumberGenerator rand = new RandomNumberGenerator();
        rand.Randomize();
        currentIncrement += incrementDegrees + rand.RandfRange(-incrementDegreesRandom,incrementDegreesRandom);
    }
}