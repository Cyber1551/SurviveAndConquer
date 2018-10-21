using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateCanvas : MonoBehaviour {

    public GameObject enemy;
    public Slider bar;
    public Slider backBar;
    private void Start()
    {
        CheckForEnemy();
    }
    public void CheckForEnemy()
    {
        GameObject[] g = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject gg in g)
        {
            if (gg.gameObject.layer == LayerMask.NameToLayer("isEnemy"))
            {
                enemy = gg; 


            }
        }
        bar = enemy.GetComponentsInChildren<Slider>()[1];
        backBar = enemy.GetComponentsInChildren<Slider>()[0];
        Debug.Log(bar.gameObject.name);
    }
    // Update is called once per frame
    void Update () {
        if (bar == null)
        {
            CheckForEnemy();
            
        } 
        else
        {

            bar.transform.LookAt(bar.transform.position + transform.rotation * Vector3.back, transform.rotation * Vector3.down);
            backBar.transform.LookAt(backBar.transform.position + transform.rotation * Vector3.back, transform.rotation * Vector3.down);
        }
        
	}
}
