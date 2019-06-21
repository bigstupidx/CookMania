using UnityEngine;
using System.Collections;
using MiniJSON;
using UnityEngine.UI;
using Facebook.Unity ;
using System.Collections.Generic;
using System.Linq;
using System;
using Facebook.Unity.Canvas;
using Facebook.Unity.Editor;
using Facebook.Unity.Mobile;
using Facebook.Unity.Mobile.Android;
using Facebook.Unity.Mobile.IOS;
using UnityEngine;


public class FacebookHandler : MonoBehaviour {
	string baseUrl = "Please insert your server site link here";
	bool initializationComplete;
	
	public GameObject fbPopup , popupPanel , levelsExceedPopup ;
	public GameObject loader;
	public Text popupText;
	public UILabel difficultyLabel;
	
	public Button userLevelsButton;
	
	public static bool readyToProceed , noOfLevelsCreatedFound;
	public static int noOfLevelsCreated;
	
	public static FacebookHandler _instance;
	bool allowPauseFb;
	public Image circular_loader ;
	public float circular_time ;
	public GameObject circular_parent ;
	// Use this for initialization

	void Awake()
	{
		if (_instance == null) {
			_instance = this;
			DontDestroyOnLoad (gameObject);
			Invoke ("FbPauseAllow" , 3.0f);
			CallFBInit ();
		} else {
			Destroy (gameObject);
		}
	}
	void Start () {


	
	
	
		}




	public void FBAppinvite()
	{

		fbPopup.SetActive (false);
		if (FB.IsLoggedIn) {

//			FB.AppRequest(
//				"Come play this great game!",
//				null, null, null, null, null, null,
//				delegate (IAppRequestResult result) {
//				Debug.Log(result.RawResult);
//			}
//			);
//			FB.Mobile.AppInvite (
//				new Uri ("https://fb.me/556189471196620"),null);
			Debug.Log ("invite");
			FB.ShareLink(
				new Uri("Please insert your Appstore link here"),
			null);


		} else {
			popupPanel.SetActive(true);
			popupText.text = "Loign In FaceBook";
		}
	}



	void FbPauseAllow()
	{
		allowPauseFb = true;
	}

	void OnApplicationPause (bool pauseStatus)
	{
		// Check the pauseStatus to see if we are in the foreground
		// or background
		if (!pauseStatus && allowPauseFb) {
			
			Debug.Log ("activate in puase");
			//app resume
			if (FB.IsInitialized) {
				FB.ActivateApp();
			} else {
				//Handle FB.Init
				FB.Init(OnInitComplete, OnHideUnity => {
					FB.ActivateApp();
				});
			}
		}
	}

	public void Disable_popup_panel()
	{
		popupPanel.SetActive (false);
	}
	private void CallFBInit()
	{
		FB.Init(OnInitComplete, OnHideUnity);

	}
	
	private void OnInitComplete()
	{
		Debug.Log("FB.Init completed: Is user logged in? " + FB.IsLoggedIn);
		if(FB.IsLoggedIn)
		{
			//send details
			StartCoroutine(SendLoginData());
		}
		else
		{
			Debug.Log("0---------init complete");
			//userLevelsButton.interactable = true;
			initializationComplete = true;
		}
	}
	private void OnHideUnity(bool isGameShown)
	{
		Debug.Log("Is game showing? " + isGameShown);
	}
	
	
	IEnumerator SendLoginData()
	{
		Debug.Log ("FB.IsLoggedIn  =  "+FB.IsLoggedIn);
		if (FB.IsLoggedIn && Application.internetReachability != NetworkReachability.NotReachable) {
			string url = baseUrl + "getDetail";
			WWWForm wForm = new WWWForm ();
			Debug.Log("access token    == =    "+AccessToken.CurrentAccessToken.TokenString);
			wForm.AddField ("access_token", AccessToken.CurrentAccessToken.TokenString);
			WWW wLogin = new WWW (url, wForm);
			yield return wLogin;
			Debug.Log ("aa" + wLogin.text);
			if (wLogin.error == null) {
				Debug.Log (wLogin.text);
				if (wLogin.text.Contains ("status")) {
					IDictionary responseDic = (IDictionary)Json.Deserialize (wLogin.text);
					string status = responseDic ["status"].ToString ();
					string message = responseDic ["message"].ToString ();
					if (status == "success") {
						PlayerPrefs.SetInt ("FBLogin", 1);
						if (message.Contains ("New user created.")) {
							StartCoroutine (SendUpdatedData ());


							//Send Data
						} else {
							Debug.Log (message);

							IDictionary dataDic = (IDictionary)responseDic ["message"];

							Debug.Log (dataDic ["ColaTraysLevel"].ToString ());

							int a = int.Parse (dataDic ["ColaTraysLevel"].ToString ());


							Debug.Log ("yes" + PlayerPrefs.GetString ("HotTraysLevel").ToString ());

							int grilUpLevel = 0;
							int.TryParse (dataDic ["GrilUpLevel"].ToString (), out grilUpLevel);

							if (grilUpLevel > (int)EncryptionHandler64.Decrypt (PlayerPrefs.GetString ("GrillsUpgrade"))) {


								PlayerPrefs.SetString ("GrillsUpgrade", EncryptionHandler64.Encrypt (dataDic ["GrilUpLevel"].ToString ()));

							}
							int colaTraysLevel = 0;
							int.TryParse (dataDic ["ColaTraysLevel"].ToString (), out colaTraysLevel);
							if (colaTraysLevel > (int)EncryptionHandler64.Decrypt (PlayerPrefs.GetString ("USCokeUpgrade"))) {
							
								PlayerPrefs.SetString ("USCokeUpgrade", EncryptionHandler64.Encrypt (dataDic ["ColaTraysLevel"].ToString ()));
							}

							int hotTrayLevels = 0;
							int.TryParse (dataDic ["HotTraysLevel"].ToString (), out hotTrayLevels);

							if (hotTrayLevels > (int)EncryptionHandler64.Decrypt (PlayerPrefs.GetString ("PlateUpgrade"))) {
							
								PlayerPrefs.SetString ("PlateUpgrade", EncryptionHandler64.Encrypt (dataDic ["HotTraysLevel"].ToString ()));
							}

							int soupBowlLevel = 0;
							int.TryParse (dataDic ["SoupBowlLevel"].ToString (), out soupBowlLevel);

							if (soupBowlLevel > (int)EncryptionHandler64.Decrypt (PlayerPrefs.GetString ("ChinaBowlsUpgrade"))) {
							
								PlayerPrefs.SetString ("ChinaBowlsUpgrade", EncryptionHandler64.Encrypt (dataDic ["SoupBowlLevel"].ToString ()));
							}

							int skilletLevel = 0;
							int.TryParse (dataDic ["SkilletLevel"].ToString (), out skilletLevel);
							if (skilletLevel > (int)EncryptionHandler64.Decrypt (PlayerPrefs.GetString ("ChinaPansUpgrade"))) {
							
								PlayerPrefs.SetString ("ChinaPansUpgrade", EncryptionHandler64.Encrypt (dataDic ["SkilletLevel"].ToString ()));
							}
							int stockPotLevel = 0;
							int.TryParse (dataDic ["StockPotLevel"].ToString (), out stockPotLevel);
							if (stockPotLevel > (int)EncryptionHandler64.Decrypt (PlayerPrefs.GetString ("ChinaSoupContainerUpgrade"))) {
							
								PlayerPrefs.SetString ("ChinaSoupContainerUpgrade", EncryptionHandler64.Encrypt (dataDic ["StockPotLevel"].ToString ()));
							}
							int noodleDishesLevel = 0;
							int.TryParse (dataDic ["NoodleDishesLevel"].ToString (), out noodleDishesLevel);
							if (noodleDishesLevel > (int)EncryptionHandler64.Decrypt (PlayerPrefs.GetString ("ChinaPlateUpgrade"))) {
							
								PlayerPrefs.SetString ("ChinaPlateUpgrade", EncryptionHandler64.Encrypt (dataDic ["NoodleDishesLevel"].ToString ()));
							}
							int colaTraysItalyLevel = 0;
							int.TryParse (dataDic ["ColaTraysItalyLevel"].ToString (), out colaTraysItalyLevel);
							if (colaTraysItalyLevel > (int)EncryptionHandler64.Decrypt (PlayerPrefs.GetString ("ItalyCokeUpgrade"))) {
							
								PlayerPrefs.SetString ("ItalyCokeUpgrade", EncryptionHandler64.Encrypt (dataDic ["ColaTraysItalyLevel"].ToString ()));
							}
							int dishesLevel = 0;
							int.TryParse (dataDic ["DishesLevel"].ToString (), out dishesLevel);
							if (dishesLevel > (int)EncryptionHandler64.Decrypt (PlayerPrefs.GetString ("ItalyPlateUpgrade"))) {
							
								PlayerPrefs.SetString ("ItalyPlateUpgrade", EncryptionHandler64.Encrypt (dataDic ["DishesLevel"].ToString ()));
							}
							int ovenLevel = 0;
							int.TryParse (dataDic ["OvenLevel"].ToString (), out ovenLevel);
							if (ovenLevel > (int)EncryptionHandler64.Decrypt (PlayerPrefs.GetString ("OvenUpgrade"))) {
							
								PlayerPrefs.SetString ("OvenUpgrade", EncryptionHandler64.Encrypt (dataDic ["OvenLevel"].ToString ()));
							}
							int dishesAusLevel = 0;
							int.TryParse (dataDic ["DishesAusLevel"].ToString (), out dishesAusLevel);
							if (dishesAusLevel > (int)EncryptionHandler64.Decrypt (PlayerPrefs.GetString ("AusPlateUpgrade"))) {
							
								PlayerPrefs.SetString ("AusPlateUpgrade", EncryptionHandler64.Encrypt (dataDic ["DishesAusLevel"].ToString ()));
							}

							int colaTraysAusLevel = 0;
							int.TryParse (dataDic ["ColaTraysAusLevel"].ToString (), out colaTraysAusLevel);
							if (colaTraysAusLevel > (int)EncryptionHandler64.Decrypt (PlayerPrefs.GetString ("AusCokeUpgrade"))) {
							
								PlayerPrefs.SetString ("AusCokeUpgrade", EncryptionHandler64.Encrypt (dataDic ["ColaTraysAusLevel"].ToString ()));
							}

							int deepFrierLevel = 0;
							int.TryParse (dataDic ["DeepFrierLevel"].ToString (), out deepFrierLevel);
							if (deepFrierLevel > (int)EncryptionHandler64.Decrypt (PlayerPrefs.GetString ("FriesUpgrade"))) {
							
								PlayerPrefs.SetString ("FriesUpgrade", EncryptionHandler64.Encrypt (dataDic ["DeepFrierLevel"].ToString ()));
							}

							int grillUpAusLevel = 0;
							int.TryParse (dataDic ["GrillUpAusLevel"].ToString (), out grillUpAusLevel);

							if (grillUpAusLevel > (int)EncryptionHandler64.Decrypt (PlayerPrefs.GetString ("AusGrillsUpgrade"))) {
							
								PlayerPrefs.SetString ("AusGrillsUpgrade", EncryptionHandler64.Encrypt (dataDic ["GrillUpAusLevel"].ToString ()));
							}

							int fridgeLevel = 0;
							int.TryParse (dataDic ["FridgeLevel"].ToString (), out fridgeLevel);
							if (fridgeLevel > (PlayerPrefs.GetInt ("Fridge"))) {
							
								PlayerPrefs.SetInt ("Fridge", int.Parse (dataDic ["FridgeLevel"].ToString ()));
							}

							int usTableCover = 0;
							int.TryParse (dataDic ["UsTableCover"].ToString (), out usTableCover);
							if (usTableCover > (PlayerPrefs.GetInt ("US/base-flat-2"))) {
							
								PlayerPrefs.SetInt ("US/base-flat-2", int.Parse (dataDic ["UsTableCover"].ToString ()));
							}

							int usTableCover3 = 0;
							int.TryParse (dataDic ["UsTableCover3"].ToString (), out usTableCover3);
							if (usTableCover3 > (PlayerPrefs.GetInt ("US/base-flat-3"))) {
							
								PlayerPrefs.SetInt ("US/base-flat-3", int.Parse (dataDic ["UsTableCover3"].ToString ()));
							}

							int usTableCover4 = 0;
							int.TryParse (dataDic ["UsTableCover4"].ToString (), out usTableCover4);
							if (usTableCover4 > (PlayerPrefs.GetInt ("US/base-flat-4"))) {
							
								PlayerPrefs.SetInt ("US/base-flat-4", int.Parse (dataDic ["UsTableCover4"].ToString ()));
							}

							int chinaTableCover = 0;
							int.TryParse (dataDic ["ChinaTableCover"].ToString (), out chinaTableCover);
							if (chinaTableCover > (PlayerPrefs.GetInt ("China/2"))) {
							
								PlayerPrefs.SetInt ("China/2", int.Parse (dataDic ["ChinaTableCover"].ToString ()));
							}
						
							int chinaTableCover3 = 0;
							int.TryParse (dataDic ["ChinaTableCover3"].ToString (), out chinaTableCover3);
							if (chinaTableCover3 > (PlayerPrefs.GetInt ("China/3"))) {
							
								PlayerPrefs.SetInt ("China/3", int.Parse (dataDic ["ChinaTableCover3"].ToString ()));
							}
						
							int chinaTableCover4 = 0;
							int.TryParse (dataDic ["ChinaTableCover4"].ToString (), out chinaTableCover4);
							if (usTableCover4 > (PlayerPrefs.GetInt ("China/4"))) {
							
								PlayerPrefs.SetInt ("China/4", int.Parse (dataDic ["ChinaTableCover4"].ToString ()));
							}

							int chinaTableTop = 0;
							int.TryParse (dataDic ["ChinaTableTop"].ToString (), out chinaTableTop);
							if (chinaTableTop > (PlayerPrefs.GetInt ("China/a-2"))) {
							
								PlayerPrefs.SetInt ("China/a-2", int.Parse (dataDic ["ChinaTableTop"].ToString ()));
							}
						
							int chinaTableTop3 = 0;
							int.TryParse (dataDic ["ChinaTableTop3"].ToString (), out chinaTableTop3);
							if (chinaTableTop3 > (PlayerPrefs.GetInt ("China/a-3"))) {
							
								PlayerPrefs.SetInt ("China/a-3", int.Parse (dataDic ["ChinaTableTop3"].ToString ()));
							}
						
							int chinaTableTop4 = 0;
							int.TryParse (dataDic ["ChinaTableTop4"].ToString (), out chinaTableTop4);
							if (chinaTableTop4 > (PlayerPrefs.GetInt ("China/a-4"))) {
							
								PlayerPrefs.SetInt ("China/a-4", int.Parse (dataDic ["ChinaTableTop4"].ToString ()));
							}

							int italyTableTop = 0;
							int.TryParse (dataDic ["ItalyTableTop"].ToString (), out italyTableTop);
							if (italyTableTop > (PlayerPrefs.GetInt ("Italy/top-strip-2"))) {
							
								PlayerPrefs.SetInt ("Italy/top-strip-2", int.Parse (dataDic ["ItalyTableTop"].ToString ()));
							}

							int italyTableCover = 0;
							int.TryParse (dataDic ["ItalyTableCover"].ToString (), out italyTableCover);
							if (italyTableCover > (PlayerPrefs.GetInt ("Italy/2"))) {
							
								PlayerPrefs.SetInt ("Italy/2", int.Parse (dataDic ["ItalyTableCover"].ToString ()));
							}

							int italyTableCover3 = 0;
							int.TryParse (dataDic ["ItalyTableCover3"].ToString (), out italyTableCover3);
							if (italyTableCover3 > (PlayerPrefs.GetInt ("Italy/3"))) {
							
								PlayerPrefs.SetInt ("Italy/3", int.Parse (dataDic ["ItalyTableCover3"].ToString ()));
							}

							int italyTableCover4 = 0;
							int.TryParse (dataDic ["ItalyTableCover3"].ToString (), out italyTableCover4);
							if (italyTableCover4 > (PlayerPrefs.GetInt ("Italy/4"))) {
							
								PlayerPrefs.SetInt ("Italy/4", int.Parse (dataDic ["ItalyTableCover4"].ToString ()));
							}

							int AusTableTop = 0;
							int.TryParse (dataDic ["AusTableTop"].ToString (), out AusTableTop);
							if (AusTableTop > (PlayerPrefs.GetInt ("Aus/top-shed-2"))) {
							
								PlayerPrefs.SetInt ("Aus/top-shed-2", int.Parse (dataDic ["AusTableTop"].ToString ()));
							}

							int ausTableColor = 0;
							int.TryParse (dataDic ["AusTableColor"].ToString (), out ausTableColor);
							if (ausTableColor > (PlayerPrefs.GetInt ("Aus/2"))) {
							
								PlayerPrefs.SetInt ("Aus/2", int.Parse (dataDic ["AusTableColor"].ToString ()));
							}
							int ausTableColor3 = 0;
							int.TryParse (dataDic ["AusTableColor3"].ToString (), out ausTableColor3);
							if (ausTableColor3 > (PlayerPrefs.GetInt ("Aus/3"))) {
							
								PlayerPrefs.SetInt ("Aus/3", int.Parse (dataDic ["AusTableColor3"].ToString ()));
							}
							int ausTableColor4 = 0;
							int.TryParse (dataDic ["AusTableColor4"].ToString (), out ausTableColor4);
							if (ausTableColor4 > (PlayerPrefs.GetInt ("Aus/4"))) {
							
								PlayerPrefs.SetInt ("Aus/4", int.Parse (dataDic ["AusTableColor4"].ToString ()));
							}

							int bell = 0;
							int.TryParse (dataDic ["Bell"].ToString (), out bell);
							if (bell > (PlayerPrefs.GetInt ("Bell"))) {
							
								PlayerPrefs.SetInt ("Bell", int.Parse (dataDic ["Bell"].ToString ()));
							}

							int radio = 0;
							int.TryParse (dataDic ["Radio"].ToString (), out radio);
							if (radio > (PlayerPrefs.GetInt ("Radio"))) {
							
								PlayerPrefs.SetInt ("Radio", int.Parse (dataDic ["Radio"].ToString ()));
							}

							int special = 0;
							int.TryParse (dataDic ["Special"].ToString (), out special);
							if (special > (PlayerPrefs.GetInt ("Whistle"))) {
							
								PlayerPrefs.SetInt ("Whistle", int.Parse (dataDic ["Special"].ToString ()));
							}

							Debug.Log (dataDic ["ColaTraysLevel"].ToString ());


							Debug.Log (dataDic ["GrilUpLevel"].ToString ());
							Debug.Log (dataDic ["SoupBowlLevel"].ToString ());
							//GET DATA
							//Update data
							StartCoroutine (SendUpdatedData ());
						}

						readyToProceed = true;
						if (callingAfterLogin)
						//UIManager.Instance.ActivePanelForThisKeyWithFadding("UserLevel");
							Debug.Log (message);
					} else {
						Debug.Log (responseDic ["message"].ToString ());
						if (callingAfterLogin) {
							FB.LogOut ();
							PlayerPrefs.SetInt ("FBLogin", 0);
							//popupPanel.SetActive (true);
//						popupText.text = responseDic["message"].ToString();
						}
					}
				
				}
			}
		}
		//StartCoroutine(DeactivateLoader());
	}

	public void send_single_key(string key,string val)
	{
		StartCoroutine(SendSingleKey(key,val));
	}


	public IEnumerator SendSingleKey(string keyName , string valName)
	{

		if (FB.IsLoggedIn && Application.internetReachability != NetworkReachability.NotReachable) 
		{

		

			string url = baseUrl + "update";
			WWWForm wForm = new WWWForm ();
		
			wForm.AddField ("access_token", AccessToken.CurrentAccessToken.TokenString);
		
			wForm.AddField ("key", keyName);
			wForm.AddField ("value", valName);

			WWW wLogin = new WWW (url, wForm);
		

			yield return wLogin;
	
		
			if (wLogin.error == null) {
				Debug.Log (wLogin.text);
				if (wLogin.text.Contains ("status")) {
					IDictionary responseDic = (IDictionary)Json.Deserialize (wLogin.text);
					string status = responseDic ["status"].ToString ();
					if (status == "success") {
						Debug.Log (responseDic ["message"].ToString ());
					} else {
						Debug.Log (responseDic ["message"].ToString ());
					}
				
				}
			}
		}
	}


	IEnumerator SendUpdatedData()
	{
		if (FB.IsLoggedIn && Application.internetReachability != NetworkReachability.NotReachable) {

			string url = baseUrl + "create";
			WWWForm wForm = new WWWForm ();
			wForm.AddField ("access_token", AccessToken.CurrentAccessToken.TokenString);
			//23 keys
	

			wForm.AddField ("GrilUpLevel", EncryptionHandler64.Decrypt (PlayerPrefs.GetString ("GrillsUpgrade")).ToString ());
			wForm.AddField ("ColaTraysLevel", EncryptionHandler64.Decrypt (PlayerPrefs.GetString ("USCokeUpgrade")).ToString ());
			wForm.AddField ("HotTraysLevel", EncryptionHandler64.Decrypt (PlayerPrefs.GetString ("PlateUpgrade")).ToString ());
			wForm.AddField ("SoupBowlLevel", EncryptionHandler64.Decrypt (PlayerPrefs.GetString ("ChinaBowlsUpgrade")).ToString ());
			wForm.AddField ("SkilletLevel", EncryptionHandler64.Decrypt (PlayerPrefs.GetString ("ChinaPansUpgrade")).ToString ());
			wForm.AddField ("StockPotLevel", EncryptionHandler64.Decrypt (PlayerPrefs.GetString ("ChinaSoupContainerUpgrade")).ToString ());
			wForm.AddField ("NoodleDishesLevel", EncryptionHandler64.Decrypt (PlayerPrefs.GetString ("ChinaPlateUpgrade")).ToString ());
			wForm.AddField ("ColaTraysItalyLevel", EncryptionHandler64.Decrypt (PlayerPrefs.GetString ("ItalyCokeUpgrade")).ToString ());
			wForm.AddField ("DishesLevel", EncryptionHandler64.Decrypt (PlayerPrefs.GetString ("ItalyPlateUpgrade")).ToString ());
			wForm.AddField ("OvenLevel", EncryptionHandler64.Decrypt (PlayerPrefs.GetString ("OvenUpgrade")).ToString ());
			wForm.AddField ("DishesAusLevel", EncryptionHandler64.Decrypt (PlayerPrefs.GetString ("AusPlateUpgrade")).ToString ());
			wForm.AddField ("ColaTraysAusLevel", EncryptionHandler64.Decrypt (PlayerPrefs.GetString ("AusCokeUpgrade")).ToString ());
			wForm.AddField ("DeepFrierLevel", EncryptionHandler64.Decrypt (PlayerPrefs.GetString ("FriesUpgrade")).ToString ());
			wForm.AddField ("GrillUpAusLevel", EncryptionHandler64.Decrypt (PlayerPrefs.GetString ("AusGrillsUpgrade")).ToString ());
			wForm.AddField ("FridgeLevel", PlayerPrefs.GetInt ("Fridge").ToString ());


			wForm.AddField ("UsTableCover", PlayerPrefs.GetInt ("US/base-flat-2").ToString ());
			wForm.AddField ("UsTableCover3", PlayerPrefs.GetInt ("US/base-flat-3").ToString ());
			wForm.AddField ("UsTableCover4", PlayerPrefs.GetInt ("US/base-flat-4").ToString ());

			wForm.AddField ("ChinaTableTop", PlayerPrefs.GetInt ("China/a-2").ToString ());
			wForm.AddField ("ChinaTableTop3", PlayerPrefs.GetInt ("China/a-3").ToString ());
			wForm.AddField ("ChinaTableTop4", PlayerPrefs.GetInt ("China/a-4").ToString ());

			wForm.AddField ("ChinaTableCover", PlayerPrefs.GetInt ("China/2").ToString ());
			wForm.AddField ("ChinaTableCover3", PlayerPrefs.GetInt ("China/3").ToString ());
			wForm.AddField ("ChinaTableCover4", PlayerPrefs.GetInt ("China/4").ToString ());


			wForm.AddField ("ItalyTableTop", PlayerPrefs.GetInt ("Italy/top-strip-2").ToString ());
		
			wForm.AddField ("ItalyTableCover", PlayerPrefs.GetInt ("Italy/2").ToString ());
			wForm.AddField ("ItalyTableCover3", PlayerPrefs.GetInt ("Italy/3").ToString ());
			wForm.AddField ("ItalyTableCover4", PlayerPrefs.GetInt ("Italy/4").ToString ());

			wForm.AddField ("AusTableTop", PlayerPrefs.GetInt ("Aus/top-shed-2").ToString ());
		
			wForm.AddField ("AusTableColor", PlayerPrefs.GetInt ("Aus/2").ToString ());
			wForm.AddField ("AusTableColor3", PlayerPrefs.GetInt ("Aus/3").ToString ());
			wForm.AddField ("AusTableColor4", PlayerPrefs.GetInt ("Aus/4").ToString ());

			wForm.AddField ("Radio", PlayerPrefs.GetInt ("Radio").ToString ());
			wForm.AddField ("Bell", PlayerPrefs.GetInt ("Bell").ToString ());
			wForm.AddField ("Special", PlayerPrefs.GetInt ("Whistle").ToString ());


			Debug.LogError ("aaaaaaaaaaaaaa");
			//Debug.Log(PlayerPrefs.GetString("Aus_TableTop"));

			WWW wLogin = new WWW (url, wForm);
			yield return wLogin;
			if (wLogin.error == null) {
				Debug.Log ("aaa" + wLogin.text);
				if (wLogin.text.Contains ("status")) {
					IDictionary responseDic = (IDictionary)Json.Deserialize (wLogin.text);
					string status = responseDic ["status"].ToString ();
					if (status == "success") {
						Debug.Log (responseDic ["message"].ToString ());
					} else {
						Debug.Log (responseDic ["message"].ToString ());
					}
				
				}
			}
		}
	}



	
	IEnumerator DeactivateLoader()
	{
		float timer = 0.0f;
		while(timer < 1.0f)
		{
			timer+=0.02f;
			yield return 0;
		}
//		loader.SetActive (false);
	}
	
	
	
	
	public void CallFbLogin()
	{
		//if(!Loader.loading)
		{
			if(Application.internetReachability != NetworkReachability.NotReachable)
			{
				if(FB.IsLoggedIn)
				{
					if(readyToProceed)
					{
						//UIManager.Instance.ActivePanelForThisKeyWithFadding("UserLevel");
					}
					else
					{
						popupPanel.SetActive (true);
						popupText.text = "Connecting to Server..\nTry again Later!";
					}
				}
				else
				{
					if(PlayerPrefs.GetInt ("FBLogin") == 1 && !initializationComplete)
					{
						popupPanel.SetActive (true);
						popupText.text = "Initializing facebook..\nTry again Later!";
					}
					else{
					}
						fbPopup.SetActive (true);
				}
			}
			else
			{
				popupPanel.SetActive (true);
				popupText.text = "No internet \n Connection";
			}
		}
	}
	

	public void EnableFB()
	{
		fbPopup.SetActive (true);

	}

	public void DisableFB()
	{
		fbPopup.SetActive (false);
	}

	public void FbLogin()
	{
		fbPopup.SetActive (false);
		if (!FB.IsLoggedIn) {
			if (Application.internetReachability != NetworkReachability.NotReachable) {
				if (initializationComplete) {
					FB.LogInWithReadPermissions (new List<string> () { "public_profile", "email", "user_friends" }, this.LoginCallback);
				}
			} else {
				popupPanel.SetActive (true);
				popupText.text = "No Internet Connection.";
			}
		} else {
			popupPanel.SetActive (true);
			popupText.text = "Already Login.";
		}
	}
	
	string lastResponse;
	bool callingAfterLogin;



	public void LoginCallback(IResult result)
	{
//		if (result.Error != null)
//			lastResponse = "Error Response:\n" + result.Error;
//		else if (!FB.IsLoggedIn)
//		{
//			lastResponse = "Login cancelled by Player";
//		}
//		else
//		{
//			lastResponse = "Login was successful!";
////			loader.SetActive (true);
//			StartCoroutine(SendLoginData());
//			callingAfterLogin = true;
//		}

		if (result == null)
		{
			string LastResponse = "Null Response\n";
			Debug.Log(LastResponse);
			return;
		}
		

		// Some platforms return the empty string instead of null.
		if (!string.IsNullOrEmpty(result.Error))
		{

			Debug.Log(result.Error);
		}
		else if (result.Cancelled)
		{

			Debug.Log("Cancelesd   ==  "+result.RawResult);
		}
		else if (!string.IsNullOrEmpty(result.RawResult))
		{
			lastResponse = "Login was successful!";
			//			loader.SetActive (true);
			StartCoroutine(SendLoginData());
			callingAfterLogin = true;
			Debug.Log("resuly  =  "+result.RawResult);
		}
		else
		{
			Debug.Log("empty resp");
		}




	}
	
	public IEnumerator FindNoOfLevelsCreated()
	{
		if(Application.internetReachability != NetworkReachability.NotReachable)
		{
//			loader.SetActive (true);
			string url = baseUrl+"create";
			WWWForm wForm = new WWWForm();
			wForm.AddField ("access_token",AccessToken.CurrentAccessToken.TokenString);
			WWW wLogin = new WWW(url , wForm);
			yield return wLogin;


			if(wLogin.error == null)
			{
				Debug.Log(wLogin.text);
				if(wLogin.text.Contains ("created_levels"))
				{
					IDictionary responseDic = (IDictionary)Json.Deserialize(wLogin.text);
					string status = responseDic["created_levels"].ToString();
					int.TryParse(status,out noOfLevelsCreated );
					if(noOfLevelsCreated >= 10)
					{
						levelsExceedPopup.SetActive (true);
					}
					else
					{
						//UIManager.Instance.InactiveAll ();
						//UserLevelsManager._instance.createLevelPanel.SetActive (true);
					}
				}
			}
			StartCoroutine (DeactivateLoader ());
		}	
		else
		{
			//popupPanel.SetActive (true);
			//popupText.text = "No Internet \n Connection.";
		}
	}
	
	public void YesAllowCreation()
	{
		levelsExceedPopup.SetActive (false);
		//UIManager.Instance.InactiveAll ();
		//UserLevelsManager._instance.createLevelPanel.SetActive (true);
	}
	
	public void DeactivateLevelesExceedPopup()
	{
		levelsExceedPopup.SetActive (false);
	}
	
	public void SubmitCreatedLevel()
	{
		if(Application.internetReachability != NetworkReachability.NotReachable)
			StartCoroutine (SubmitCreatedLevelsCoroutine());
		else
		{
			//popupPanel.SetActive (true);
			//popupText.text = "No Internet \n Connection.";
		}
	}
	
	bool levelSubmitted;
	public IEnumerator SubmitCreatedLevelsCoroutine()
	{
		//loader.SetActive (true);
		string url = baseUrl+"update";
		WWWForm wForm = new WWWForm();
		wForm.AddField ("access_token",AccessToken.CurrentAccessToken.TokenString);

		string key = "GrilUpLevel";
		wForm.AddField ("key",key);
		WWW wLogin = new WWW(url , wForm);
		yield return wLogin;
		if(wLogin.error == null)
		{
			Debug.Log(wLogin.text);
			if(wLogin.text.Contains ("status"))
			{
				IDictionary responseDic = (IDictionary)Json.Deserialize(wLogin.text);
				string status = responseDic["status"].ToString();
				if(status == "success")
				{

					levelSubmitted = true;
					Debug.Log(responseDic["message"].ToString());
				}
				else
				{
					Debug.Log(responseDic["message"].ToString());
				}
				
			}
		}
//		loader.SetActive (false);
	}
	
	public void OpenCreateALevelPanel()
	{
		//UIManager.Instance.ActivePanelForThisKeyWithFadding("UserLevel");
		levelSubmitted = false;
		//UserLevelsManager.levelCreated = false;
	}
	
	public void DeactivatePopup()
	{
//		popupPanel.SetActive(false);
		if(levelSubmitted)
			//UIManager.Instance.ActivePanelForThisKeyWithFadding("UserLevel");
		levelSubmitted = false;
		//UserLevelsManager.levelCreated = false;
		
	}
	
	public void DeactivateFBPopup()
	{
		fbPopup.SetActive(false);
	}
	
	
	
	
}
