using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupText : MonoBehaviour {
    public Animator animator;
    private Text amount;

	// Use this for initialization
	void Awake () {
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        Destroy(gameObject, clipInfo[0].clip.length);
        amount = animator.GetComponent<Text>();
	}
	
    public void SetText(string text)
    {
        amount.text = text;
    }
}
