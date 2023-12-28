using UnityEngine;
using UnityEngine.AI;

public class Worker : Unit{
    private Cafe _cafe;
    private CoffeeMachine _coffeeMachine;
    private CashMachine _cashMachine;
    private DishWasher _dishWasher;

    private NavMeshAgent _agent;

    private float _currentTimeOfAlive = 0;
    [SerializeField] int timeOfLife = 60;

    private Transform _target;

    public void Construct(Cafe cafe, CoffeeMachine coffeeMachine, CashMachine cashMachine, DishWasher dishWasher,
        int priority){
        _agent = GetComponent<NavMeshAgent>();
        _cafe = cafe;
        _coffeeMachine = coffeeMachine;
        _cashMachine = cashMachine;
        _dishWasher = dishWasher;
        FindWork();
        _agent.avoidancePriority = priority;
    }

    private void Update(){
        _currentTimeOfAlive += Time.deltaTime;
        if (_currentTimeOfAlive >= timeOfLife){
            Destroy(gameObject);
        }

        float distance = Vector3.Distance(transform.position, _agent.destination);
        if (distance <= _agent.stoppingDistance){
            if (_target.TryGetComponent(out Table table)){
                ReceiveDirtyDishes(table.GivebackListDirtyDishes(capacity - _dirtyDishes.Count));
                if (_dirtyDishes.Count > 0){
                    _target = _dishWasher.transform;
                    GoToTarget();
                }
                else FindWork();
            }

            else if (_target.TryGetComponent(out CoffeeMachine coffeeMachine)){
                ReceiveCoffee(coffeeMachine.GivebackListCoffee(capacity - _coffees.Count));
                if (_coffees.Count > 0){
                    _target = _cashMachine.transform;
                    GoToTarget();
                }
                else FindWork();
            }
            else if (_target.TryGetComponent(out CashMachine cashMachine)){
                foreach (var coffee in _coffees){
                    coffee.transform.parent = null;
                }

                cashMachine.ReceiveCoffeesFromWorker(_coffees);

                _coffees.Clear();
                FindWork();
            }
            else if (_target.TryGetComponent(out DishWasher dishWasher)){
                float duration = 0.1f;
                foreach (var dish in _dirtyDishes){
                    dish.transform.parent = dishWasher.transform;
                    dish.MoveTo(dishWasher.LocalPosForPortObj, duration, () => Destroy(dish.gameObject));
                    duration += 0.3f;
                }

                _dirtyDishes.Clear();

                FindWork();
            }
        }
    }

    private void FindWork(){
        if (_cafe.TryGetDirtyTable(out Table table)){
            _target = table.transform;
            PlayMoveAnim();
            GoToTarget();
        }
        else{
            if (_target == _coffeeMachine.transform){
                PlayIdleAnim();
            }
            else{
                _target = _coffeeMachine.transform;
                PlayMoveAnim();
                GoToTarget();
            }
        }
    }

    private void GoToTarget(){
        PlayMoveAnim();
        _agent.SetDestination(_target.position);
    }
}