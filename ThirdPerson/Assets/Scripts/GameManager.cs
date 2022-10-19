using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab)){
            ShowToggleInventory();
        }
    }

    private void ShowToggleInventory() {
        //call method inside InventoryManager.cs to toggle the inventory window animation
        canvas.GetComponentInChildren<InventoryManager>().ShowToggleInventory();
    }
}
