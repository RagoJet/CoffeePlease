using Services;
using UnityEngine;

namespace Units{
    [RequireComponent(typeof(CharacterController), typeof(Rigidbody), typeof(Collider))]
    public class Player : Unit{
        private IInputSystem _inputSystem;
        private Rigidbody _rb;
        private CharacterController controller;
        private float _speed = 4;

        private bool moving;

        protected override void Awake(){
            base.Awake();
            controller = GetComponent<CharacterController>();
            _rb = GetComponent<Rigidbody>();
            _inputSystem = AllServices.Instance.Get<IInputSystem>();
        }

        void Update(){
            if (_inputSystem.IsUse()){
                transform.forward = _inputSystem.GetDirection();
                controller.Move(transform.forward * _speed * Time.deltaTime);
                if (moving == false){
                    PlayMoveAnim();
                    moving = true;
                }
            }
            else{
                if (moving){
                    PlayIdleAnim();
                    moving = false;
                }
            }
        }

        private void OnTriggerEnter(Collider other){
            if (other.TryGetComponent(out CoffeeMachine coffeeMachine)){
                if (_dirtyDishes.Count > 0){
                    return;
                }

                ReceiveCoffee(coffeeMachine.GivebackListCoffee(capacity - _coffees.Count));
            }

            if (other.TryGetComponent(out CashMachine cashMachine)){
                if (_dirtyDishes.Count > 0){
                    return;
                }

                foreach (var coffee in _coffees){
                    coffee.transform.parent = null;
                }

                cashMachine.ReceiveCoffeesFromWorker(_coffees);

                _coffees.Clear();
            }

            if (other.TryGetComponent(out Table table)){
                if (_coffees.Count > 0){
                    return;
                }

                ReceiveDirtyDishes(table.GivebackListDirtyDishes(capacity - _dirtyDishes.Count));
            }

            if (other.TryGetComponent(out DishWasher dishWasher)){
                if (_dirtyDishes.Count > 0){
                    foreach (var dish in _dirtyDishes){
                        _dirtyDishes.Remove(dish);
                        Destroy(dish.gameObject);
                    }
                }
            }
        }
    }
}