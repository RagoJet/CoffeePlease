using System.Collections.Generic;
using UnityEngine;

public class CashMachine : MonoBehaviour{
    [SerializeField] private Transform portObjTrans;
    private List<Client> _clients = new List<Client>();
    private List<Coffee> _coffees = new List<Coffee>();

    [SerializeField] private Transform transForClient;
    [SerializeField] private Transform transForWorker;

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

    public void GetCoffeesFromWorker(List<Coffee> list){
        foreach (var coffee in list){
            _coffees.Add(coffee);
            coffee.transform.position = portObjTrans.position + new Vector3(0, _coffees.Count * 0.2f, 0);
        }
    }

    public Vector3 GetPositionForClient(){
        return transForClient.position;
    }

    public Vector3 GetPositionForWorker(){
        return transForWorker.position;
    }
}