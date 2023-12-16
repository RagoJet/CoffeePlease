using UnityEngine;

public enum WorkerState{
    Nothing,
    Carry
}

[RequireComponent(typeof(Animator))]
public class Unit : MonoBehaviour{
    private WorkerState _state = WorkerState.Nothing;
    protected int capacity = 2;
    protected Coffee[] coffees;

    private Animator _animator;
    private readonly string _sitAnimation = "Sitting Idle";
    private readonly string _idleAnimation = "Idle";
    private readonly string _carryingAnimation = "Carrying";
    private readonly string _walkingCarryAnimation = "WalkingCarry";
    private readonly string _walkingAnimation = "Walking";

    protected virtual void Awake(){
        _animator = GetComponent<Animator>();
    }

    private void IdleAnim(){
        _animator.CrossFade(_idleAnimation, 0.15F);
    }

    private void WalkAnim(){
        _animator.CrossFade(_walkingAnimation, 0.15F);
    }

    public void SitAnim(){
        _animator.CrossFade(_sitAnimation, 0.15F);
    }

    private void CarryAnim(){
        _animator.CrossFade(_carryingAnimation, 0.15F);
    }

    private void WalkCarryAnim(){
        _animator.CrossFade(_walkingCarryAnimation, 0.15F);
    }

    public void PlayMoveAnim(){
        switch (_state){
            case WorkerState.Nothing:
                WalkAnim();
                break;
            case WorkerState.Carry:
                WalkCarryAnim();
                break;
        }
    }

    public void PlayIdleAnim(){
        switch (_state){
            case WorkerState.Nothing:
                IdleAnim();
                break;
            case WorkerState.Carry:
                CarryAnim();
                break;
        }
    }
}