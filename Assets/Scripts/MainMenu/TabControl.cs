using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabControl : MonoBehaviour {

    public int currentTab = 0;
    public GameObject[] tabs;
    // Use this for initialization
    private void Start()
    {
        foreach(GameObject t in tabs)
        {
            t.SetActive(false);
        }
        tabs[currentTab].SetActive(true);
        tabs[currentTab].GetComponent<Animator>().Play("Panel Open");
    }
    public void changeTab(int tab)
    {
        tabs[currentTab].SetActive(false);
        //tabs[currentTab].GetComponent<Animator>().Play("Panel Close");
        currentTab = tab;
        tabs[currentTab].SetActive(true);
        tabs[currentTab].GetComponent<Animator>().Play("Panel Open");
    }
}
