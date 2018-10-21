using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellList : MonoBehaviour {

    public GameObject imagePrefab;
    public string[] fireTxt; //ALL of the fire spells (This allows the indexes to match) the collection.cs is the spell you unlock
    public Spell[] fireSpells;
    public string[] waterTxt;
    public Spell[] waterSpells;

    public GameObject FireTab;
    public GameObject WaterTab;
    public GameObject EarthTab;
    public GameObject WindTab;
    public Boolean inGame = false;
    private void Start()
    {
        if (!inGame)
        {

            foreach (string i in PlayerData.col.fire)
            {
                int index = Array.IndexOf(fireTxt, i);
                if (Array.IndexOf(PlayerData.spellBook, i) > -1)
                {
                    GameObject img = Instantiate(imagePrefab.transform.GetChild(0).gameObject, getSpellSlot(i).position, Quaternion.identity, getSpellSlot(i));
                    img.transform.GetComponent<Image>().sprite = fireSpells[index].img;
                    img.transform.GetChild(0).GetComponent<Text>().text = i;

                    GameObject border = Instantiate(imagePrefab, FireTab.transform.position, Quaternion.identity, FireTab.transform);
                    border.GetComponent<Slot>().type = "Fire";
                    GameObject.Destroy(border.transform.GetChild(0).gameObject);
                }
                else
                {
                    GameObject img = Instantiate(imagePrefab, FireTab.transform.position, Quaternion.identity, FireTab.transform);
                    img.transform.GetChild(0).GetComponent<Image>().sprite = fireSpells[index].img;
                    img.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = i;
                    img.GetComponent<Slot>().type = "Fire";
                }


            }
            foreach (string i in PlayerData.col.water)
            {

                int index = Array.IndexOf(waterTxt, i);
                if (Array.IndexOf(PlayerData.spellBook, i) > -1)
                {
                    GameObject img = Instantiate(imagePrefab.transform.GetChild(0).gameObject, getSpellSlot(i).position, Quaternion.identity, getSpellSlot(i));
                    img.transform.GetComponent<Image>().sprite = waterSpells[index].img;
                    img.transform.GetChild(0).GetComponent<Text>().text = i;

                    GameObject border = Instantiate(imagePrefab, WaterTab.transform.position, Quaternion.identity, WaterTab.transform);
                    border.GetComponent<Slot>().type = "Water";
                    GameObject.Destroy(border.transform.GetChild(0).gameObject);
                }
                else
                {
                    GameObject img = Instantiate(imagePrefab, WaterTab.transform.position, Quaternion.identity, WaterTab.transform);
                    img.transform.GetChild(0).GetComponent<Image>().sprite = waterSpells[index].img;
                    img.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = i;
                    img.GetComponent<Slot>().type = "Water";
                }

            }
        }

    }
    public string getSpellElement(Sprite img)
    {
        foreach (Spell sp in fireSpells)
        {
            if (sp.img == img)
            {
                return "Fire";
            }
        }
        foreach (Spell sp in waterSpells)
        {
            if (sp.img == img)
            {
                return "Water";
            }
        }
        return "NULL";
    }
    public string getSpellType(Sprite img)
    {
        foreach (Spell sp in fireSpells)
        {
            if (sp.img == img)
            {
                return sp.type;
            }
        }
        foreach (Spell sp in waterSpells)
        {
            if (sp.img == img)
            {
                return sp.type;
            }
        }
        return "NULL";
    }
    public Spell getSpell(string name)
    {

        foreach (string s in fireTxt)
        {
            if (s == name)
            {
                int index = Array.IndexOf(fireTxt, s);
                return fireSpells[index];
            }
        }
        foreach (string s in waterTxt)
        {
            if (s == name)
            {
                int index = Array.IndexOf(waterTxt, s);
                return waterSpells[index];
            }
        }
        return null;
    }
    public string getSpellName(Sprite img, string type)
    {
        Spell[] spr = null;
        string[] txt = null;
        switch (type)
        {
            case "Fire":
                spr = fireSpells;
                txt = fireTxt;
                break;
            case "Earth":

                break;
            case "Water":
                spr = waterSpells;
                txt = waterTxt;
                break;
            case "Wind":

                break;
        }
        foreach (Spell sp in spr)
        {
            if (sp.img == img)
            {
                int index = Array.IndexOf(spr, sp);
                return txt[index];
            }
        }
        return "NULL";
    }
    public Sprite getSpellImage(string n)
    {
        foreach (Spell sp in fireSpells)
        {
            if (sp.name == n)
            {
                return sp.img;
            }
        }
        foreach (Spell sp in waterSpells)
        {
            if (sp.name == n)
            {
                return sp.img;
            }
        }
        return null;
    }
    public Transform getSpellSlot(string n)
    {
        foreach (string s in PlayerData.spellBook)
        {
            if(s == n)
            {
                int index = Array.IndexOf(PlayerData.spellBook, s);
                return transform.GetChild(0).GetChild(index);
            }
        }
        return null;
    }
    public void saveLoadout()
    {
        string json = "";
        foreach (Transform t in transform.GetChild(0))
        {
            if (t.childCount > 0)
            {
                json += t.GetChild(0).GetChild(0).GetComponent<Text>().text + ",";
            }
            else
            {
                json += "Empty,";
            }
        }
        json = json.Substring(0, json.Length - 1);
        AddUser au = GameObject.Find("NetworkManager").GetComponent<AddUser>();
        StartCoroutine(au.UpdateUser(PlayerData.username, "spellbook", json));
       }
}
