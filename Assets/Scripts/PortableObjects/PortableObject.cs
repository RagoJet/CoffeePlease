using System;
using UnityEngine;

public class PortableObject : MonoBehaviour{
    public event Action<PortableObject> AfterUsed;

    protected void Utilize(){
        AfterUsed?.Invoke(this);
    }
}