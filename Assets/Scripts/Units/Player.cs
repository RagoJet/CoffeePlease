using System;
using Services;
using UnityEngine;

namespace Units{
    [RequireComponent(typeof(CharacterController), typeof(Rigidbody), typeof(Collider))]
    public class Player : Unit{
        private int _amountOfMoney = 300;
        public int AmountOfMoney => _amountOfMoney;

        public event Action OnChangedMoney;

        [SerializeField] private Transform body;
        private IInputSystem _inputSystem;
        private Rigidbody _rb;
        private CharacterController controller;
        private float _speed = 4;

        private bool _isMoving;

        public Transform Body => body;

        private float _timeInFillImage;

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
                if (_isMoving == false){
                    PlayMoveAnim();
                    _isMoving = true;
                }
            }
            else{
                if (_isMoving){
                    PlayIdleAnim();
                    _isMoving = false;
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
                    float duration = 0.1f;
                    foreach (var dish in _dirtyDishes){
                        dish.transform.parent = dishWasher.transform;
                        dish.LocalMoveTo(dishWasher.LocalPosForPortObj, duration, () => Destroy(dish.gameObject));
                        duration += 0.3f;
                    }

                    _dirtyDishes.Clear();
                }

                return;
            }

            if (other.TryGetComponent(out StackOfMoney stackOfMoney)){
                stackOfMoney.GiveMoney(this);
                return;
            }

            if (other.TryGetComponent(out FillImage fillImage)){
                _timeInFillImage = 0;
            }

            ReAnim();
        }

        private void OnTriggerStay(Collider other){
            if (other.TryGetComponent(out FillImage fillImage)){
                _timeInFillImage += Time.deltaTime;
                if (_timeInFillImage >= 0.2f){
                    fillImage.GetMoney(this);
                    _timeInFillImage = 0;
                }
            }
        }

        public void AddMoney(int money){
            _amountOfMoney += money;
            OnChangedMoney.Invoke();
        }

        public bool TryGiveMoney(int money){
            if (_amountOfMoney >= money){
                _amountOfMoney -= money;
                OnChangedMoney.Invoke();
                return true;
            }
            else return false;
        }

        private void ReAnim(){
            if (_isMoving){
                PlayMoveAnim();
            }
            else
                PlayIdleAnim();
        }
    }
}