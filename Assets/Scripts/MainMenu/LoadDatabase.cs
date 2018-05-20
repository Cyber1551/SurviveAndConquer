using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Security.Cryptography;
using System;

public class LoadDatabase : MonoBehaviour {
	private string[] users;

	// Use this for initialization
	IEnumerator Start () {
		WWW userData = new WWW ("http://localhost/SurviveAndConquerMoba/GetData.php");
		yield return userData;
		string userString = userData.text;
		 
		users = userString.Split (';'); 


	}
		
	/*private static string hash = "string!12345@";
	public static string Decrypt(string input)
	{

		byte[] data = Convert.FromBase64String (input);
		using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider ()) {
			byte[] key = md5.ComputeHash (UTF8Encoding.UTF8.GetBytes (hash));
			using (TripleDESCryptoServiceProvider trip = new TripleDESCryptoServiceProvider () {
				Key = key,
				Mode = CipherMode.ECB,
				Padding = PaddingMode.PKCS7
			}) {
				ICryptoTransform tr = trip.CreateDecryptor ();
				byte[] results = tr.TransformFinalBlock (data, 0, data.Length);
				return UTF8Encoding.UTF8.GetString(results);
			} 
		}
	}*/

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

		

	string GetDataValue(string data, string index)
	{
		if (data != "") {

			string value = data.Substring (data.IndexOf (index) + index.Length);
			if (value.Contains ("|")) {
				value = value.Remove (value.IndexOf ('|'));


			}

			return value;
		} else {
			return "ERROR";
		}
	}

	public bool Login(string username, string password)
	{
		if (!username.Equals ("") && !password.Equals ("")) 
		{
			
			foreach (string user in users) {
				print (GetDataValue (user, "Pass:").Equals (Encrypt(password)));
				if (GetDataValue (user, "User:").Equals(username)) {
					if (GetDataValue (user, "Pass:").Equals(Encrypt(password))) {
						return true;
					} else {
						return false;
					}
				}
				else {
					return false;
				}
			} 
			return false;
		}
		else {
			return false;
		}
	}


	public bool userExists(string username)
	{
		

		foreach (string user in users) {
			
				if (user != "") {
					if (GetDataValue (user , "User:").Equals (username)) {
						return true;
					}
				}
			}
		return false;

		}


}