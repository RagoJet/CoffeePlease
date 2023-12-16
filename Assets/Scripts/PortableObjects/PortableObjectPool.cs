using System.Collections.Generic;
using UnityEngine;

public class PortableObjectPool{
    private Queue<PortableObject> _queue = new Queue<PortableObject>();

    public void Add(PortableObject portObj){
        _queue.Enqueue(portObj);
        portObj.gameObject.SetActive(false);
    }

    public PortableObject TryGet(){
        if (_queue.TryDequeue(out PortableObject portObj)){
            portObj.gameObject.SetActive(true);
        }

        return portObj;
    }
}