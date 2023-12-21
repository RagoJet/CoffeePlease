using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public class Unit : MonoBehaviour{
    [SerializeField] protected Transform carryTrans;
    protected int capacity = 2;
    protected List<Coffee> _coffees = new List<Coffee>();
    protected List<DirtyDish> _dirtyDishes = new List<DirtyDish>();

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
        if (_coffees.Count > 0 || _dirtyDishes.Count > 0){
            WalkCarryAnim();
        }
        else{
            WalkAnim();
        }
    }

    public void PlayIdleAnim(){
        if (_coffees.Count > 0 || _dirtyDishes.Count > 0){
            CarryAnim();
        }
        else{
            IdleAnim();
        }
    }

    public void GiveDirtyDishes(DishWasher dishWasher){
    }

    public void GiveCoffee(CashMachine cashMachine){
    }

    public void ReceiveCoffee(List<Coffee> newListOfCoffee){
        foreach (var coffee in newListOfCoffee){
            _coffees.Add(coffee);
            CarryPorObj();
        }
    }

    public void ReceiveDirtyDishes(List<DirtyDish> newListOfDirtyDishes){
        foreach (var dirtyDish in newListOfDirtyDishes){
            _dirtyDishes.Add(dirtyDish);
            CarryPorObj();
        }
    }

    public void CarryPorObj(){
        if (_coffees.Count != 0){
            for (int i = 0; i < _coffees.Count; i++){
                _coffees[i].transform.position = carryTrans.position + new Vector3(0, i * 0.3f, 0);
                _coffees[i].transform.parent = this.transform;
            }

            return;
        }

        if (_dirtyDishes.Count != 0){
            for (int i = 0; i < _dirtyDishes.Count; i++){
                _dirtyDishes[i].transform.position = carryTrans.position + new Vector3(0, i * 0.3f, 0);
                _dirtyDishes[i].transform.parent = this.transform;
            }

            return;
        }
    }
}