using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetwork : MonoBehaviour {

	//[SerializeField] private Transform playerCamera;
	[SerializeField] private MonoBehaviour[] playerControl;


	Vector3 realPos = Vector3.zero;
	Quaternion realRot = Quaternion.identity;

	private PhotonView photonView;

	private void Start()
	{
		PhotonNetwork.sendRate = 20;
		PhotonNetwork.sendRateOnSerialize = 10;
		photonView = GetComponent<PhotonView> ();
		Init ();
	}
	/*void Update()
	{
		if (photonView.isMine) {

		} else {
			transform.position = Vector3.Lerp (transform.position, realPos, 0.1f);
			transform.rotation = Quaternion.Lerp (transform.rotation, realRot, 0.1f);
		}
	}
*/
	private void Init()
	{ 
		if (photonView.isMine) {
			
		} else {
			//playerCamera.gameObject.SetActive (false);
			foreach (MonoBehaviour m in playerControl) {
				m.enabled = false;
			}
		}
	}

	/*void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting) {
			stream.SendNext (transform.position);
			stream.SendNext (transform.rotation);
		} else {
			realPos = (Vector3)stream.ReceiveNext ();
			realRot = (Quaternion)stream.ReceiveNext ();
		}
	}*/
}
