using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

	public float scrollSpeed;
	public float topBarrier;
	public float botBarrier;
	public float leftBarrier;
	public float rightBarrier;
	public GameObject origin;
	public float distance;



	// Update is called once per frame
	void Update () {
		
		if (Input.mousePosition.y >= Screen.height * topBarrier && Input.mousePosition.y <= Screen.height) {
			transform.Translate (Vector3.forward * Time.deltaTime * scrollSpeed, Space.World);
		}
		if (Input.mousePosition.y <= Screen.height * botBarrier && Input.mousePosition.y >= 0) {
			transform.Translate (Vector3.back * Time.deltaTime * scrollSpeed, Space.World);
		}


		if (Input.mousePosition.x >= Screen.width * rightBarrier && Input.mousePosition.x <= Screen.width) {
			transform.Translate (Vector3.right * Time.deltaTime * scrollSpeed, Space.World);
		}
		if (Input.mousePosition.x <= Screen.width * leftBarrier && Input.mousePosition.y >= 0) {
			transform.Translate (Vector3.left * Time.deltaTime * scrollSpeed, Space.World);
		}
		if (Input.GetAxis ("Mouse ScrollWheel") > 0f) {
			transform.Translate (Vector3.down * Time.deltaTime * scrollSpeed, Space.World);
		} else if(Input.GetAxis ("Mouse ScrollWheel") < 0f) {
			transform.Translate (Vector3.up * Time.deltaTime * scrollSpeed, Space.World);
		} 
		/*if (Input.GetKey(KeyCode.LeftArrow)) {
			transform.RotateAround (origin.transform.position, Vector3.up, 20 * Time.deltaTime);
		} else if (Input.GetKey(KeyCode.RightArrow)) {
			transform.RotateAround (origin.transform.position, Vector3.up, -20 * Time.deltaTime);
		}*/

	} 
}
