using Godot;
using System;

public partial class heartgui : Panel
{
	private Sprite2D _sprite2D;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_sprite2D = GetNode<Sprite2D>("Sprite2D");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Update(bool whole)
	{
		_sprite2D.Frame = whole ? 0 : 4;
	}
}
