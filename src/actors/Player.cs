using Godot;
using System;

public class Player : Actor
{
    private int jumpCount = 0;
    public int MaxJumpCount = 2;

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

        //newVelocity.y = (velocity.y > speed.y) ? speed.y : velocity.y;


        return @out;
    }
}
