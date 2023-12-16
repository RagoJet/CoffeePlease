using UnityEngine;

public class MyJoyStick : IInputSystem{
    private Joystick _joystick;

    public MyJoyStick(Joystick joystick){
        _joystick = joystick;
    }

    public Vector3 GetDirection(){
        Vector3 cameraDirection = Camera.main.transform.forward;
        cameraDirection.y = 0;
        Vector3 direction = -Vector3.right * _joystick.Vertical + Vector3.forward * _joystick.Horizontal +
                            cameraDirection;
        return direction.normalized;
    }

    public bool IsUse(){
        return _joystick.Direction != Vector2.zero;
    }
}