using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour {



	public void startFade () {

		if (!gameObject.activeSelf) { 
			gameObject.SetActive (true);
			StartCoroutine (ErrorFadeOut ());
		}

	}

	IEnumerator ErrorFadeOut()
	{
		gameObject.GetComponent<Text>().CrossFadeAlpha (0.0f, 3.0f, false);
		yield return new WaitForSeconds (3);
		gameObject.SetActive (false);
	}
}
