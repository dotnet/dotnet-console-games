﻿namespace Shmup.Enemies;

internal interface IEnemy
{
	public void Shot();

	public void Render();

	public void Update();

	public bool CollidingWith(int x, int y);

	public bool IsOutOfBounds();
}
