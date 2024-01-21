using Godot;
using System;

public partial class inventory_gui : Control
{
	[Signal]
	public delegate void OpenedEventHandler();

	[Signal]
	public delegate void ClosedEventHandler();


	public bool IsOpen = false;

	public void Open()
	{
		Visible = true;
		IsOpen = true;
		EmitSignal(SignalName.Opened);
	}


	public void Close()
	{
		Visible = false;
		IsOpen = false;
		EmitSignal(SignalName.Closed);
	}
}
