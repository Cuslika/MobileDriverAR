using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Core;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    private CarController carController;

    [SerializeField]
    private Manager gameManager;

    private bool accelerate, reverse, left, right, carReset;

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

}
