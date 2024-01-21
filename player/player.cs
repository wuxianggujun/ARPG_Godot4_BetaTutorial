using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class player : CharacterBody2D
{
	// 用来更新生命值的信号
	[Signal]
	public delegate void HealthChangedEventHandler(int currentHealth);

	[Export] private int _speed = 35;
	[Export] private AnimationPlayer _animations;
	private AnimationPlayer _effects;
	private Area2D _hurtBox;
	private Timer _hurtTimer;
	[Export] public int MaxHealth { get; set; } = 5;
	public int CurrentHealth;
	[Export] private int KnockbackPower { get; set; } = 500;

	private bool _isHurt = false;

	public override void _Ready()
	{
		CurrentHealth = MaxHealth;
		_animations = GetNode<AnimationPlayer>("AnimationPlayer");
		_effects = GetNode<AnimationPlayer>("Effects");
		_hurtBox = GetNode<Area2D>("HurtBox");
		_hurtTimer = GetNode<Timer>("hurtTimer");
		_hurtTimer.WaitTime = 0.2f;
		_hurtTimer.Timeout += OnHurtTimerTimeout;
		_effects.Play("RESET");
	}


	private void HandleInput()
	{
		var moveDirection = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		Velocity = moveDirection * _speed;
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
			var direction = "Down";
			switch (Velocity.X)
			{
				case < 0:
					direction = "Left";
					break;
				case > 0:
					direction = "Right";
					break;
				default:
				{
					if (Velocity.Y < 0) direction = "Up";
					break;
				}
			}

			_animations.Play("walk" + direction);
		}
	}

	// 处理玩家和史莱姆之间的碰撞
	private void HandleCollision()
	{
		for (var i = 0; i < GetSlideCollisionCount(); i++)
		{
			var collision = GetSlideCollision(i);
			Node collider = (Node)collision.GetCollider();
			GD.Print("I collided with ", collider.Name);
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		HandleInput();
		MoveAndSlide();
		HandleCollision();
		UpdateAnimation();
		if (!_isHurt)
		{
			foreach (var area in _hurtBox.GetOverlappingAreas())
			{
				if (area.Name == "HitBox")
				{
					hurtByEnemy(area);
				}
			}
		}
	}

	private void hurtByEnemy(Area2D area)
	{
		var parent = area.GetParent();
		CurrentHealth -= 1;
		if (CurrentHealth < 0)
		{
			CurrentHealth = MaxHealth;
		}

		EmitSignal(SignalName.HealthChanged, CurrentHealth);
		_isHurt = true;
		if (parent is CharacterBody2D characterBody2D)
		{
			GD.Print(" Velocity ： " + characterBody2D.Velocity);
			Knockback(characterBody2D.Velocity);
			_effects.Play("hurtBlink");
			_hurtTimer.Start();
			_isHurt = false;
		}
	}


	private void _on_hurt_box_area_entered(Area2D area)
	{
		if (area is collectable collectable)
		{
			/*if (area.HasMethod("Collect"))
			{*/
				collectable.Collect();
			// }
		}
	}

	private void Knockback(Vector2 enemyVelocity)
	{
		var knockbackDirection = (enemyVelocity - Velocity).Normalized() * KnockbackPower;
		GD.Print(Velocity);
		GD.Print(Position);
		Velocity = knockbackDirection;
		GD.Print(Position);
		GD.Print(" ");
		MoveAndSlide();
	}

	private void OnHurtTimerTimeout()
	{
		_effects.Play("RESET");
	}

	private void _on_hurt_box_area_exited(Area2D area)
	{
	}
}
