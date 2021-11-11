using Godot;
using System;

public class SquareGenerator : Generator
{
    [Export] protected int bulletsPerSide;
	protected override void Shoot()
    {
        GD.Print(bullets.Count);
		GD.Print(Engine.GetFramesPerSecond());
        base.Shoot();
        for(int i = 0; i < bulletsPerSide; i++)
        {
			for(int j = 0; j < 4; j++) {
				Vector2 vec = new Vector2();
				switch(j) {
					case 0: {vec.y = -1; vec.x = ((float)i/(bulletsPerSide - 1) - 0.5f)*2.0f; break;}
					case 1: {vec.y = 1; vec.x = ((float)i/(bulletsPerSide - 1) - 0.5f)*2.0f; break;}
					case 2: {vec.x = 1; vec.y = ((float)i/(bulletsPerSide - 1) - 0.5f)*2.0f; break;}
					case 3: {vec.x = -1; vec.y = ((float)i/(bulletsPerSide - 1) - 0.5f)*2.0f; break;}
				}
				Vector2 velocity = vec * speed;
				AddBullet(velocity, GlobalPosition);
			}
        }
    }
}