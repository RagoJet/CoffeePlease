using System.Collections.Generic;
using Services;
using UnityEngine;

public class CoffeeMachine : MonoBehaviour{
    [SerializeField] private Transform portObjTrans;
    private List<Coffee> _coffees = new List<Coffee>();

    public List<Coffee> GetListCoffee(int amount){
        List<Coffee> newListOfCoffees = new List<Coffee>();

        for (int i = 0; i < amount; i++){
            if (_coffees.Count > 0){
                newListOfCoffees.Add(_coffees[_coffees.Count - 1]);
                _coffees.Remove(_coffees[_coffees.Count - 1]);
            }
            else{
                break;
            }
        }

        return newListOfCoffees;
    }

    private void Awake(){
        for (int i = 0; i < 5; i++){
            var coffee = AllServices.Instance.GetService<IPortableObjectsFactory>()
                .CreateCoffee(portObjTrans.position + new Vector3(0, 0.2f * _coffees.Count, 0));
            _coffees.Add(coffee);
        }
    }
}