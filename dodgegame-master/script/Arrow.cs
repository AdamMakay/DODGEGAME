using Godot;
using System;

public partial class Arrow : CharacterBody2D
{
    public const float Speed = 300.0f;
    public  CharacterBody2D player;
    //public const string PlayerPos = "res://scene/player.tscn";
    Vector2 _position;
    Vector2 _Move;
    public Sprite2D sprite;
    public Vector2 dir;

    public override void _Ready()
    {
        sprite = GetNode<Sprite2D>("Sprite2D");
        //_player = GetNode<CharacterBody2D>("res://scene/player.tscn");
        player = GetTree().GetFirstNodeInGroup("player") as CharacterBody2D;
        _Move = player.GlobalPosition;
        _position = (_Move-GlobalPosition).Normalized();
        sprite.Rotation = _position.Angle() + Mathf.DegToRad(-90);

        /*if (_position.X < 0)
            sprite.Rotation = Mathf.DegToRad(90);    // balra
        else if (_position.X > 0) sprite.Rotation = Mathf.DegToRad(-90);
        else if((_position.Y < 0)) sprite.Rotation = Mathf.DegToRad(180);
        */
    }
    public override void _PhysicsProcess(double delta)
    {
        if (player == null) return;

        
        
        Velocity = _position * Speed;
        MoveAndSlide();

        for (int i = 0; i < GetSlideCollisionCount(); i++)
        {
            KinematicCollision2D collision = GetSlideCollision(i);
            Node collider = (Node)collision.GetCollider();

            // Ha a játékost találtuk el (aki a "player" csoportban van)
            if (collider.IsInGroup("player"))
            {
                GD.Print("TALÁLAT! Game Over!");
                
                // Itt állíthatod le a játékot:
                GetTree().ReloadCurrentScene(); // Újrakezdés
           }
        }
        

    }
}
