using Services;
using UnityEngine;

namespace Units{
    [RequireComponent(typeof(CharacterController), typeof(Rigidbody), typeof(Collider))]
    public class Player : Worker{
        private IInputSystem _inputSystem;
        private Rigidbody _rb;
        private CharacterController controller;
        private float _speed = 5;

        protected override void Awake(){
            base.Awake();
            controller = GetComponent<CharacterController>();
            _rb = GetComponent<Rigidbody>();
            _inputSystem = AllServices.Instance.GetService<IInputSystem>();
        }


        void Update(){
            if (_inputSystem.IsUse()){
                transform.forward = _inputSystem.GetDirection();
                controller.Move(transform.forward * _speed * Time.deltaTime);
            }
        }
    }
}