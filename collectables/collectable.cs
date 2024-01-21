using Godot;
using System;

public partial class collectable : Area2D
{
	
	public virtual void Collect()
	{
		QueueFree();
	}
	
}
