using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Client : Unit{
    private NavMeshAgent _navMeshAgent;

    private CashMachine _cashMachine;
    private Cafe _cafe;

    protected override void Awake(){
        capacity = 1;
        _cashMachine = FindObjectOfType<CashMachine>();
        _cafe = FindObjectOfType<Cafe>();
        base.Awake();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.SetDestination(_cashMachine.GetPositionForClient());
        PlayMoveAnim();
    }

    private void OnTriggerEnter(Collider other){
        if (other.TryGetComponent(out CashMachine cashMachine)){
            GetCoffee(cashMachine.GetListCoffee(capacity - _coffees.Count));
            if (_cafe.TryGetAvailableTable(out Vector3 pos)){
                {
                    _navMeshAgent.SetDestination(pos);
                    PlayMoveAnim();
                }
            }
        }

        if (other.TryGetComponent(out Table table)){
            Chair chair;
            if (table.TryGetAvailableChair(out chair)){
                _navMeshAgent.isStopped = true;
                foreach (var coffee in _coffees){
                    coffee.transform.parent = null;
                }

                transform.position = chair.transform.position + new Vector3(0, 1, 0);

                table.GetCoffeesFromClient(_coffees);
                _coffees.Clear();

                transform.forward = table.transform.position - transform.position;
                SitAnim();
                StartCoroutine(CycleOfDrinking(table));
            }
        }

        IEnumerator CycleOfDrinking(Table table){
            yield return new WaitForSeconds(5);
            table.CoffeesToDirtyDishes();
            Destroy(gameObject);
        }
    }
}