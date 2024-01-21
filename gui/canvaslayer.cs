using Godot;
using System;

public partial class canvaslayer : CanvasLayer
{
	private inventory_gui _inventoryGui;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_inventoryGui = GetNode<inventory_gui>("InventoryGui");
		_inventoryGui.Close();
	}

	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("toggle_inventory"))
		{
			if (_inventoryGui.IsOpen)
			{
				_inventoryGui.Close();
			}
			else
			{
				_inventoryGui.Open();
			}
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
