using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : Photon.PunBehaviour {

	[SerializeField] private Transform spawnPoint;
	[SerializeField] private Text playText;
	[SerializeField] private GameObject player;
	[SerializeField] private GameObject world;
	[SerializeField] private GameObject menu;
	public GameObject cam;
	private AudioSource[] audio;

	public GameObject[] spawns;

	void Awake()
	{
		audio = FindObjectsOfType<AudioSource> ();
	}
	public virtual void OnJoinedRoom()
	{
		 
		playText.text = "PLAY";

		world.gameObject.SetActive (true);
		menu.gameObject.SetActive (false);
		foreach (AudioSource a in audio) {
			a.Stop ();
		}
		GameObject spawn = spawns [Random.Range (0, spawns.Length)];
		Vector3 pos = new Vector3 (spawn.transform.position.x + Random.Range (-10, 10), spawn.transform.position.y, spawn.transform.position.z + Random.Range (-10, 10));
		GameObject myPlayer = PhotonNetwork.Instantiate (player.name, pos, spawn.transform.rotation, 0);
		//cam.transform.position = pos + new Vector3 (0, 20, 0);
		//Debug.Log (pos.x + ", " + cam.transform.position.x);
		//cam.transform.LookAt (myPlayer.transform);
	}
	public void FindRoom()
	{
		playText.text = "Searching..."; 
		RoomOptions ro = new RoomOptions ();
		ro.IsOpen = true;
		ro.IsVisible = true;
		ro.MaxPlayers = 2;

		ro.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable () { { "C0", "Level" } }; 
		ro.CustomRoomPropertiesForLobby = new string[] { "C0" };

		int level = PlayerStats.Level;
		int min = level - 2;
		int max = level + 2;
		if (min < 1)
			min = 1; 
		TypedLobby sqlLobby = new TypedLobby (null, LobbyType.SqlLobby); 
		string lobbyFilter = "(C0 &gt;" + min + " AND C0 &lt; " + max + ")";
		Debug.Log (lobbyFilter);

		PhotonNetwork.JoinOrCreateRoom ("Room" + Random.Range(0, 99999), ro, sqlLobby);
		//PhotonNetwork.JoinRandomRoom (null, 2, MatchmakingMode.FillRoom, sqlLobby, lobbyFilter);
		
	} 
}
