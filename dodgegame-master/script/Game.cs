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
	//private Timer ArrowSpawn;
	//private Timer Acceleration;
	private double cooldown = 5.0;       // Másodpercenként lő alapból
    private double acceleration = 30;   // 5 másodpercenként gyorsul
    private double spawnInterval = 5.0;
    private Godot.Label _label;
    private double time = 0.0;
    public override void _Ready()
	{
        //ArrowSpawn = GetNode<Timer>("ArrowSpawn");
        //Acceleration = GetNode<Timer>("Acceleration");
        _label = GetNode<Godot.Label>("Label");
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

	public override void _PhysicsProcess(double delta)
	{

		cooldown -= delta;
		acceleration -= delta;
		if(acceleration <= 0) 
		{
		spawnInterval =- 0.2;
		if(spawnInterval<=0.3) spawnInterval = 0.3;
		acceleration = 5.0;
		GD.Print($"Gyorsulás! Új köz: {spawnInterval}");
		}
		if(cooldown<=0)
		{
			_ArrowSpawn();
			cooldown = spawnInterval;
		}
		time += delta;
        _label.Text = time.ToString("F0");
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
	public void timer()
	{
		GD.Print("vege");
		
	}
	
}
