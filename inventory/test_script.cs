using Godot;
using System;

public partial class test_script : Resource
{
	[Export] public string Name { get; set; }
	[Export] public Texture2D Texture { get; set; }
}
