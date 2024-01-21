using Godot;
using System;

/**
 * 这个相机用于跟随角色移动
 */
public partial class FollowCamera : Camera2D
{
	[Export] private TileMap _tileMap;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var mapRect = _tileMap.GetUsedRect();
		var tileSize = _tileMap.CellQuadrantSize;
		var worldSizeInPixels = mapRect.Size * tileSize;
		LimitRight = worldSizeInPixels.X;
		LimitBottom = worldSizeInPixels.Y;
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
