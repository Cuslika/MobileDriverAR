using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    [SerializeField] private GameObject redPlacement, bluePlacement, greenPlacement, carPrefab, carControllers, UISecelctionWindow;

    [SerializeField] private Button newItemButton, placeDownButton;

    // Start is called before the first frame update
    void Start()
    {
        carControllers.SetActive(false);
        UISecelctionWindow.SetActive(false);
        newItemButton.onClick.AddListener(newItemButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void newItemButtonClicked()
    {
        UISecelctionWindow.SetActive(true);
        UISecelctionWindow.transform.Find("BoxButton").GetComponentInChildren<Button>().onClick
            .AddListener(() => Logger.Instance.LogInfo("Box is selected"));
    }
}
