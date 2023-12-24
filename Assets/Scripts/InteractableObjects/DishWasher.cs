using UnityEngine;

public class DishWasher : MonoBehaviour{
    [SerializeField] private Transform portObjTrans;

    public Vector3 LocalPosForPortObj => portObjTrans.localPosition;
}