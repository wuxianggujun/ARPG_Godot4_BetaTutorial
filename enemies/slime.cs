using Godot;
using System;

public partial class slime : CharacterBody2D
{
	[Export] private int _speed = 20;
	[Export] private float _limit = 0.5f;
	[Export] private Marker2D _endPoint;
	[Export] private AnimationPlayer _animations;
	private Vector2 _startPosition;
	private Vector2 _endPosition;

	public override void _Ready()
	{
		_animations = GetNode<AnimationPlayer>("AnimationPlayer");
		_endPoint = GetNode<Marker2D>("Marker2D");
		
		_startPosition = Position;
		_endPosition = _endPoint.GlobalPosition;

	}

	private void ChangeDirection()
	{
		(_endPosition, _startPosition) = (_startPosition, _endPosition);
	}
	private void UpdateVelocity()
	{
		var moveDirection = _endPosition - Position;
		if (moveDirection.Length() < _limit)
		{
			Position = _endPosition;
			ChangeDirection();
		}
		Velocity = moveDirection.Normalized() * _speed;
	}

	private void UpdateAnimation()
	{
		if (Velocity.Length() == 0)
		{
			if (_animations.IsPlaying())
			{
				_animations.Stop();
			}
		}
		else
		{
			string direction = "Down";
			if (Velocity.X < 0) direction = "Left";
			else if (Velocity.X > 0) direction = "Right";
			else if (Velocity.Y < 0) direction = "Up";

			_animations.Play("walk" + direction);
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		UpdateVelocity();
		MoveAndSlide();
		UpdateAnimation();
	}
}
