using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    [Tooltip("Number of items in inventory")]
    public int numberOfItems = 5;

    [Tooltip("Items Selection Panel")]
    public GameObject itemsSelectionPanel;

    [Tooltip("List of Items")]
    public List<ItemScriptableObjects> ItemsAvailable;

    [Tooltip("Selected Item Color")]
    public Color selectedColor;

    [Tooltip("Not Selected Item Color")]
    public Color notSelectedColor;

    private List<InventoryItem> itemsForPlayer; //the items visible to the player during the game

    public int currentSelectedIndex = 0; //by default start/select the first button in the inventory system

    private Animator animator;

    [Tooltip ("Show Inventory GUI")]
    public bool showInventory = false;

    // Start is called before the first frame update
    void Start()
    {

        animator = itemsSelectionPanel.GetComponent<Animator>();

        itemsForPlayer = new List<InventoryItem>();
        PopulateInventorySpawn();
        RefreshInventoryGUI();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.K))
            ChangeSelection();

        else if(Input.GetKeyDown(KeyCode.Return))
            ConfirmSelection();

        */
    }

    public void ShowToggleInventory(){
        if(showInventory == false){
            showInventory = true;
            //print("inventoryin");
            animator.SetTrigger("InventoryIn");
        }

        else {
            showInventory = false;
            //print("inventoryout");
            animator.SetTrigger("InventoryOut");
        }
    }

    public void ConfirmSelection() {
        if (itemsForPlayer.Count != 0){
            //Get the item from the itemsForPlayer list using the currentSelectedIndex
            InventoryItem inventoryItem = itemsForPlayer[currentSelectedIndex];
            print("Item Selected is:" + inventoryItem.item.name);

            //reduce the quantity of 1
            inventoryItem.quantity -= 1;

            //check if the quantity is 0, if it is we need to remove this item from the itemsForPlayer list
            if(inventoryItem.quantity == 0)
                itemsForPlayer.RemoveAt(currentSelectedIndex);
        }

        RefreshInventoryGUI();

    }

    public void ChangeSelection(bool moveLeft) {

        /*
        //move to the left hand side
        if(Input.GetKeyDown(KeyCode.J)) {
            currentSelectedIndex -= 1;
        }
        //move to the right hand side
        else if(Input.GetKeyDown(KeyCode.K)) {
            currentSelectedIndex += 1;
        }

        */

        if(moveLeft == true) 
            currentSelectedIndex -= 1;
        
        else 
            currentSelectedIndex += 1;
        
        //check boundaries
        if(currentSelectedIndex < 0)
            currentSelectedIndex = 0;

        if (currentSelectedIndex == itemsForPlayer.Count)
            currentSelectedIndex = currentSelectedIndex - 1;

        RefreshInventoryGUI();
    }

    /// <summary>
    /// This method will generate the inventory items for the player to use during the game.
    /// The total number of inventory items cannot exceed the number set int the variable numberOfItems.
    /// </summary>
    private void PopulateInventorySpawn()
    {
        for (int i = 0; i < numberOfItems; i++)
        {
            //pick random object from list ItemsAvailable
            ItemScriptableObjects objItem = ItemsAvailable[Random.Range(0, ItemsAvailable.Count)];
            //check whether objItem exists in itemsForPlayer. So basically we need to count how many times an item appears.
            //i.e the number of objItems inside itemsForPlayer
            int countItems = itemsForPlayer.Where(x => x.item == objItem).ToList().Count;

            if (countItems == 0)
            {
                itemsForPlayer.Add(new InventoryItem() { item = objItem, quantity = 1 });
            }
            else
            {
                //search for the element of the same type inside itemsForPlayer
                var item = itemsForPlayer.First(x => x.item == objItem);
                //increase the quantity by 1
                item.quantity += 1;
            }

            /*this code is the same for above line
            InventoryItem inventoryItem = new InventoryItem();
            inventoryItem.item = objItem;
            inventoryItem.quantity = 1;
            itemsForPlayer.Add(inventoryItem);*/

            print("Number of Inventory Items for Player" + itemsForPlayer.Count);
        }
    }

    private void RefreshInventoryGUI()
    {
        int buttonID = 0;

        foreach (InventoryItem i in itemsForPlayer)
        {
            //load the button
            GameObject button = itemsSelectionPanel.transform.Find("Button" + buttonID).gameObject;

            //search for the child image and change its sprite
            button.transform.Find("Image").GetComponent<Image>().sprite = i.item.icon;

            //change the quantity
            button.transform.Find("Quantity").GetComponent<TextMeshProUGUI>().text = "x" + i.quantity;

            if(buttonID == currentSelectedIndex) {
                button.GetComponent<Image>().color = selectedColor;
            }
            else {
                button.GetComponent<Image>().color = notSelectedColor;
            }

            buttonID += 1;
        }

        // set active false redundant buttons
        for(int i = buttonID; i<3; i++) {
            itemsSelectionPanel.transform.Find("Button" + i).gameObject.SetActive(false);
        }

    }


    public class InventoryItem
    {
        public ItemScriptableObjects item { get; set; }
        public int quantity { get; set; }
    }

}
