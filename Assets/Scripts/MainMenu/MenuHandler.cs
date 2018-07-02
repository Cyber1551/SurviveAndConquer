using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour {

    public GameObject home;
    string previousRoom = "home";

    public GameObject ExitPanel;
    public Button ExitBtn;

    public Text user;
    public Text level;
    public Text gold;
    public Text honor;
    public Image expFill;

    public Image honorFill;
    

    public void Start()
    {
        user.text = PlayerData.username;
        level.text = "Level: " + PlayerData.level;
        honor.text = PlayerData.honor + "/100";
        gold.text = PlayerData.gold + "";
        honorFill.fillAmount = ((float)PlayerData.honor / 100);
        expFill.fillAmount = ((float)PlayerData.exp / PlayerData.maxExp);
        
    }
    public void exitConfirm()
    {
        
        
        ExitPanel.gameObject.SetActive(true);
        //ExitBtn.gameObject.SetActive(false);

    }
    public void quitGame()
    {

        SceneManager.LoadScene("LoginRegister");
    }
    public void cancel()
    {

        ExitPanel.gameObject.SetActive(false);
       
       
    }
}
