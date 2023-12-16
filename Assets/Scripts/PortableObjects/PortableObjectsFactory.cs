using Services;
using UnityEngine;

public class PortableObjectsFactory : IPortableObjectsFactory{
    private PortableObjectPool _coffeePool = new PortableObjectPool();
    private PortableObjectPool _dirtyPool = new PortableObjectPool();

    public void CreatePortableObject(Vector3 pos, PortableObjectPool pool, string path){
        PortableObject obj = pool.TryGet();
        if (obj == null){
            obj = GameObject.Instantiate(Resources.Load<PortableObject>(path), pos, Quaternion.identity);
            obj.AfterUsed += pool.Add;
        }
        else{
            obj.transform.position = pos;
        }
    }

    public void CreateCoffee(Vector3 pos){
        CreatePortableObject(pos, _coffeePool, "PortableObjects/CoffeeCup");
    }


    public void CreateDirtyDish(Vector3 pos){
        CreatePortableObject(pos, _dirtyPool, "PortableObjects/DirtyDish");
    }
}

public interface IPortableObjectsFactory : IService{
    public void CreateCoffee(Vector3 pos);
    public void CreateDirtyDish(Vector3 pos);
}