using System.Collections.Generic;
using System.Linq;
using Services;
using UnityEngine;

public class Table : MonoBehaviour{
    public bool available = true;
    [SerializeField] private Chair[] chairs;
    [SerializeField] Transform portObjTrans;
    private List<DirtyDish> _dirtyDishes = new List<DirtyDish>();
    private List<Coffee> _coffees = new List<Coffee>();


    public List<DirtyDish> GetListDirtyDishes(int amount){
        List<DirtyDish> newListOfDirtyDish = new List<DirtyDish>();

        for (int i = 0; i < amount; i++){
            if (_dirtyDishes.Count > 0){
                newListOfDirtyDish.Add(_dirtyDishes[_dirtyDishes.Count - 1]);
                _dirtyDishes.Remove(_dirtyDishes[_dirtyDishes.Count - 1]);
            }
            else{
                break;
            }
        }

        return newListOfDirtyDish;
    }


    public bool TryGetAvailableChair(out Chair availableChair){
        availableChair = null;
        foreach (var chair in chairs){
            if (chair.available){
                chair.available = false;
                availableChair = chair;
                return true;
            }
        }

        return false;
    }

    public void GetCoffeesFromClient(List<Coffee> coffees){
        foreach (var coffee in coffees){
            coffee.transform.position = portObjTrans.position + new Vector3(0, 0, _coffees.Count * -1f);
            _coffees.Add(coffee);
        }
    }

    public void CoffeesToDirtyDishes(){
        foreach (var coffee in _coffees.ToList()){
            _dirtyDishes.Add(AllServices.Instance.GetService<IPortableObjectsFactory>()
                .CreateDirtyDish(coffee.transform.position));
            _coffees.Remove(coffee);
            Destroy(coffee.gameObject);
        }
    }
}