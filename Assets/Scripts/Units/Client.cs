using System.Collections;
using Services;
using UnityEngine;
using UnityEngine.AI;

public class Client : Unit{
    private NavMeshAgent _agent;

    private CashMachine _cashMachine;
    private Cafe _cafe;
    private Vector3 _posDestination;
    private Chair _targetChair;

    private bool _onPositionQueue;

    public void Construct(CashMachine cashMachine, Cafe cafe){
        capacity = 1;
        _cashMachine = cashMachine;
        _cafe = cafe;
        _agent = GetComponent<NavMeshAgent>();
        _cashMachine.TakeClient(this);
    }


    public void GoInQueue(Vector3 pos){
        _onPositionQueue = false;
        _agent.isStopped = false;
        _posDestination = pos;
        float distance = Vector3.Distance(transform.position, _posDestination);
        if (distance >= _agent.stoppingDistance){
            _agent.SetDestination(_posDestination);
            PlayMoveAnim();
        }
    }

    private void Update(){
        if (_onPositionQueue == false){
            float distance = Vector3.Distance(transform.position, _posDestination);
            if (distance <= _agent.stoppingDistance){
                _onPositionQueue = true;
                _agent.isStopped = true;
                PlayIdleAnim();
                TryGetOrder();
            }
        }
        else if (_agent.isStopped == false && _targetChair != null){
            float distance = Vector3.Distance(transform.position, _targetChair.transform.position);
            if (distance <= _agent.stoppingDistance){
                _agent.isStopped = true;

                transform.position = _targetChair.transform.position;
                Table table = _targetChair.table;
                transform.forward = (table.transform.position - transform.position).normalized;


                foreach (var coffee in _coffees){
                    coffee.transform.parent = null;
                    coffee.transform.position = _targetChair.PosForPortableObj;
                }

                table.ReceiveCoffees(_coffees);
                SitAnim();
                StartCoroutine(DrinkingCoffee());
            }
        }
    }

    public void TryGetOrder(){
        if (_onPositionQueue){
            _cashMachine.TryGiveOrder(this, capacity - _coffees.Count);
            if (_coffees.Count == capacity && _cafe.TryGetAvailableTable(out Table table)){
                _targetChair = table.GetChair();
                _cashMachine.RemoveClient(this);
                _agent.isStopped = false;
                _agent.SetDestination(_targetChair.transform.position);
                PlayMoveAnim();
            }
        }
    }

    IEnumerator DrinkingCoffee(){
        Coffee coffee = _coffees[0];
        yield return new WaitForSeconds(7f);
        DirtyDish dirtyDish = AllServices.Instance.Get<IGameObjectsFactory>()
            .CreateDirtyDish(coffee.transform.position);
        _targetChair.table.AddDirtyDish(dirtyDish);
        _targetChair.table.RemoveCoffeeFromList(coffee);
        _coffees.Remove(coffee);
        dirtyDish.chair = _targetChair;
        Destroy(coffee.gameObject);
        Destroy(this.gameObject);
    }
}