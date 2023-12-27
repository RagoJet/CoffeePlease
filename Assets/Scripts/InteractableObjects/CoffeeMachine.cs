using System.Collections.Generic;
using Services;
using UnityEngine;

public class CoffeeMachine : MonoBehaviour{
    [SerializeField] private Transform portObjTrans;
    private List<Coffee> _coffees = new List<Coffee>();

    private float _time;

    public List<Coffee> GivebackListCoffee(int amount){
        amount = Mathf.Clamp(amount, 0, _coffees.Count);
        var result = _coffees.GetRange(_coffees.Count - amount, amount);
        _coffees.RemoveRange(_coffees.Count - amount, amount);
        return result;
    }

    private void Awake(){
        for (int i = 0; i < 5; i++){
            var coffee = AllServices.Instance.Get<IGameObjectsFactory>()
                .CreateCoffee(portObjTrans.position + new Vector3(0, 0.2f * _coffees.Count, 0));
            _coffees.Add(coffee);
        }
    }

    private void Update(){
        _time += Time.deltaTime;

        if (_coffees.Count <= 10 && _time >= 2.7f){
            var coffee = AllServices.Instance.Get<IGameObjectsFactory>()
                .CreateCoffee(portObjTrans.position + new Vector3(0, 0.2f * _coffees.Count, 0));
            _coffees.Add(coffee);
            _time = 0;
        }
    }
}