using Godot;
using System;

public partial class Player : CharacterBody2D
{
    public const float Speed = 150.0f;
    public AnimatedSprite2D _animation;
    
    private Vector2 mouse_click_position; //hol van az eger amikor kattintok
    private Vector2 character_move; // hova menjen a karakterem miutan kattintok
    


    public override void _Ready()
    {
        mouse_click_position = Position;
        _animation = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        _animation.Play("default");
        _animation.AnimationFinished += AnimationFinished; // utolso frame MEGJELENITESE UTAN ide kuld jelet hogy vege ezaltal lefut az animationfinished
        AddToGroup("player");    
    }


    public override void _PhysicsProcess(double delta) // inkabb a szamolas, allapot es a pl irany
    {
        if (_animation.Animation == "attack" || _animation.Animation == "flash_start_animation" || _animation.Animation == "flash_end_animation")
        {
            Velocity = Vector2.Zero; // x semmi 0 sebesseggel es ugyan ez y-al
            MoveAndSlide();
            return; // megszakitja az egeszet
        }
        if (Input.IsActionJustPressed("right_click"))
        {
            mouse_click_position = GetGlobalMousePosition(); // elmetni az eger helyet
        }


        if (Position.DistanceTo(mouse_click_position) > 3)
        {
            character_move = (mouse_click_position - Position).Normalized(); // irany kiszamitasa
            Velocity = character_move * Speed;
            MoveAndSlide();
            if (Input.IsActionJustPressed("s_stop"))
            {
                mouse_click_position = Position;
            }
        }
        

    }
    public override void _Process(double delta) // animacio vfx stb
    {

        var direction = GetGlobalMousePosition() - GlobalPosition; // megnezi hogy merre van az eger a karakterhez kepest
        if (Input.IsActionJustPressed("right_click"))
        {
            if (direction.X > 0) _animation.FlipH = false;
            else _animation.FlipH = true;
        }
        else if (Input.IsActionJustPressed("q_attack"))
        {
            
            _animation.Play("attack"); // eleri az utolso framet
            
        }
        else if (Input.IsActionJustPressed("flash_animation"))
        {
            
            _animation.Play("flash_start_animation"); // eleri az utolso framet
        }
    }
    private void AnimationFinished()
    {
        if (_animation.Animation == "attack") //meg az utolso framenel van nem valtott at semmire azaz if = true
        {

            _animation.Play("default"); //mivel true lefuttatja az animaciot
        }
        else if (_animation.Animation == "flash_start_animation") 
        {                       
            Position = mouse_click_position;
            
            // position a karakter pozicioja
            _animation.Play("flash_end_animation"); 
        }
        else if (_animation.Animation == "flash_end_animation") 
        {
            _animation.Play("default");
            

        }


    }
}
