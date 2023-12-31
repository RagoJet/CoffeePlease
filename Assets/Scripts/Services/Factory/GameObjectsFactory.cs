﻿using Services;
using UnityEngine;

public class GameObjectsFactory : IGameObjectsFactory{
    public MonoBehaviour CreateObject(Vector3 pos, string path){
        return GameObject.Instantiate(Resources.Load<MonoBehaviour>(path), pos, Quaternion.identity);
    }

    public Coffee CreateCoffee(Vector3 pos){
        return CreateObject(pos, "PortableObjects/CoffeeCup") as Coffee;
    }


    public DirtyDish CreateDirtyDish(Vector3 pos){
        return CreateObject(pos, "PortableObjects/DirtyDish") as DirtyDish;
    }

    public Client CreateClient(Vector3 pos){
        return CreateObject(pos, "Client") as Client;
    }

    public MoneyObject CreateMoney(Vector3 pos){
        return CreateObject(pos, "PortableObjects/MoneyObject") as MoneyObject;
    }

    public Worker CreateWorker(Vector3 pos){
        return CreateObject(pos, "Worker") as Worker;
    }
}

public interface IGameObjectsFactory : IService{
    public Coffee CreateCoffee(Vector3 pos);
    public DirtyDish CreateDirtyDish(Vector3 pos);
    public Client CreateClient(Vector3 pos);
    public MoneyObject CreateMoney(Vector3 pos);
    public Worker CreateWorker(Vector3 pos);
}