using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupText : MonoBehaviour {
    //public Animator animator;

    public Camera cam;
	// Use this for initialization
	void Awake () {
        //AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        //Destroy(gameObject, clipInfo[0].clip.length);
       
    }
    public void GetCamera()
    {
        GameObject[] g = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject gg in g)
        { 
            if (gg.gameObject.layer == LayerMask.NameToLayer("isSelf"))
            {
                cam = gg.transform.Find("FirstPersonCharacter").GetComponent<Camera>();


            }
        }

    }
    private void Start()
    {
        Destroy(gameObject, 1.5f);
        GetCamera();
        //transform.position += offset;


    }   
    void Update()
    {
        /*if (cam == null) 
        {
            GetCamera();
        }
        else
        {
            transform.LookAt(transform.position + cam.transform.rotation * Vector3.forward, cam.transform.rotation * Vector3.up);
           
        }*/
        transform.rotation = transform.parent.Find("Slider").transform.rotation ;
        
    }

}
