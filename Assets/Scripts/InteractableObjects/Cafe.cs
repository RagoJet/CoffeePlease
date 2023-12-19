using System.Collections.Generic;
using UnityEngine;

public class Cafe : MonoBehaviour{
    public List<Table> _tables = new List<Table>();

    public bool TryGetAvailableTable(out Vector3 pos){
        foreach (var table in _tables){
            if (table.available){
                pos = table.transform.position;
                return true;
            }
        }

        pos = Vector3.zero;
        return false;
    }
}