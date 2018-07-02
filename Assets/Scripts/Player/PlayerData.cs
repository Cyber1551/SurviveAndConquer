using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public static string username;
    public static int wins;
    public static int losses;
    public static int gold;
    public static int exp;
    public static int maxExp;
    public static int level;
    public static int honor;


    public PlayerData(string _name)
    {
        LoadDatabase ld = GameObject.Find("NetworkManager").GetComponent<LoadDatabase>();
        string user = ld.getUserString(_name);
        Debug.Log(user);
        if (user != null)
        {
            username = _name;
            wins = int.Parse(ld.GetDataValue(user, "Wins:"));
            losses = int.Parse(ld.GetDataValue(user, "Losses:"));
            gold = int.Parse(ld.GetDataValue(user, "Gold:"));
            exp = int.Parse(ld.GetDataValue(user, "Exp:"));
            maxExp = int.Parse(ld.GetDataValue(user, "ExpMax:"));
            level = int.Parse(ld.GetDataValue(user, "Level:"));
            honor = int.Parse(ld.GetDataValue(user, "Honor:"));
            Debug.Log(user);
        }
        

    }
}
