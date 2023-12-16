using Services;
using UnityEngine;

public interface IInputSystem : IService{
    public Vector3 GetDirection();
    public bool IsUse();
}