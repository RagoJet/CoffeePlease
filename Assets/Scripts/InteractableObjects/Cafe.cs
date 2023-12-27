using System;
using System.Collections.Generic;
using UnityEngine;

public class Cafe : MonoBehaviour{
    [SerializeField] private CashMachine cashMachine;
    [SerializeField] private List<Table> tables = new List<Table>();
    [SerializeField] private List<FillImage> fillImages = new List<FillImage>();

    private int _numberOfNewTable;

    private void Awake(){
        foreach (var fillImage in fillImages){
            fillImage.OnFilled += OpenNewTable;
        }
    }

    public bool TryGetDirtyTable(out Table theTable){
        foreach (var table in tables){
            if (table.isActiveAndEnabled && !table.Available){
                if (table.HasDirtyDish()){
                    theTable = table;
                    return true;
                }
            }
        }

        theTable = null;
        return false;
    }

    public bool TryGetAvailableTable(out Table theTable){
        foreach (var table in tables){
            if (table.isActiveAndEnabled && table.Available){
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

    public void OpenNewTable(){
        Destroy(fillImages[_numberOfNewTable].gameObject);
        _numberOfNewTable++;
        if (_numberOfNewTable < fillImages.Count){
            fillImages[_numberOfNewTable].gameObject.SetActive(true);
        }

        if (_numberOfNewTable < tables.Count){
            tables[_numberOfNewTable].gameObject.SetActive(true);
        }
    }
}