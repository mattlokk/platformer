using Godot;
using System;

public class Enemy : Actor
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready(){
        SetPhysicsProcess(false);
        Velocity.x = -Speed.x;
    }

    private void _on_StompDetector_body_entered(PhysicsBody2D body){
        // GD.Print(
        //     string.Format("stomp detected  | {0:#0.000} | {1:#0.000}  ",
        //         body.GlobalPosition.y, 
        //         GetNode<Area2D>("StompDetector").GlobalPosition.y
        //     )
        // );

        if (body.GlobalPosition.y > GetNode<Area2D>("StompDetector").GlobalPosition.y)
            { return; }

        GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred("Disabled", true);        

        QueueFree();
    }

    public override void _PhysicsProcess(float delta){
        Velocity.y = Gravity * delta;
        if (IsOnWall()){
            Velocity.x *= -1;
        }
        Velocity.y = MoveAndSlide(Velocity, FLOOR_NORMAL).y;
    }

}
