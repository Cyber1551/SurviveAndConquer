using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LoadUserData : MonoBehaviour {


	string username = "";

	 LoadDatabase ld;

	public Text[] userTxt;


	public Text winsTxt;
	public Text lossesTxt;
	public Text WRTxt;
	public Text levelTxt;
	public Text honorTxt;

	public Text goldTxt;

	public Image expBar;
	public Image honorBar;
	public Image WrBar;
	void Awake()
	{
		username = PlayerStats.Username;
	}
	// Use this for initialization
	void Start () {
		foreach (Text t in userTxt) {
			t.text = username;
		}
		ld = GameObject.Find ("Manager").GetComponent<LoadDatabase> ();
		string[] user = ld.getUser (username);
		winsTxt.text = "Wins: " + user [1];
		lossesTxt.text = "Losses: " + user [2];
		goldTxt.text = user [3];
		float WRPercent = 0.0f;

		if ((int.Parse(user [1]) + int.Parse(user [2])) != 0) 
		{
				
			WRPercent = ((float)int.Parse(user [1]) / (int.Parse(user [1]) + int.Parse(user [2])));
		}

		
		WRTxt.text = "Win Rate: " + Math.Round(WRPercent * 100) + "%"; 
		WrBar.fillAmount = WRPercent;

		float expPercent = ((float)int.Parse (user [4]) / int.Parse (user [5]));
		expBar.fillAmount = expPercent;
		levelTxt.text = "Level:" + user [6];

		honorBar.fillAmount = ((float)int.Parse (user [7]) / 100);
		honorTxt.text = "Honor: " + int.Parse (user [7]) + "/100";

	}
	 




}
