// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Inputs.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Inputs : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Inputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Inputs"",
    ""maps"": [
        {
            ""name"": ""Drive"",
            ""id"": ""7c142110-f9ca-4494-a367-e6363641416d"",
            ""actions"": [
                {
                    ""name"": ""Accelerate"",
                    ""type"": ""PassThrough"",
                    ""id"": ""83d0ca08-d006-4b42-85a4-7ca57923ab7d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Reverse"",
                    ""type"": ""PassThrough"",
                    ""id"": ""ce0c8bbb-e832-42b3-859b-ac921f03105c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TurnLeft"",
                    ""type"": ""PassThrough"",
                    ""id"": ""f00473c2-6129-4952-ab36-c84aa39830df"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TurnRight"",
                    ""type"": ""PassThrough"",
                    ""id"": ""34d75f8c-61b3-444b-bd87-52e2194f78b6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ResetCar"",
                    ""type"": ""PassThrough"",
                    ""id"": ""7663cc37-cbf6-4671-b208-f89a0d096e56"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""2f07df78-eede-4fd7-9655-02beeec5acc4"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Accelerate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""07827e3a-9e79-40f5-8ad6-0d35607bbf0b"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Reverse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c47a5c36-0484-4d51-96b7-833d2d09dc67"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TurnLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e48ede6d-7466-4644-80a3-0e1e23b230a2"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TurnRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b4862853-f651-4c5f-9b4e-0fbc69f2501b"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ResetCar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Drive
        m_Drive = asset.FindActionMap("Drive", throwIfNotFound: true);
        m_Drive_Accelerate = m_Drive.FindAction("Accelerate", throwIfNotFound: true);
        m_Drive_Reverse = m_Drive.FindAction("Reverse", throwIfNotFound: true);
        m_Drive_TurnLeft = m_Drive.FindAction("TurnLeft", throwIfNotFound: true);
        m_Drive_TurnRight = m_Drive.FindAction("TurnRight", throwIfNotFound: true);
        m_Drive_ResetCar = m_Drive.FindAction("ResetCar", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Drive
    private readonly InputActionMap m_Drive;
    private IDriveActions m_DriveActionsCallbackInterface;
    private readonly InputAction m_Drive_Accelerate;
    private readonly InputAction m_Drive_Reverse;
    private readonly InputAction m_Drive_TurnLeft;
    private readonly InputAction m_Drive_TurnRight;
    private readonly InputAction m_Drive_ResetCar;
    public struct DriveActions
    {
        private @Inputs m_Wrapper;
        public DriveActions(@Inputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Accelerate => m_Wrapper.m_Drive_Accelerate;
        public InputAction @Reverse => m_Wrapper.m_Drive_Reverse;
        public InputAction @TurnLeft => m_Wrapper.m_Drive_TurnLeft;
        public InputAction @TurnRight => m_Wrapper.m_Drive_TurnRight;
        public InputAction @ResetCar => m_Wrapper.m_Drive_ResetCar;
        public InputActionMap Get() { return m_Wrapper.m_Drive; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DriveActions set) { return set.Get(); }
        public void SetCallbacks(IDriveActions instance)
        {
            if (m_Wrapper.m_DriveActionsCallbackInterface != null)
            {
                @Accelerate.started -= m_Wrapper.m_DriveActionsCallbackInterface.OnAccelerate;
                @Accelerate.performed -= m_Wrapper.m_DriveActionsCallbackInterface.OnAccelerate;
                @Accelerate.canceled -= m_Wrapper.m_DriveActionsCallbackInterface.OnAccelerate;
                @Reverse.started -= m_Wrapper.m_DriveActionsCallbackInterface.OnReverse;
                @Reverse.performed -= m_Wrapper.m_DriveActionsCallbackInterface.OnReverse;
                @Reverse.canceled -= m_Wrapper.m_DriveActionsCallbackInterface.OnReverse;
                @TurnLeft.started -= m_Wrapper.m_DriveActionsCallbackInterface.OnTurnLeft;
                @TurnLeft.performed -= m_Wrapper.m_DriveActionsCallbackInterface.OnTurnLeft;
                @TurnLeft.canceled -= m_Wrapper.m_DriveActionsCallbackInterface.OnTurnLeft;
                @TurnRight.started -= m_Wrapper.m_DriveActionsCallbackInterface.OnTurnRight;
                @TurnRight.performed -= m_Wrapper.m_DriveActionsCallbackInterface.OnTurnRight;
                @TurnRight.canceled -= m_Wrapper.m_DriveActionsCallbackInterface.OnTurnRight;
                @ResetCar.started -= m_Wrapper.m_DriveActionsCallbackInterface.OnResetCar;
                @ResetCar.performed -= m_Wrapper.m_DriveActionsCallbackInterface.OnResetCar;
                @ResetCar.canceled -= m_Wrapper.m_DriveActionsCallbackInterface.OnResetCar;
            }
            m_Wrapper.m_DriveActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Accelerate.started += instance.OnAccelerate;
                @Accelerate.performed += instance.OnAccelerate;
                @Accelerate.canceled += instance.OnAccelerate;
                @Reverse.started += instance.OnReverse;
                @Reverse.performed += instance.OnReverse;
                @Reverse.canceled += instance.OnReverse;
                @TurnLeft.started += instance.OnTurnLeft;
                @TurnLeft.performed += instance.OnTurnLeft;
                @TurnLeft.canceled += instance.OnTurnLeft;
                @TurnRight.started += instance.OnTurnRight;
                @TurnRight.performed += instance.OnTurnRight;
                @TurnRight.canceled += instance.OnTurnRight;
                @ResetCar.started += instance.OnResetCar;
                @ResetCar.performed += instance.OnResetCar;
                @ResetCar.canceled += instance.OnResetCar;
            }
        }
    }
    public DriveActions @Drive => new DriveActions(this);
    public interface IDriveActions
    {
        void OnAccelerate(InputAction.CallbackContext context);
        void OnReverse(InputAction.CallbackContext context);
        void OnTurnLeft(InputAction.CallbackContext context);
        void OnTurnRight(InputAction.CallbackContext context);
        void OnResetCar(InputAction.CallbackContext context);
    }
}
