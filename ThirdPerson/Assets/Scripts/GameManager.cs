using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance = null;

    public GameObject canvas;
    private GameState gameState;

    // Start is called before the first frame update
    void Start()
    {
        //create singleton
        if(GameManager.Instance != null && Instance != this) 
            Destroy(gameObject);        
        else 
            Instance = this;

        canvas = GameObject.Find("Canvas");
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(Input.GetKeyDown(KeyCode.Tab)){
            ShowToggleInventory();
        } 
        */
    }

    public void OnChangeGameState(GameState newGameState) {
        print("Changing game state to: " + newGameState.ToString());
        gameState = newGameState;
    }

    public void OnButtonPressed(string key) {
        if(gameState == GameState.AreaA) {
            switch (key) 
            {
                case "TAB":
                    ShowToggleInventory();
                    break;
                case "K":
                    canvas.GetComponentInChildren<InventoryManager>().ChangeSelection(false);
                    break;
                case "J":
                    canvas.GetComponentInChildren<InventoryManager>().ChangeSelection(true);
                    break;
                case "RETURN":
                    canvas.GetComponentInChildren<InventoryManager>().ConfirmSelection();
                    break;
            }
        }

        else if(gameState == GameState.AreaB) {
            switch(key) {
                case "TAB":
                print("Hide the coin");
                GameObject coin = GameObject.Find("Plane2/Coin");
                if(coin != null) coin.SetActive(false);
                break;
            }
        }
    }

    public void OnMouseButtonPressed(int mouse) {
        if(gameState == GameState.AreaA) {
            switch(mouse) {
                case 0:
                    GameObject.Find("Player").GetComponent<PlayerManager>().ThrowGrenade();
                    break;
            }
        }
    }

    private void ShowToggleInventory() {
        //call method inside InventoryManager.cs to toggle the inventory window animation
        canvas.GetComponentInChildren<InventoryManager>().ShowToggleInventory();
    }

    //game consists of two areas. AreaA (BLUE zone), AreaB (Green Zone)
    public enum GameState
    {
        AreaA,
        AreaB
    }
}
