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
	private bool searching = false;
	public GameObject cam;
	private int joinAttempts = 0;
	private int maximumSkillDeviation = 3;
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

		searching = true;


	
		
	}

	void Update()
	{
		if (searching) {

			if (PhotonNetwork.inRoom == false) {
				if (joinAttempts < maximumSkillDeviation) {
					Debug.Log ("Join");
					string sqlLobbyFilter = SQLSearch ();
					if (PhotonNetwork.JoinRandomRoom (null, 2, MatchmakingMode.FillRoom, PhotonNetwork.lobby, sqlLobbyFilter)) {
						searching = false;
					}
					joinAttempts++;
				} else {
					Debug.Log ("Create room");
					searching = false;
					CreateServer ();
				}
			}

		}
	}
	public void CreateServer()
	{
		ExitGames.Client.Photon.Hashtable sqlProperties = new ExitGames.Client.Photon.Hashtable ();


		sqlProperties.Add ("C0", PlayerStats.Level);
		string[] lobbyProperties = new string[] { "C0" };

		RoomOptions ro = new RoomOptions ();
		ro.CustomRoomPropertiesForLobby = lobbyProperties;
		ro.CustomRoomProperties = sqlProperties;
		PhotonNetwork.CreateRoom ("Room" + Random.Range (0, 9999), ro, PhotonNetwork.lobby);
	}

	string SQLSearch()
	{
		string possibleSearchResult = "(C0 > 0) AND (C0 < 5)";

		int skill = joinAttempts + 1;

		return 
			"(C0 > " + (PlayerStats.Level - skill) + ") AND (C0 < " + (PlayerStats.Level + skill) + ")";
	}
}
