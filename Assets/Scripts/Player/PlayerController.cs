using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour {

	Camera cam;
	public LayerMask ground;

	public NavMeshAgent playerAgent;
	// Use this for initialization

	#region Monobehaivor API
	void Awake () {
		cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (1)) {
			playerAgent.SetDestination (GetPointUnderCursor ());
		}
	}
	#endregion 

	private Vector3 GetPointUnderCursor()
	{
		Vector2 screenPos = Input.mousePosition;
		Vector3 mouseWorldPos = cam.ScreenToWorldPoint (screenPos); 
		RaycastHit hitPos;

		Physics.Raycast (mouseWorldPos, cam.transform.forward, out hitPos, 100, ground);
		return hitPos.point;
	}

}
