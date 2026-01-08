using Godot;
using System;

public partial class Game : Node2D
{
    public const string ArrowPath = "res://scene/Arrow.tscn";
	public Vector2 ArrowPos;
	public Camera2D _Camera;
	public Vector2 _CameraPos;
	public Vector2 ViewSize;
	private PackedScene _arrowScene;

    public override void _Ready()
	{
		_Camera = GetNode<Camera2D>("Camera2D");
        ViewSize = GetViewport().GetVisibleRect().Size / 2;
		_CameraPos = _Camera.GlobalPosition;
        _arrowScene = GD.Load<PackedScene>(ArrowPath);
        
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        if (Input.IsActionJustPressed("ui_accept"))
            _ArrowSpawn();
		
    }


    public void _ArrowSpawn()
	{
        
        var ArrowSpawn = _arrowScene.Instantiate<CharacterBody2D>();
        int side = GD.RandRange(0, 3);
		ArrowSpawn.GlobalPosition = side switch
		{
			0 => _CameraPos + new Vector2(-ViewSize.X, 0),
			1 => _CameraPos + new Vector2(+ViewSize.X, 0),
			2 => _CameraPos + new Vector2(0, -ViewSize.Y),
			_ => _CameraPos + new Vector2(0, +ViewSize.Y)
		};
        AddChild(ArrowSpawn);
        // irany kiszamitasa
        
        
        GD.Print($"Spawned arrow at {ArrowSpawn.GlobalPosition} side {side}");
    }
}
