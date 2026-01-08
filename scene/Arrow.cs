using Godot;
using System;

public partial class Arrow : CharacterBody2D
{
    public const float Speed = 300.0f;
    public  CharacterBody2D player;
    //public const string PlayerPos = "res://scene/player.tscn";
    Vector2 _position;
    Vector2 _idk;
    
    public override void _Ready()
    {
        //_player = GetNode<CharacterBody2D>("res://scene/player.tscn");
        
    }
    public override void _PhysicsProcess(double delta)
    {
        _idk = player.GlobalPosition;
        _position = (_idk-GlobalPosition).Normalized();
        Velocity = _position * Speed;
        MoveAndSlide();

    }


}
