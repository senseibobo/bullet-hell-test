using Godot;
using System;

public class TestEnemy : Enemy
{
    public int phase = 1;
	public override void Hit()
	{
		base.Hit();
        if(health < 900.0f && phase == 1) {
            phase = 2;
            SetSineBullets();
            GD.Print("Phase 2");
        }
	}
    public void SetSineBullets()
    {
        CircleGenerator gen = GetNode<CircleGenerator>("CircleGenerator");
        gen.BulletType = (CSharpScript)GD.Load("res://bullets/types/SineBullet.cs");
        gen.AdditionalArgs = new float[] {50.0f,1.0f,0.0f,1.0f};
    }
}
