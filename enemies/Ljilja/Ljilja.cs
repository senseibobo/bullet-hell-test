using Godot;
using System;

public class Ljilja : Enemy
{

    [Export] private float healthForPhase2 = 350.0f;
    [Export] private float healthForPhase3 = 120.0f;
    public override void Hit() 
    {
        base.Hit();
        GD.Print(health);
        switch(phase) {
            case 1: if(health < healthForPhase2) SetPhase(2); break;
            case 2: if(health < healthForPhase3) SetPhase(3); break;
        }
    }
    private void SetPhase(int phase) 
    {
        this.phase = phase;
        AnimationNodeStateMachinePlayback statemachine = (AnimationNodeStateMachinePlayback)GetNode<AnimationTree>("AnimationTree").Get("parameters/playback");
        statemachine.Travel("phase" + phase.ToString());
        GD.Print("Set phase");
    }
}
