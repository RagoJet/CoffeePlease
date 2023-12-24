using UnityEngine;

public class Chair : MonoBehaviour{
    [SerializeField] private Transform transForPortableObj;
    public bool available = true;
    public Table table;

    public Vector3 PosForPortableObj => transForPortableObj.position;
}