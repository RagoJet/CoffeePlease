using Services;
using UnityEngine;

public class Shop : MonoBehaviour{
    [SerializeField] Cafe cafe;
    [SerializeField] CoffeeMachine coffeeMachine;
    [SerializeField] CashMachine cashMachine;
    [SerializeField] DishWasher dishWasher;
    [SerializeField] private FillImage fillImage;

    private int _priorityOfWorker;

    private void Awake(){
        fillImage.OnFilled += CreateWorker;
    }

    public void CreateWorker(){
        _priorityOfWorker++;
        if (_priorityOfWorker == 99){
            _priorityOfWorker = 1;
        }

        AllServices.Instance.Get<IGameObjectsFactory>().CreateWorker(fillImage.transform.position)
            .Construct(cafe, coffeeMachine, cashMachine, dishWasher, _priorityOfWorker);
        fillImage.Refresh();
    }
}