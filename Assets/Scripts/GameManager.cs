using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject carPrefab, rampPrefab, boxPrefab, gasCanPrefab;

    [SerializeField] private GameObject placementIndicator;

    [SerializeField] private Material redIndicatorMaterial, blueIndicatorMaterial, greenIndicatorMaterial, selectionMaterial;

    private Material baseMaterial;

    [SerializeField] private GameObject carControllers, objectControllers, uiSelectionWindow, placingButtons, missionTracker, winnerScreen;

    [SerializeField] private Text collectedText, remainingText;

    [SerializeField] private Button addNewItemButton, placeDownButton, indicatorButton, closeButton, rampButton, boxButton, carButton, missionObjectButton, selectButton, deleteObjectButton, moveUpButton, moveDownButton, rotateLeftButton, rotateRightButton, continueButton;

    private ARRaycastManager rayCastMgr;

    private bool isCarPlaced, isMissionItemSelected, selectionState, allMissionItemsPlaced, isCarSelected = true, indicatorState = true;

    private GameObject car, currentPrefab, selectedObject;

    private CarController carController;
    
    private Rigidbody carRigidbody;

    private int placedMissionObjects = 0;

    private void Start()
    {
        rayCastMgr = FindObjectOfType<ARRaycastManager>();

        placementIndicator.SetActive(false);
        carControllers.SetActive(false);
        uiSelectionWindow.SetActive(false);
        objectControllers.SetActive(false);
        missionTracker.SetActive(false);
        winnerScreen.SetActive(true);

        deleteObjectButton.gameObject.SetActive(false);

        selectButton.enabled = false;
        selectButton.onClick.AddListener(SelectState);
        addNewItemButton.onClick.AddListener(ChooseNewItem);
        placeDownButton.onClick.AddListener(PlacePrefab);
        indicatorButton.onClick.AddListener(IndicatorStateChanged);
        closeButton.onClick.AddListener(Close);
        deleteObjectButton.onClick.AddListener(DeleteObject);
        continueButton.onClick.AddListener(Continue);
        
        rampButton.onClick.AddListener(RampSelected);
        boxButton.onClick.AddListener(BoxSelected);
        carButton.onClick.AddListener(CarSelected);
        missionObjectButton.onClick.AddListener(GasSelected);

        currentPrefab = carPrefab;
    }

    private void Awake()
    {
        EnhancedTouchSupport.Enable();
    }

    private void Update()
    {
        if(indicatorState)
            UpdatePlacementIndicator();

        if (isCarPlaced && carRigidbody.position.y < car.transform.position.y - 3.0)
        {
            carController.Reset();
        }

        if (selectionState)
        {
            SelectingObject();
        }

        if (allMissionItemsPlaced)
        {
            missionObjectButton.enabled = false;
            UpdateText();
        }

        if (carController.collectedMissionObjects == 4)
        {
            missionTracker.SetActive(false);
            winnerScreen.SetActive(true);
        }

    }

    private void PlacePrefab()
    {
        Logger.Instance.LogInfo("PlaceDown pressed");
        if (isCarSelected)
        {
            car = Instantiate(currentPrefab, placementIndicator.transform.position, placementIndicator.transform.rotation);
            carRigidbody = car.GetComponentInChildren<Rigidbody>();
            carController = car.GetComponentInChildren<CarController>();
            InputManager.Instance.Bind(carController);

            isCarPlaced = true;
            carControllers.SetActive(true);

            currentPrefab = null;
            placementIndicator.SetActive(false);
            indicatorState = false;
            placeDownButton.enabled = false;

            carButton.enabled = false;
        } 
        else if (isMissionItemSelected)
        {
            if (placedMissionObjects <= 4 && !allMissionItemsPlaced)
            {
                GameObject newMissionGameObject = Instantiate(currentPrefab, placementIndicator.transform.position,
                    placementIndicator.transform.rotation);
                placedMissionObjects++;
                if (placedMissionObjects == 4)
                    allMissionItemsPlaced = true;
            }
        }
        else
        {
            GameObject newGameObject = Instantiate(currentPrefab, placementIndicator.transform.position, placementIndicator.transform.rotation);
        }

        selectButton.enabled = true;
    }

    private void UpdateText()
    {
        missionTracker.SetActive(true);
        collectedText.text = "Collected items: " + carController.collectedMissionObjects;
        remainingText.text = "Remaining items: " + (placedMissionObjects - carController.collectedMissionObjects);
    }
    
    private void UpdatePlacementIndicator()
    {
        var hits = new List<ARRaycastHit>();
        rayCastMgr.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);
        if (hits.Count > 0)
        {
            placementIndicator.transform.position = hits[0].pose.position;
            placementIndicator.transform.rotation = hits[0].pose.rotation;
            
            if(!placementIndicator.activeInHierarchy)
                placementIndicator.SetActive(true);
        }
    }

    private void SelectState()
    {
        SelectionState();
    }

    private void SelectionState()
    {
        selectionState = !selectionState;
        indicatorState = !selectionState;

        placementIndicator.SetActive(!selectionState);
        placingButtons.SetActive(!selectionState);
        if(car != null)
            carControllers.SetActive(!selectionState);

        deleteObjectButton.gameObject.SetActive(selectionState);
        objectControllers.SetActive(selectionState);

        if (selectedObject != null)
            selectedObject.GetComponentInChildren<MeshRenderer>().material = baseMaterial;
    }

    private void Continue()
    {
        missionObjectButton.enabled = true;
        placedMissionObjects = 0;
        carController.collectedMissionObjects = 0;
        allMissionItemsPlaced = false;
        winnerScreen.SetActive(false);
    }

    private void SelectingObject()
    {
        var activeTouches = Touch.activeTouches;

        if (activeTouches.Count > 0)
        {
            var touch = activeTouches[0];

            if (touch.phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject())
            {
                if (selectedObject != null)
                    selectedObject.GetComponentInChildren<MeshRenderer>().material = baseMaterial;

                Logger.Instance.LogInfo("Touching began");
                Ray raycast = Camera.main.ScreenPointToRay(touch.screenPosition);
                RaycastHit raycastHit;

                if (Physics.Raycast(raycast, out raycastHit))
                {
                    selectedObject = raycastHit.collider.gameObject;
                    if (selectedObject.gameObject.CompareTag("PlacedObject") || selectedObject.gameObject.CompareTag("MissionObject"))
                    {
                        Logger.Instance.LogInfo(selectedObject.gameObject.tag);
                        baseMaterial = selectedObject.GetComponentInChildren<MeshRenderer>().material;
                        selectedObject.GetComponentInChildren<MeshRenderer>().material = selectionMaterial;
                    }
                    else
                    {
                        selectedObject = null;
                    }
                }
            }
        }
    }

    private void DeleteObject()
    {
        Destroy(selectedObject);
    }
    
    private void ChooseNewItem()
    {
        if(selectionState)
            SelectionState();
        addNewItemButton.enabled = false;
        uiSelectionWindow.SetActive(true);
        placeDownButton.enabled = false;
        if (car == null)
            carButton.enabled = true;
    }

    private void Close()
    {
        uiSelectionWindow.SetActive(false);
        addNewItemButton.enabled = true;
        placeDownButton.enabled = true;
    }

    private void RampSelected()
    {
        currentPrefab = rampPrefab;
        placementIndicator.GetComponent<MeshRenderer>().material = greenIndicatorMaterial;
        placementIndicator.SetActive(true);
        indicatorState = true;
        isCarSelected = false;
        isMissionItemSelected = false;
    }

    private void BoxSelected()
    {
        currentPrefab = boxPrefab;
        placementIndicator.GetComponent<MeshRenderer>().material = greenIndicatorMaterial;
        placementIndicator.SetActive(true);
        indicatorState = true;
        isCarSelected = false;
        isMissionItemSelected = false;
    }

    private void GasSelected()
    {
        currentPrefab = gasCanPrefab;
        placementIndicator.GetComponent<MeshRenderer>().material = redIndicatorMaterial;
        placementIndicator.SetActive(true);
        indicatorState = true;
        isCarSelected = false;
        isMissionItemSelected = true;
    }

    private void CarSelected()
    {
        currentPrefab = carPrefab;
        placementIndicator.GetComponent<MeshRenderer>().material = blueIndicatorMaterial;
        placementIndicator.SetActive(true);
        indicatorState = true;
        isCarSelected = true;
        isMissionItemSelected = false;
    }

    private void IndicatorStateChanged()
    {
        indicatorState = !indicatorState;
        if(!indicatorState)
            placementIndicator.SetActive(false);
    }

    public void MoveObjectUp()
    {
        Logger.Instance.LogInfo("Moving Object Up");
        if(selectedObject != null)
            selectedObject.transform.position += new Vector3(0f, 0.05f, 0f);
    }

    public void MoveObjectDown()
    {
        Logger.Instance.LogInfo("Moving Object Down");
        if (selectedObject != null)
            selectedObject.transform.position += new Vector3(0f, -0.05f, 0f);
    }

    public void RotateObjectLeft()
    {
        Logger.Instance.LogInfo("Rotating Object Left");
        if (selectedObject != null)
            selectedObject.transform.Rotate(0f, 2f, 0f);
    }

    public void RotateObjectRight()
    {
        Logger.Instance.LogInfo("Moving Object Right");
        if (selectedObject != null)
            selectedObject.transform.Rotate(0f, -2f, 0f);
    }
}
