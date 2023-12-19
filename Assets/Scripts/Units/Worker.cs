using System.Linq;
using UnityEngine;

public class Worker : Unit{
    private void OnTriggerEnter(Collider other){
        if (other.TryGetComponent(out CoffeeMachine coffeeMachine)){
            if (_dirtyDishes.Count > 0){
                return;
            }

            GetCoffee(coffeeMachine.GetListCoffee(capacity - _coffees.Count));
        }

        if (other.TryGetComponent(out CashMachine cashMachine)){
            if (_dirtyDishes.Count > 0){
                return;
            }

            foreach (var coffee in _coffees){
                coffee.transform.parent = null;
            }

            cashMachine.GetCoffeesFromWorker(_coffees);

            _coffees.Clear();
        }

        if (other.TryGetComponent(out Table table)){
            if (_coffees.Count > 0){
                return;
            }

            GetDirtyDishes(table.GetListDirtyDishes(capacity - _dirtyDishes.Count));
        }

        if (other.TryGetComponent(out DishWasher dishWasher)){
            if (_dirtyDishes.Count > 0){
                foreach (var dish in _dirtyDishes.ToList()){
                    _dirtyDishes.Remove(dish);
                    Destroy(dish.gameObject);
                }
            }
        }
    }
}