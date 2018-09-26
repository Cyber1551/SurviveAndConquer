using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NetworkingManager : Photon.MonoBehaviour {

    public string version = "0.0.1";
    public GameObject player;
    public GameObject lobbyCamera;
    public Text progressText;
    public Transform spawnPoint;

	void Start ()
    {
        progressText.text = "Searching...";
        PhotonNetwork.ConnectUsingSettings(version);	
	}
	public virtual void OnJoinedLobby()
    {
        
        RoomOptions ro = new RoomOptions
        {
            MaxPlayers = 2
        };
        progressText.text = "Found Match!";
        PhotonNetwork.JoinOrCreateRoom("New", ro, null);
    }
    public virtual void OnJoinedRoom()
    {
        
        lobbyCamera.SetActive(false);
        progressText.gameObject.SetActive(false);
        PhotonNetwork.Instantiate(player.name, spawnPoint.position, spawnPoint.rotation, 0);
        GameObject.Find("NetworkManager").GetComponent<AudioSource>().Stop();
    }

}
