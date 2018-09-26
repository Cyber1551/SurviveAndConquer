using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCanvas : MonoBehaviour {

    public GameObject enemy;
    public Canvas can;
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
        can = enemy.GetComponentInChildren<Canvas>();
        Debug.Log(can.gameObject.name);
    }
    // Update is called once per frame
    void Update () {
        if (can == null)
        {
            CheckForEnemy();
            
        } 
        else
        {
            
            can.transform.LookAt(can.transform.position + transform.rotation * Vector3.back, transform.rotation * Vector3.down);
        }
        
	}
}
