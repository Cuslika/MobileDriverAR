using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Core;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    private CarController carController;

    [SerializeField]
    private GameManager gameManager;

    private bool accelerate, reverse, left, right, carReset, up, down, rotateLeft, rotateRight;

    public void Bind(CarController carController)
    {
        this.carController = carController;
    }

    public void FixedUpdate()
    {
        if(accelerate)
            carController.Accelerate();
        if (reverse)
            carController.Reverse();
        if (left)
            carController.TurnLeft();
        if (right)
            carController.TurnRight();
        if (carReset)
            carController.Reset();

        if (up)
            gameManager.MoveObjectUp();
        if (down)
            gameManager.MoveObjectDown();
        if (rotateLeft)
            gameManager.RotateObjectLeft();
        if (rotateRight)
            gameManager.RotateObjectRight();
    }

    public void OnAccelerate(InputValue inputValue)
    {
        accelerate = inputValue.isPressed;
    }
    public void OnReverse(InputValue inputValue)
    {
        reverse = inputValue.isPressed;
    }
    public void OnTurnLeft(InputValue inputValue)
    {
        left = inputValue.isPressed;
    }
    public void OnTurnRight(InputValue inputValue)
    {
        right = inputValue.isPressed;
    }
    public void OnResetCar(InputValue inputValue)
    {
        carReset = inputValue.isPressed;
    }

    public void OnUp(InputValue inputValue)
    {
        Logger.Instance.LogInfo("On Object Up");
        up = inputValue.isPressed;
    }
    public void OnDown(InputValue inputValue)
    {
        Logger.Instance.LogInfo("On Object Down");
        down = inputValue.isPressed;
    }

    public void OnRotateLeft(InputValue inputValue)
    {
        Logger.Instance.LogInfo("On Object RLeft");
        rotateLeft = inputValue.isPressed;
    }

    public void OnRotateRight(InputValue inputValue)
    {
        Logger.Instance.LogInfo("On Object RRight");
        rotateRight = inputValue.isPressed;
    }

}
