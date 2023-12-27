using Services;
using UnityEngine;

public class ClientCreator : MonoBehaviour{
    [SerializeField] CashMachine cashMachine;
    [SerializeField] Cafe cafe;
    private float _time;

    private int _priorityOfWorker;

    private void Update(){
        _time += Time.deltaTime;
        if (_time >= 2.5f){
            if (cashMachine.IsFreeSpace){
                _priorityOfWorker++;
                if (_priorityOfWorker == 90){
                    _priorityOfWorker = 1;
                }

                AllServices.Instance.Get<IGameObjectsFactory>().CreateClient(transform.position)
                    .Construct(cashMachine, cafe, _priorityOfWorker);
                _time = 0;
            }
        }
    }
}