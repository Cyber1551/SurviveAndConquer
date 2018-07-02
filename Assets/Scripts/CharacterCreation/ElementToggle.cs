using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ElementToggle : MonoBehaviour {
    
    public string selected;
    public Sprite[] sprites;
    public Sprite[] selectedSprites;
    public GameObject[] panelsObj;
    //public Button[] elements;

    public Dictionary<string, Sprite> spr_elements;
    public Dictionary<string, Sprite> spr_selected;
    
    public Dictionary<string, GameObject> panels;
    
    public Dictionary<string, Button> elements;
	// Use this for initialization
	void Start () {
        elements = new Dictionary<string, Button>
        {
            { "Fire", GameObject.Find("Fire").GetComponent<Button>() },
            { "Water", GameObject.Find("Water").GetComponent<Button>() },
            { "Earth", GameObject.Find("Earth").GetComponent<Button>() },
            { "Wind", GameObject.Find("Wind").GetComponent<Button>() },
            { "Life", GameObject.Find("Life").GetComponent<Button>() },
            { "Death", GameObject.Find("Death").GetComponent<Button>() },
            { "Void", GameObject.Find("Void").GetComponent<Button>() }
        };
        spr_elements = new Dictionary<string, Sprite>
        {
            { "Fire", sprites[0] },
            { "Water", sprites[1] },
            { "Earth", sprites[2] },
            { "Wind", sprites[3] },
            { "Life", sprites[4] },
            { "Death", sprites[5] },
            { "Void", sprites[6] },
        };
        spr_selected = new Dictionary<string, Sprite>
        {
            { "Fire", selectedSprites[0] },
            { "Water", selectedSprites[1] },
            { "Earth", selectedSprites[2] },
            { "Wind", selectedSprites[3] },
            { "Life", selectedSprites[4] },
            { "Death", selectedSprites[5] },
            { "Void", selectedSprites[6] },
        };
        panels = new Dictionary<string, GameObject>
        {
            { "Fire", panelsObj[0] },
            { "Water", panelsObj[1]},
            { "Earth", panelsObj[2] },
            { "Wind", panelsObj[3] },
            { "Life", panelsObj[4]},
            { "Death", panelsObj[5]},
            { "Void", panelsObj[6] },
        };

        selected = "Fire";
        elements["Fire"].GetComponent<Image>().sprite = spr_selected["Fire"];
        panels["Fire"].gameObject.SetActive(true);
         foreach(KeyValuePair<string, Button> btn in elements)
        {
            btn.Value.onClick.AddListener(delegate { ChangeSelected(btn.Value.name); });
        }
	}
    void ChangeSelected(string ele)
    {
        elements[selected].GetComponent<Image>().sprite = spr_elements[selected];
        panels[selected].SetActive(false);
        selected = ele;

        elements[selected].GetComponent<Image>().sprite = spr_selected[selected];
        panels[selected].SetActive(true);
        
    }

    public void selectElement()
    {
        
        GameObject.Find("NetworkManager").GetComponent<AddUser>().UpdateUser(PlayerData.username, "element", selected);
        SceneManager.LoadScene("LoginRegister");
    }
	
	
}
