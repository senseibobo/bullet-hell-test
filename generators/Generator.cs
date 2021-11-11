using Godot;
using System;
using System.Collections.Generic;

public class Generator : Node2D
{
    [Export] protected float interval = 0.2f;
    [Export] protected float speed = 50.0f;
    [Export] protected float lifeTime = 3.0f;
    [Export] protected Vector2 size = new Vector2(16f,16f);
    [Export] protected float radius = 3.0f;
    [Export] protected Texture texture = GD.Load<Texture>("res://bullets/textures/01.png");
    [Export] protected CSharpScript bulletType = GD.Load<CSharpScript>("res://bullets/Bullet.cs");
    [Export] protected int shotsPerInterval = 1;
    [Export] protected float[] additionalArgs = new float[3] {1f,1f,1f};
    [Export] protected int frameCount = 1;
    [Export] protected float frameDuration = 0.1f;
    [Export] protected bool processing = true;
    [Export] protected bool shooting = true;
    [Export] protected bool RemoveAllBullets
    {
        set { for (int i = 0; i < bullets.Count; i++) bullets[i].Free(); bullets.RemoveRange(0,bullets.Count); }
        get { return false; }
    }

    Game gameNode;

    float timer = 0.0f;
    int bulletIndex = 0;

    public List<Bullet> bullets = new List<Bullet>();

    public override void _Ready()
    {
        base._Ready();
        gameNode = GetNode<Game>("/root/Game");
    }

    public override void _Process(float delta)
    {
        base._PhysicsProcess(delta);
        if (shooting)
        {
            timer += delta;
            if (timer > interval)
            {
                for (int i = 0; i < shotsPerInterval; i++)
                {
                    Shoot();
                    bulletIndex++;
                }
                timer -= interval;
            }
        }
        foreach(Bullet bullet in bullets)
        {
            ProcessBullet(bullet, delta);
        }
        bullets.RemoveAll(HasExpired);

        Update();
    }
    private bool HasExpired(Bullet bullet)
    {
        if (bullet.lifeTime < bullet.currentTime)
        {
            bullet.Free();
            return true;
        }
        return false;
    }
    private void ProcessBullet(Bullet bullet, float delta)
    {
        if (processing)
        {
            bullet.currentTime += delta;
            bullet.Process(delta);
        }
        CheckDistance(bullet);
    }
    public virtual void CheckDistance(Bullet bullet) 
    {
        if(IsInstanceValid(Game.player) && bullet.position.DistanceSquaredTo(Game.player.GlobalPosition) < bullet.radiusSquared)
        {
            bullet.currentTime = bullet.lifeTime + 1.0f;
        }
    }
    public override void _Draw()
    {
        base._Draw();
        foreach(Bullet bullet in bullets)
        {
            Vector2 pos = bullet.position - bullet.size.Rotated(bullet.rotation) / 2.0f - GlobalPosition;
            DrawSetTransform(pos, bullet.rotation, bullet.scale);
            
            int currentFrame = (int)(bullet.currentTime / bullet.frameDuration) % bullet.frameCount;
            Vector2 frameSize = new Vector2(bullet.texture.GetSize().x / bullet.frameCount, bullet.texture.GetSize().y);

            Rect2 rect = new Rect2(0f, 0f, bullet.size);
            Rect2 srcRect = new Rect2(bullet.texture.GetSize().x / (bullet.frameCount) * currentFrame, 0f, frameSize);
            DrawTextureRectRegion(bullet.texture, rect, srcRect);
        }
    }
    public Bullet AddBullet(Vector2 velocity, Vector2 position)
    {
        Bullet bullet = (Bullet)bulletType.New(additionalArgs);
        bullet.velocity = velocity;
        bullet.position = position;
        bullet.rotation = bullet.velocity.Angle();
        bullet.lifeTime = lifeTime;
        bullet.additionalArgs = additionalArgs;
        bullet.frameCount = frameCount;
        bullet.frameDuration = frameDuration;
        bullet.texture = texture;
        bullet.size = size;
        bullet.radiusSquared = Mathf.Pow(radius,2);
        bullets.Add(bullet);
        bullet.Ready();
        return bullet;
    }
    public void RemoveBullet(Bullet bullet)
    {
        bullets.Remove(bullet);
    }
    protected virtual void Shoot()
    {

    }
}