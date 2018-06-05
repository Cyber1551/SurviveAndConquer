using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerMove : MonoBehaviour {


	private MouseLook mouseLook;

	private Camera cam;
	public Animation walk;
	// Use this for initialization
	void Start () {
		cam = Camera.main;
		mouseLook.Init (transform, cam.transform);

	}
	
	// Update is called once per frame
	void Update () {
		RotateView ();

	}

	void FixedUpdate()
	{
		mouseLook.UpdateCursorLock ();
	}

	private void RotateView()
	{
		mouseLook.LookRotation (transform, cam.transform);
	}

}
