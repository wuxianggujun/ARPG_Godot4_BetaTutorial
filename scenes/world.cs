using Godot;
using System;

public partial class world : Node2D
{
	private heartsContainer _heartsContainer;

	private player _player;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_player = GetNode<player>("TileMap/Player");
		_heartsContainer = GetNode<heartsContainer>("CanvasLayer/HeartsContainer");
		_heartsContainer.SetMaxHearts(_player.MaxHealth);
		_heartsContainer.UpdateHearts(_player.CurrentHealth);
		_player.HealthChanged += _heartsContainer.UpdateHearts;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	
	
	private void _on_inventory_gui_closed()
	{
		GetTree().Paused = false;
	}


	private void _on_inventory_gui_opened()
	{
		GetTree().Paused = true;
	}

}


