using Godot;
using System;

public class Bullet : Godot.Object
{
	public Vector2 position;
	public Vector2 velocity;
	public float rotation = 0.0f;
	public float lifeTime = 10.0f;
	public float currentTime = 0.0f;
	public float radiusSquared = 4.0f;
	public Texture texture;
	public Vector2 size = new Vector2(16f, 16f);
	public Vector2 scale = new Vector2(1f, 1f);
	public int frameCount = 1;
	public float frameDuration = 0.1f;
	public int bulletIndex;
	public float[] additionalArgs;

	public Bullet() { }
	public Bullet(float[] additionalArgs) { this.additionalArgs = additionalArgs; }
	public virtual void Process(float delta)
	{
		position += velocity*delta;
	}
	public virtual void Ready()
    {

    }
}
