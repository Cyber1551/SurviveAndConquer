using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryHUD : MonoBehaviour {

    public Inventory inventory;
    public Image[] borders;
    public Sprite fillBorder;
    public Sprite emptyBorder;
	// Use this for initialization
	void Start () {

        
        borders = new Image[6];

        for (int i = 0; i < 6; i++)
        {
            borders[i] = transform.GetChild(i).GetComponent<Image>(); 
        }
       
        borders[0].sprite = fillBorder;
	}

    // Update is called once per frame
    void Update () {
        var d = Input.GetAxis("Mouse ScrollWheel");
        
        if (d > 0f)
        {
            borders[inventory.currentSlot].sprite = emptyBorder;
            inventory.currentSlot++;
            if (inventory.currentSlot == 6) inventory.currentSlot = 0;
            borders[inventory.currentSlot].sprite = fillBorder;
            inventory.UpdateSpells();
        }
        else if (d < 0f)
        {
            borders[inventory.currentSlot].sprite = emptyBorder;
            inventory.currentSlot--;
            if (inventory.currentSlot == -1) inventory.currentSlot = 5;
            borders[inventory.currentSlot].sprite = fillBorder;
            inventory.UpdateSpells();
        }
        
    }
}
