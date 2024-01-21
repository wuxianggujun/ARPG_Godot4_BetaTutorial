using Godot;
using System;
using System.Collections.Generic;

public partial class heartsContainer : HBoxContainer
{
	private PackedScene _heartGuiClass;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_heartGuiClass = GD.Load<PackedScene>("res://gui/heart_gui.tscn");

		//_heartGuiClass.AddResource("heart","res://gui/heart_gui.tscn");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void SetMaxHearts(int max)
	{
		for (var i = 0; i < max; i++)
		{
			var heart = _heartGuiClass.Instantiate();
			AddChild(heart);
		}
	}

	public void UpdateHearts(int currentHealth)
	{
		var hearts = GetChildren();

		// for i in range(currentHealth)

		int i = 0;

		foreach (var child in hearts)
		{
			if (child is heartgui heartGui)
			{
				// 如果当前索引小于currentHealth，更新为激活状态
				heartGui.Update(i < currentHealth);
				// 如果当前索引大于等于currentHealth，更新为非激活状态
			}

			i++;
		}
	}
}
