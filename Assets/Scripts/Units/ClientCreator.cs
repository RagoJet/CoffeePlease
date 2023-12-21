using Services;
using UnityEngine;

public class ClientCreator : MonoBehaviour{
    [SerializeField] CashMachine cashMachine;
    [SerializeField] Cafe cafe;
    private float _time;

    private void Update(){
        _time += Time.deltaTime;
        if (_time >= 3){
            if (cashMachine.IsFreeSpace){
                AllServices.Instance.Get<IGameObjectsFactory>().CreateClient(transform.position)
                    .Construct(cashMachine, cafe);
                _time = 0;
            }
        }
    }
}