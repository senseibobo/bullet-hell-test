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
    [Export] protected Texture texture = GD.Load<Texture>("res://bullets/textures/02.png");
    [Export] protected CSharpScript bulletType = GD.Load<CSharpScript>("res://bullets/Bullet.cs");
    [Export] protected int shotsPerInterval = 1;
    [Export] protected float[] additionalArgs = new float[3] {1f,1f,1f};
    [Export] protected int frameCount = 1;
    [Export] protected float frameDuration = 0.1f;
    [Export] protected bool processing = true;
    [Export] protected bool shooting = true;
    private MultiMeshInstance2D multimeshInstance;
    private MultiMesh multimesh;
    [Export] protected bool RemoveAllBullets
    {
        set { for (int i = 0; i < bullets.Count; i++) bullets[i].Free(); bullets.RemoveRange(0,bullets.Count); }
        get { return false; }
    }

    public CSharpScript BulletType 
    {
        set {bulletType = value;}
        get {return bulletType;}
    } 
    public float[] AdditionalArgs 
    {
        set {additionalArgs = value;}
        get {return additionalArgs;}
    }
    Game gameNode;

    float timer = 0.0f;
    int bulletIndex = 0;

    public List<Bullet> bullets = new List<Bullet>();

    public override void _Ready()
    {
        base._Ready();
        gameNode = GetNode<Game>("/root/Game");
        multimeshInstance = new MultiMeshInstance2D();
        multimeshInstance.Texture = texture;
        multimeshInstance.SetAsToplevel(true);
        multimesh = new MultiMesh();
        multimesh.ColorFormat = MultiMesh.ColorFormatEnum.None;
        multimeshInstance.Multimesh = multimesh;
        QuadMesh mesh = new QuadMesh();
        mesh.Size = size;
        multimesh.Mesh = mesh;
        AddChild(multimeshInstance);
    }

    public override void _Process(float delta)
    {
        base._Process(delta);
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
            multimesh.InstanceCount -= 1;
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
        if(IsInstanceValid(Game.player) && bullet.position.DistanceSquaredTo(Game.player.GlobalPosition) < bullet.radiusSquared && Game.player.hitTimer <= 0.0f)
        {
            bullet.currentTime = bullet.lifeTime + 1.0f;
            Game.player.Hit();
        }
    }
    public override void _Draw()
    {
        base._Draw();
        for(int i = 0; i < multimesh.InstanceCount; i++)
        {
            Bullet bullet = bullets[i];
            Transform2D transform = new Transform2D(bullet.rotation,bullet.position);
            multimesh.SetInstanceTransform2d(i,transform);
        }
    }
    public Bullet AddBullet(Vector2 velocity, Vector2 position)
    {
        multimesh.InstanceCount += 1;
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