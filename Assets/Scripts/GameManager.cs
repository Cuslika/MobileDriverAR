using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject carPrefab, rampPrefab, boxPrefab;

    [SerializeField] private GameObject placementIndicator;

    [SerializeField] private Material redIndicatorMaterial, blueIndicatorMaterial, greenIndicatorMaterial;

    [SerializeField] private GameObject carControllers, uiSelectionWindow;

    [SerializeField] private Button addNewItemButton, placeDownButton, indicatorButton, closeButton, rampButton, boxButton, carButton;

    private ARRaycastManager rayCastMgr;
    
    private bool isCarPlaced, isCarSelected, indicatorState = true;

    private GameObject car, currentPrefab;

    private List<GameObject> placedObjects;

    private Rigidbody carRigidbody;

    private void Start()
    {
        rayCastMgr = FindObjectOfType<ARRaycastManager>();

        placementIndicator.SetActive(false);
        carControllers.SetActive(false);
        uiSelectionWindow.SetActive(false);

        addNewItemButton.onClick.AddListener(ChooseNewItem);
        placeDownButton.onClick.AddListener(PlacePrefab);
        indicatorButton.onClick.AddListener(IndicatorStateChanged);
        closeButton.onClick.AddListener(Close);

        rampButton.onClick.AddListener(RampSelected);
        boxButton.onClick.AddListener(BoxSelected);
        carButton.onClick.AddListener(CarSelected);

        currentPrefab = carPrefab;
        isCarSelected = true;
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
            Destroy(car);
            isCarPlaced = false;
        }
    }

    private void PlacePrefab()
    {
        if (isCarSelected)
        {
            car = Instantiate(currentPrefab, placementIndicator.transform.position, placementIndicator.transform.rotation);
            carRigidbody = car.GetComponentInChildren<Rigidbody>();
            CarController carController = car.GetComponentInChildren<CarController>();
            InputManager.Instance.Bind(carController);

            isCarPlaced = true;
            carControllers.SetActive(true);

            currentPrefab = null;
            placementIndicator.SetActive(false);
            indicatorState = false;
            placeDownButton.enabled = false;

            carButton.enabled = false;
        }

        GameObject newGameObject = Instantiate(currentPrefab, placementIndicator.transform.position, placementIndicator.transform.rotation);
        placedObjects.Add(newGameObject);
    }

    /*private void PlaceSelectedPrefab(GameObject prefab)
    {
        var activeTouches = Touch.activeTouches;

        if (activeTouches.Count > 0)
        {
            var touch = activeTouches[0];

            bool isOverUi = EventSystem.current.IsPointerOverGameObject();
            Logger.Instance.LogInfo($"Over UI: {isOverUi}");

            if (!isOverUi && touch.phase == UnityEngine.InputSystem.TouchPhase.Began)
            {
                car = Instantiate(carPrefab, placementIndicator.transform.position, placementIndicator.transform.rotation);
                carRigidbody = car.GetComponentInChildren<Rigidbody>();
                CarController carController = car.GetComponentInChildren<CarController>();
                InputManager.Instance.Bind(carController);

                isCarPlaced = true;
                placementIndicator.SetActive(false);
            }
        }
    }*/

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

    private void ChooseNewItem()
    {
        Logger.Instance.LogInfo((car == null).ToString());
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
    }

    private void BoxSelected()
    {
        currentPrefab = boxPrefab;
        placementIndicator.GetComponent<MeshRenderer>().material = redIndicatorMaterial;
        placementIndicator.SetActive(true);
        indicatorState = true;
        isCarSelected = false;
    }

    private void CarSelected()
    {
        currentPrefab = carPrefab;
        placementIndicator.GetComponent<MeshRenderer>().material = blueIndicatorMaterial;
        placementIndicator.SetActive(true);
        indicatorState = true;
        isCarSelected = true;
    }

    private void IndicatorStateChanged()
    {
        indicatorState = !indicatorState;
        if(!indicatorState)
            placementIndicator.SetActive(false);
    }
}
