using System;
using System.Collections.Generic;
using UnityEngine;

public class CashMachine : MonoBehaviour{
    [SerializeField] private Transform portObjTrans;
    private List<Client> _clients = new List<Client>();
    private List<Coffee> _coffees = new List<Coffee>();

    [SerializeField] private Transform[] transformsForClient;
    [SerializeField] private Transform transForWorker;

    public bool IsFreeSpace => _clients.Count < transformsForClient.Length;

    public List<Coffee> GivebackListCoffee(int amount){
        amount = Mathf.Clamp(amount, 0, _coffees.Count);
        var result = _coffees.GetRange(_coffees.Count - amount, amount);
        _coffees.RemoveRange(_coffees.Count - amount, amount);
        return result;
    }

    public void ReceiveCoffeesFromWorker(List<Coffee> newlistOfCoffee){
        foreach (var coffee in newlistOfCoffee){
            _coffees.Add(coffee);
            coffee.transform.position = portObjTrans.position + new Vector3(0, _coffees.Count * 0.2f, 0);
        }
    }

    public void TakeClient(Client client){
        _clients.Add(client);
        SortClientsInQueue();
    }

    public void SortClientsInQueue(){
        for (int i = 0; i < _clients.Count; i++){
            _clients[i].GoInQueue(transformsForClient[i].position);
        }
    }

    public Vector3 GetPositionForWorker(){
        return transForWorker.position;
    }
}