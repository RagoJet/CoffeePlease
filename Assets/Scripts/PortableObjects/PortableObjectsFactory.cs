using Services;
using UnityEngine;

public class PortableObjectsFactory : IPortableObjectsFactory{
    private PortableObjectPool _coffeePool = new PortableObjectPool();
    private PortableObjectPool _dirtyPool = new PortableObjectPool();

    public PortableObject CreatePortableObject(Vector3 pos, PortableObjectPool pool, string path){
        PortableObject obj = pool.TryGet();
        if (obj == null){
            obj = GameObject.Instantiate(Resources.Load<PortableObject>(path), pos, Quaternion.identity);
            obj.AfterUsed += pool.Add;
        }
        else{
            obj.transform.position = pos;
        }

        return obj;
    }

    public Coffee CreateCoffee(Vector3 pos){
        return CreatePortableObject(pos, _coffeePool, "PortableObjects/CoffeeCup") as Coffee;
    }


    public DirtyDish CreateDirtyDish(Vector3 pos){
        return CreatePortableObject(pos, _dirtyPool, "PortableObjects/DirtyDish") as DirtyDish;
    }
}

public interface IPortableObjectsFactory : IService{
    public Coffee CreateCoffee(Vector3 pos);
    public DirtyDish CreateDirtyDish(Vector3 pos);
}