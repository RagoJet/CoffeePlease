using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour{
    public bool Available => (_chairs[0].available || _chairs[1].available) && _dirtyDishes.Count < 2;
    private Chair[] _chairs;
    [SerializeField] Transform portObjTrans;
    private List<DirtyDish> _dirtyDishes = new List<DirtyDish>();
    private List<Coffee> _coffeesOnTableList = new List<Coffee>();


    private void Awake(){
        _chairs = GetComponentsInChildren<Chair>();
        foreach (var chair in _chairs){
            chair.table = this;
        }
    }

    public bool HasDirtyDish(){
        if (_dirtyDishes.Count > 0){
            return true;
        }

        return false;
    }

    public List<DirtyDish> GivebackListDirtyDishes(int amount){
        amount = Mathf.Clamp(amount, 0, _dirtyDishes.Count);
        var result = _dirtyDishes.GetRange(_dirtyDishes.Count - amount, amount);
        _dirtyDishes.RemoveRange(_dirtyDishes.Count - amount, amount);
        foreach (var dirtyDish in result){
            dirtyDish.chair.available = true;
        }

        GetComponentInParent<Cafe>().CheckClients();
        return result;
    }

    public void ReceiveCoffees(List<Coffee> coffees){
        foreach (var coffee in coffees){
            _coffeesOnTableList.Add(coffee);
        }
    }

    public Chair GetChair(){
        foreach (var chair in _chairs){
            if (chair.available){
                chair.available = false;
                return chair;
            }
        }

        return null;
    }

    public void RemoveCoffeeFromList(Coffee coffee){
        _coffeesOnTableList.Remove(coffee);
    }

    public void AddDirtyDish(DirtyDish dish){
        _dirtyDishes.Add(dish);
    }
}