using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecievedMovement : MonoBehaviour {

	Vector3 newPosition;
	public float speed;
	public float walkRange;

	public GameObject graphics;

	// Use this for initialization
	void Start () {
		newPosition = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance (newPosition, this.transform.position) > walkRange) {
			this.transform.position = Vector3.MoveTowards (this.transform.position, newPosition, speed * Time.deltaTime);
			Quaternion transRot = Quaternion.LookRotation (newPosition - this.transform.position, Vector3.up);
			graphics.transform.rotation = Quaternion.Slerp (transRot, graphics.transform.rotation, 0.8f);
		}
	}
	 
	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		/*if (stream.isWriting) {
			stream.SendNext (transform.position);
			stream.SendNext (transform.rotation);
		} else {
			realPos = (Vector3)stream.ReceiveNext ();
			realRot = (Quaternion)stream.ReceiveNext ();
		}*/
	}
	[PunRPC]
	public void RecievedMove(Vector3 movePos)
	{
		newPosition = movePos;
	}
}
