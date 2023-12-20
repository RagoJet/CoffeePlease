using System.Collections.Generic;
using Services;
using UnityEngine;

public class CoffeeMachine : MonoBehaviour{
    [SerializeField] private Transform portObjTrans;
    private List<Coffee> _coffees = new List<Coffee>();

    public List<Coffee> GivebackListCoffee(int amount){
        amount = Mathf.Clamp(amount, 0, _coffees.Count);
        var result = _coffees.GetRange(_coffees.Count - amount, amount);
        _coffees.RemoveRange(_coffees.Count - amount, amount);
        return result;
    }

    private void Awake(){
        for (int i = 0; i < 8; i++){
            var coffee = AllServices.Instance.GetService<IGameObjectsFactory>()
                .CreateCoffee(portObjTrans.position + new Vector3(0, 0.2f * _coffees.Count, 0));
            _coffees.Add(coffee);
        }
    }
}