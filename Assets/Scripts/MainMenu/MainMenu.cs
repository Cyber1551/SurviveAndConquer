using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public GameObject loginPanel;
	public GameObject registerPanel;
	public Button backBtn;
	public Button settingsBtn;
	public GameObject settingsPanel;



	//login
	public InputField loginUsername;
	public InputField loginPassword;


	//Register
	public InputField regUsername;
	public InputField regPassword;
	public InputField regRepeatPassword;

	public Text errorTxt;

	public GameObject userData;
	LoadDatabase ld;
	AddUser au;

	private string previousRoom = "";

	void Start()
	{
		ld = userData.GetComponent<LoadDatabase> ();
		au = userData.GetComponent<AddUser> ();
	}

	//In Login Scene
	public void LoginButton()
	{
		if (ld.Login (loginUsername.text, loginPassword.text)) {
			SceneManager.LoadScene ("MainMenu");
		} else {
			
			errorTxt.text = "Incorrect Username or Password!"; 
			Fade ();
		}
	    
	}
	/*void ShowError(string txt)
	{
		if (!errorTxt.gameObject.activeSelf) {
			errorTxt.gameObject.SetActive (true);
			errorTxt.text = txt;
			StartCoroutine (ErrorFadeOut ());
		}

	}

	IEnumerator ErrorFadeOut()
	{
		errorTxt.CrossFadeAlpha (0.0f, 3.0f, false);
		yield return new WaitForSeconds (3);
		errorTxt.gameObject.SetActive (false);
	}*/ 

	public void ToRegister()
	{
		loginPanel.gameObject.SetActive (false);
		registerPanel.gameObject.SetActive (true);
		backBtn.gameObject.SetActive (true);
		regUsername.text = "";
		regPassword.text = "";
		regRepeatPassword.text = "";
	}

	//In Register Scene
	public void BackButton()
	{
		registerPanel.gameObject.SetActive (false);
		loginPanel.gameObject.SetActive (true);
		backBtn.gameObject.SetActive (false);
		loginUsername.text = "";
		loginPassword.text = "";
	}
	public void RegisterButton()
	{
		string user = regUsername.text;
		string pass = regPassword.text;
		string rpass = regRepeatPassword.text;
		if (!user.Equals ("") && !pass.Equals ("") && !rpass.Equals ("")) {
			if (pass.Length > 4) {
				if (!ld.userExists (user)) {
					if (pass.Equals (rpass)) {
						au.CreateUser (user, pass);
						//StartCoroutine (ld.Start ());
					} else {
						errorTxt.text = "Passwords must match!";
						Fade ();
					}
				} else {
					errorTxt.text = "Username already exists!";
					Fade ();
				}
			} else {
				errorTxt.text = "Password must be longer than 5 characters!"; 
				Fade ();
			}
		} else {
			errorTxt.text = "Please enter a valid Username and password!";
			Fade ();
		}
	}

	void Fade()
	{
		errorTxt.gameObject.GetComponent<Fade> ().startFade ();
	}
	public void openSettings()
	{
		if (loginPanel.gameObject.activeSelf == true) {
			loginPanel.gameObject.SetActive (false);
			previousRoom = "login";
		}
		else if (registerPanel.gameObject.activeSelf == true) {
			registerPanel.gameObject.SetActive (false);
			backBtn.gameObject.SetActive (false);
			previousRoom = "register";
		}
		settingsPanel.gameObject.SetActive (true);
		settingsBtn.gameObject.SetActive (false);
	}
	public void closeSettings()
	{
		settingsPanel.gameObject.SetActive (false);
		settingsBtn.gameObject.SetActive (true);
		if (previousRoom.Equals ("login")) {
			loginPanel.gameObject.SetActive (true);
		} else if (previousRoom.Equals ("register")) {
			registerPanel.gameObject.SetActive (true);
			backBtn.gameObject.SetActive (true);
		}
	}
}
