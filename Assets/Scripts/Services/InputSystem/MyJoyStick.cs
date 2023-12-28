using UnityEngine;

public class MyJoyStick : IInputSystem{
    private Joystick _joystick;

    public MyJoyStick(Joystick joystick){
        _joystick = joystick;
    }

    public Vector3 GetDirection(){
        Vector3 joyStickeDirection = Vector3.right * _joystick.Horizontal + Vector3.forward * _joystick.Vertical;
        Vector3 direction = Camera.main.transform.TransformDirection(joyStickeDirection);
        direction.y = 0;
        return direction.normalized;
    }

    public bool IsUse(){
        return _joystick.Direction != Vector2.zero;
    }
}