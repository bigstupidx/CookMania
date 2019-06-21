using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;
using Facebook.Unity ;
using System.Linq;

public class RestorePurchase : MonoBehaviour {

	public static RestorePurchase _instance ;

	public static bool generatedOnce;

	public static string baseUrl = "http://emptask.com/foody/site/";

	// Use this for initialization
	void Start () {

		if(!generatedOnce)
		{
			_instance = this;
			DontDestroyOnLoad(this.gameObject);
			if(Application.internetReachability != NetworkReachability.NotReachable)
			{
				StartCoroutine ("RestoreData");
			}
			generatedOnce = true;
		}
		else
		{
			Destroy (gameObject);
		}
	}

	IEnumerator FetchMyData()
	{
		//FacebookHandler._instance.loader.SetActive (true);
		string url = baseUrl + "update";
		WWWForm wForm = new WWWForm ();
		wForm.AddField ("access_token", AccessToken.CurrentAccessToken.TokenString);
		WWW w = new WWW (url, wForm);
		yield return w;
		Debug.Log ("1"+w.text);

	}


	IEnumerator RestoreData()
	{
		string url = baseUrl + "getDetail";
		WWW w = new WWW (url);
		yield return w;
	
		Debug.Log (w.text);


		if (w.error == null) {
			Debug.Log ("error"+w.text);

			IDictionary responseDic = (IDictionary)Json.Deserialize (w.text);

			if (w.text.Contains ("GrilUpLevel")) 
			{
				PlayerPrefs.SetString ("GrillsUpgrade",EncryptionHandler64.Encrypt ("1"));
				Debug.Log("Done this ");
			
//				PlayerPrefs.SetString ("USCokeUpgrade", EncryptionHandler64.Encrypt ("0"));
//				PlayerPrefs.SetString ("PlateUpgrade", EncryptionHandler64.Encrypt ("0"));
//				
//				
//				PlayerPrefs.SetInt ("US/base-flat-1", 1);
//				PlayerPrefs.SetInt ("US/top-floor-1", 1);
//				
//
//				PlayerPrefs.SetString ("China_TableCover", "China/1");
//				PlayerPrefs.SetString ("China_TableTop", "China/a-1");
//				PlayerPrefs.SetString ("ChinaPlateUpgrade", EncryptionHandler64.Encrypt ("0"));
//				PlayerPrefs.SetString ("ChinaBowlsUpgrade", EncryptionHandler64.Encrypt ("0"));
//				PlayerPrefs.SetString ("ChinaPansUpgrade", EncryptionHandler64.Encrypt ("0"));
//				PlayerPrefs.SetString ("ChinaSoupContainer", EncryptionHandler64.Encrypt ("0"));
//				
//				PlayerPrefs.SetString ("Italy_TableCover", "Italy/1");
//				PlayerPrefs.SetString ("Italy_TableTop", "Italy/top-strip-1");
//				PlayerPrefs.SetString ("ItalyPlateUpgrade", EncryptionHandler64.Encrypt ("0"));
//				PlayerPrefs.SetString ("ItalyCokeUpgrade", EncryptionHandler64.Encrypt ("0"));
//				PlayerPrefs.SetString ("OvenUpgrade", EncryptionHandler64.Encrypt ("0"));
//				PlayerPrefs.SetInt ("Italy/1", 1);
//				PlayerPrefs.SetInt ("Italy/top-strip-1", 1);
//				
//				PlayerPrefs.SetInt ("China/1", 1);
//				PlayerPrefs.SetInt ("China/a-1", 1);
//				
//				
//				
//				PlayerPrefs.SetString ("AusPlateUpgrade", EncryptionHandler64.Encrypt ("0"));
//				PlayerPrefs.SetString ("FriesUpgrade", EncryptionHandler64.Encrypt ("0"));
//				PlayerPrefs.SetString ("AusCokeUpgrade", EncryptionHandler64.Encrypt ("0"));
//				PlayerPrefs.SetString ("AusGrillsUpgrade", EncryptionHandler64.Encrypt ("0"));
//				
//				PlayerPrefs.SetInt ("Aus/1", 1);
//				PlayerPrefs.SetInt ("Aus/top-shed-1", 1);
			} else {
				Debug.Log ("error"+w.text);
			}

		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
