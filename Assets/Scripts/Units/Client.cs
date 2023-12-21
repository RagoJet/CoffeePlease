using UnityEngine;
using UnityEngine.AI;

public class Client : Unit{
    private NavMeshAgent _agent;

    private CashMachine _cashMachine;
    private Cafe _cafe;
    private Vector3 _posDestination;

    private bool _onPosition;

    public void Construct(CashMachine cashMachine, Cafe cafe){
        base.Awake();
        capacity = 1;
        _cashMachine = cashMachine;
        _cafe = cafe;
        _agent = GetComponent<NavMeshAgent>();
        _cashMachine.TakeClient(this);
    }


    public void GoInQueue(Vector3 pos){
        _onPosition = false;
        _agent.isStopped = false;
        _posDestination = pos;
        float distance = Vector3.Distance(transform.position, _posDestination);
        if (distance >= _agent.stoppingDistance){
            _agent.SetDestination(_posDestination);
            PlayMoveAnim();
        }
    }

    private void Update(){
        if (_agent.isStopped == false){
            float distance = Vector3.Distance(transform.position, _posDestination);
            if (distance <= _agent.stoppingDistance){
                _onPosition = true;
                _agent.isStopped = true;
                PlayIdleAnim();
                TryGetOrder();
            }
        }
    }

    public void TryGetOrder(){
        if (_onPosition){
            _cashMachine.TryGiveOrder(this, capacity - _coffees.Count);
            if (_coffees.Count == capacity){
                _cashMachine.RemoveClient(this);
                _agent.isStopped = false;
                _agent.SetDestination(_cafe.transform.position);
            }
        }
    }
}