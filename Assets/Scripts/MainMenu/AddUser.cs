using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Security.Cryptography;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AddUser : MonoBehaviour {

    public Text msg;

    private static string hash = "Th3Hash1sV3ryS3cr3tAndN0b0dyCanKn0w@12345";
	public static string Encrypt(string input)
	{

		byte[] data = UTF8Encoding.UTF8.GetBytes (input);
		using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider ()) {
			byte[] key = md5.ComputeHash (UTF8Encoding.UTF8.GetBytes (hash));
			using (TripleDESCryptoServiceProvider trip = new TripleDESCryptoServiceProvider () {
				Key = key,
				Mode = CipherMode.ECB,
				Padding = PaddingMode.PKCS7
			}) {
				ICryptoTransform tr = trip.CreateEncryptor ();
				byte[] results = tr.TransformFinalBlock (data, 0, data.Length);
				return Convert.ToBase64String (results, 0, results.Length);
			} 
		}
	}
    public void UpdateUser(string username, string update, string value)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("update", update);
        form.AddField("value", value);
        WWW www = new WWW("http://localhost/SurviveAndConquerFight/UpdateData.php", form);
    }


    public IEnumerator CreateUser(string username, string password)
	{
        
		WWWForm form = new WWWForm();
		form.AddField("username", username);
		form.AddField ("password", Encrypt(password));
        
		WWW www = new WWW ("http://localhost/SurviveAndConquerFight/InsertData.php", form);
        yield return www;
        //msg.text = "User Created"; 
        WWW userData = new WWW("http://localhost/SurviveAndConquerFight/GetData.php");
        yield return userData;

        string userString = userData.text;
        

        GameObject.Find("NetworkManager").GetComponent<LoadDatabase>().users = userString.Split(';');
        
        new PlayerData(username);
        
        //Debug.Log(PlayerData.username);
        SceneManager.LoadScene("LoginRegister");

    }
    void Fade()
    {
        msg.gameObject.GetComponent<Fade>().startFade();
    }
}
