using Godot;
using System;

public class Player : Actor
{
    private int jumpCount = 0;
    public int MaxJumpCount = 2;

    [Export]
    public float StompImpulse = 1000.0F;

    private void _on_EnemyDetector_body_entered(PhysicsBody2D body){
        GetTree().ReloadCurrentScene();
    }

    private void _on_EnemyDetector_area_entered(Area2D body){
        
        // GD.Print(
        //     string.Format("enemy detected | {0:#0.000} | {1:#0.000}  ",
        //         body.GlobalPosition.y, 
        //         GetNode<Area2D>("EnemyDetector").GlobalPosition.y
        //     )
        // );

        // if (body.GlobalPosition.y < GetNode<Area2D>("EnemyDetector").GlobalPosition.y)
        //     { return; }
        Velocity = CalculateStompVelocity(Velocity, StompImpulse);        
    }

    public override void _PhysicsProcess(float delta)
    {
        var isJumpInterrupted = Input.IsActionJustReleased("jump") && Velocity.y < 0;
        var direction = GetDirection();
        Velocity = CalculateMoveVelocity(Velocity, direction, Speed, isJumpInterrupted);
        Velocity = MoveAndSlide(Velocity, FLOOR_NORMAL);
    }

    private Vector2 GetDirection(){
        var @out = new Vector2();
        @out.x = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left");
        @out.y = (Input.IsActionJustPressed("jump")) ? -1 : 0;
        return @out;
    }

    private Vector2 CalculateMoveVelocity(Vector2 linearVelocity, Vector2 direction, Vector2 speed, bool isJumpInterrupted){
        
        var @out = new Vector2(linearVelocity);

        @out.x = speed.x * direction.x;
        @out.y += Gravity * GetPhysicsProcessDeltaTime();

        if (direction.y == -1){
            @out.y = speed.y * direction.y;
        }

        if (isJumpInterrupted){
            @out.y = 0;
        }

        ////gravity maximum:
        //newVelocity.y = (velocity.y > speed.y) ? speed.y : velocity.y;
        return @out;
    }

    private Vector2 CalculateStompVelocity(Vector2 linearVelocity, float stompImpulse){
        var @out = new Vector2(linearVelocity);
        @out.y = -stompImpulse;
        return @out;
    }
}
