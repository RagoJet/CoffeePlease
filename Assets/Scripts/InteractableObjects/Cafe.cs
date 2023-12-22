using System.Collections.Generic;
using UnityEngine;

public class Cafe : MonoBehaviour{
    [SerializeField] private CashMachine cashMachine;
    public List<Table> _tables = new List<Table>();

    public bool TryGetAvailableTable(out Table theTable){
        foreach (var table in _tables){
            if (table.Available){
                theTable = table;
                return true;
            }
        }

        theTable = null;
        return false;
    }

    public void CheckClients(){
        cashMachine.TryGiveOrderToFirstClient();
    }
}