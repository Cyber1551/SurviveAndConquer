using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour {

    public Text[] user;
    public Text[] level;
    public Text gold;
    public Text honor;
    public Text expTxt;
    public Image expFill;
    public Text wins;
    public Text losses;
    public Text winRate;
    public Image honorFill;
    

    public void Start()
    {
        foreach(Text t in user)
        {
            t.text = PlayerData.username;
        }
        
        level[0].text = "Level: " + PlayerData.level;
        level[1].text = PlayerData.level + "";
        //honor.text = PlayerData.honor + "/100";
        gold.text = "Gold: " + PlayerData.gold;
        wins.text = "Wins: " + PlayerData.wins;
        losses.text = "Losses: " + PlayerData.losses;
        int totalGames = PlayerData.wins + PlayerData.losses;
        string wr = (totalGames == 0) ? "" + 0 : "" + PlayerData.wins / totalGames;
        winRate.text = "Win Rate: " + wr;
        //honorFill.fillAmount = ((float)PlayerData.honor / 100);
        expTxt.text = PlayerData.exp + "/" + PlayerData.maxExp;
        expFill.fillAmount = ((float)PlayerData.exp / PlayerData.maxExp);
        
    }
}
