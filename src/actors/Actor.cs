using Godot;
using System;

public class Actor : KinematicBody2D
{
    public Vector2 FLOOR_NORMAL = Vector2.Up;

    [Export]
    public Vector2 Speed = new Vector2(500, 500);
    
    [Export]
    public float Gravity = 6000.0F;


    public Vector2 Velocity = new Vector2();

    public override void _PhysicsProcess(float delta)
    {

        //base._PhysicsProcess(delta);
    }
}