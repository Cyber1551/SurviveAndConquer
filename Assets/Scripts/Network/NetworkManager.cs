using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NetworkManager : Photon.PunBehaviour {

	public Text loadingTxt;
	// Use this for initialization
	private void Start () {
		DontDestroyOnLoad (this.gameObject);
		PhotonNetwork.autoJoinLobby = false; 
		Debug.Log ("Start");
		PhotonNetwork.ConnectUsingSettings ("0.0.1");

	}
	void Update()
	{
		if (loadingTxt != null)
			loadingTxt.text = PhotonNetwork.connectionStateDetailed.ToString ();
	}

	public virtual void OnConnectedToMaster()
	{
		TypedLobby lobby = new TypedLobby ("Main Lobby", LobbyType.SqlLobby);
		PhotonNetwork.JoinLobby (lobby);
	}
	public virtual void OnJoinedLobby()
	{ 
		Debug.Log ("Joined Lobby");
		SceneManager.LoadScene ("LoginRegister");

	}


}
  