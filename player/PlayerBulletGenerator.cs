using Godot;
using System;

public class PlayerBulletGenerator : Generator
{
    public override void _Ready()
    {
        shooting = false;
    }
    public override void _UnhandledInput(InputEvent @event)
    {
        if(Input.IsActionJustPressed("shoot")) shooting = true;
        if(Input.IsActionJustReleased("shoot")) shooting = false;
    }
    public override void CheckDistance(Bullet bullet) 
    {
        if(IsInstanceValid(Game.enemy) && bullet.position.DistanceSquaredTo(Game.enemy.GlobalPosition) < bullet.radiusSquared)
        {
            bullet.currentTime = bullet.lifeTime + 1.0f;
        }
    }
    protected override void Shoot() {
        AddBullet(new Vector2(0f,-1f)*speed,GlobalPosition);
    }
}
