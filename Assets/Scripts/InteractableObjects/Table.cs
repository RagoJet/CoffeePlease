using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour{
    public bool available = true;
    [SerializeField] private Chair[] chairs;
    [SerializeField] Transform portObjTrans;
    private List<DirtyDish> _dirtyDishes = new List<DirtyDish>();
    private List<Coffee> _coffees = new List<Coffee>();


    public List<DirtyDish> GivebackListDirtyDishes(int amount){
        amount = Mathf.Clamp(amount, 0, _dirtyDishes.Count);
        var result = _dirtyDishes.GetRange(_dirtyDishes.Count - amount, amount);
        _dirtyDishes.RemoveRange(_dirtyDishes.Count - amount, amount);
        return result;
    }

    public void GetCoffeesFromClient(List<Coffee> coffees){
        foreach (var coffee in coffees){
            coffee.transform.position = portObjTrans.position + new Vector3(0, 0, _coffees.Count * -1f);
            _coffees.Add(coffee);
        }
    }
}