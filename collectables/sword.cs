using Godot;
using System;

public partial class sword : collectable
{
	private AnimationPlayer _animations;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_animations = GetNode<AnimationPlayer>("AnimationPlayer");
		_animations.AnimationFinished += OnAnimationFinished;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//_animations.Play("spin");
	}

	// 重写方法来实现播放动画
	public override void Collect()
	{
		_animations.Play("spin");
	}

	// 动画结束之后，使用base相当于super，调用父类的方法
	private void OnAnimationFinished(StringName animName)
	{
		if (animName.Equals("spin"))
		{
			
			base.Collect();
		}
	}
}
