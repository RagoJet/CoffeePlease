using System.Collections.Generic;
using Services;
using Units;
using UnityEngine;

public class StackOfMoney : MonoBehaviour{
    private List<MoneyObject> _stackOfMonies = new List<MoneyObject>();

    public void AddMoney(){
        MoneyObject moneyObject = AllServices.Instance.Get<IGameObjectsFactory>()
            .CreateMoney(transform.position + new Vector3(0, _stackOfMonies.Count * 0.4f, 0));
        _stackOfMonies.Add(moneyObject);
    }

    public void GiveMoney(Player player){
        float duration = 0.3f;
        _stackOfMonies.Reverse();
        foreach (var moneyObject in _stackOfMonies){
            moneyObject.transform.parent = player.transform;
            moneyObject.MoveTo(player.Body.localPosition, duration, () => CashOut(player, moneyObject));
            duration += 0.05f;
        }

        _stackOfMonies.Clear();
    }

    public void CashOut(Player player, MoneyObject moneyObject){
        player.AddMoney(7);
        Destroy(moneyObject.gameObject);
    }
}