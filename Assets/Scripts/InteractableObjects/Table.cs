using System;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour{
    public bool Available => (chairs[0].available || chairs[1].available) && _dirtyDishes.Count < 2;
    [SerializeField] private Chair[] chairs;
    [SerializeField] Transform portObjTrans;
    private List<DirtyDish> _dirtyDishes = new List<DirtyDish>();
    private List<Coffee> coffeesOnTableList = new List<Coffee>();

    private void Awake(){
        foreach (var chair in chairs){
            chair.table = this;
        }
    }

    public List<DirtyDish> GivebackListDirtyDishes(int amount){
        amount = Mathf.Clamp(amount, 0, _dirtyDishes.Count);
        var result = _dirtyDishes.GetRange(_dirtyDishes.Count - amount, amount);
        _dirtyDishes.RemoveRange(_dirtyDishes.Count - amount, amount);
        return result;
    }

    public void SetUpCoffeesFromClient(List<Coffee> coffees){
        foreach (var coffee in coffees){
            coffee.transform.parent = this.transform;
            coffee.transform.localPosition =
                portObjTrans.localPosition + new Vector3(0, 0, coffeesOnTableList.Count * 1f);
            coffeesOnTableList.Add(coffee);
        }
    }

    public Chair GetChair(){
        foreach (var chair in chairs){
            if (chair.available){
                chair.available = false;
                return chair;
            }
        }

        return null;
    }

    public void RemoveCoffeeFromList(Coffee coffee){
        coffeesOnTableList.Remove(coffee);
    }
}