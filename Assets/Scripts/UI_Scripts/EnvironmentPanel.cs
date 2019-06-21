using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnvironmentPanel : MonoBehaviour {
	int noOfLevelsOpen;

	public Text totalCoinsText;
	
	public Text totalGoldText;
	public GameObject Aus_lock ;
	public GameObject Italy_lock ;
	public GameObject China_lock ;
	// Use this for initialization
	void Start () {
	
		//PlayerPrefs.DeleteAll ();
		
	}
	void Awake()
	{
		//PlayerPrefs.DeleteAll ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void OnUpgradePanel()
	{
		GameObject upgradePanel = ( GameObject )Instantiate(Resources.Load ("UpgradePanel"));
		upgradePanel.transform.SetParent(transform.parent,false);
		upgradePanel.transform.localScale = Vector3.one;
		upgradePanel.transform.localPosition = Vector3.zero;
		Destroy (gameObject);
		if(MenuManager._instance != null)
			MenuManager._instance.EnableFadePanel ();
		else
			UIManager._instance.EnableFadePanel ();
	}

	void OnEnable()
	{
		if (UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPad1Gen || 
			UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPad2Gen || 
			UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPad3Gen || 
			UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPad4Gen || 
			UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPad5Gen || 
			UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPadAir2 || 
			UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPadMini1Gen || 
			UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPadMini2Gen || 
			UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPadMini3Gen || 
			UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPadUnknown) 
		{
			this.transform.Find ("totalCoins").Find ("Image (2)").transform.localPosition = new Vector3 (-75, 10f, 0);
		
			this.transform.Find ("ScrollView (2)").transform.localPosition = new Vector3 (297, 30f, 0);
			this.transform.Find ("ScrollView (2)").transform.localScale = new Vector3 (0.5f, 0.5f, 1);

			Debug.Log("envPanelEnable()____iPad____");

		} else 
		{
			this.transform.Find ("totalCoins").Find ("Image (2)").transform.localPosition = new Vector3 (-100, 10f, 0);

			this.transform.Find ("ScrollView (2)").transform.localPosition = new Vector3 (406, 30f, 0);
			this.transform.Find ("ScrollView (2)").transform.localScale = new Vector3 (0.7f,0.7f, 1);

			Debug.Log("envPanelEnable()____iPhone____");

		}
		if(PlayerPrefs.HasKey ("ChinaOpen")) {
			China_lock.SetActive (false);
		}
		
		if(PlayerPrefs.HasKey ("ItalyOpen") )
		{
			Italy_lock.SetActive (false);
		}
		if(PlayerPrefs.HasKey ("AusOpen")) 
		{
			Aus_lock.SetActive (false);
		}
		totalGoldText.text = MenuManager.golds.ToString ();
		totalCoinsText.text = MenuManager.totalscore.ToString ();
		TutorialPanel.popupPanelActive = true;
	}

	public void USLevel()
	{

		MenuManager._instance.Level_loaded = 1 ;
	
		MenuManager.envNo = "US";
		MenuManager._instance.EnableFadePanel ();
		Destroy (gameObject);
		MenuManager._instance.levelPanel.SetActive (true);
	}

	public void AustraliaLevel()
	{
		//4


		MenuManager._instance.Level_loaded = 4 ;
		MenuManager.envNo = "Aus";
		int noOfItalyLevelsOpen = (int)EncryptionHandler64.Decrypt (PlayerPrefs.GetString("ItalyLevels"));
		//Debug.Log ("NO of opne italy " + noOfItalyLevelsOpen);
		if(noOfItalyLevelsOpen >= 10 || PlayerPrefs.HasKey ("AusOpen"))
		{
			Aus_lock.SetActive (false);
			Destroy (gameObject);
			MenuManager._instance.EnableFadePanel ();
			MenuManager._instance.levelPanel.SetActive (true);
		}
		
		
	}

	void MenuPopup()
	{
		if(MenuManager._instance.popupPanel != null)
		{
			MenuManager._instance.popupPanel.gameObject.SetActive (true);
		}
		else
		{
			GameObject popupPanel = GeneratePopupPanel();
			MenuManager._instance.popupPanel = popupPanel.GetComponent<PopupPanel>();
		}
		
		

	}

	GameObject GeneratePopupPanel()
	{
		GameObject popupPanel = ( GameObject )Instantiate(Resources.Load ("PopupPanel"));
		popupPanel.transform.SetParent(transform.parent,false);
		popupPanel.transform.localScale = Vector3.one;
		popupPanel.transform.localPosition = Vector3.zero;
		return popupPanel;
	}


	public void ItalyLevel()
	{
		//3
		 {

			MenuManager._instance.Level_loaded = 3;
			MenuManager.envNo = "Italy";
			int noOfChinaLevelsOpen = (int)EncryptionHandler64.Decrypt (PlayerPrefs.GetString ("ChinaLevels"));
			//Debug.Log ("NO of opne china "+noOfChinaLevelsOpen);
			if (noOfChinaLevelsOpen >= 10 || PlayerPrefs.HasKey ("ItalyOpen")) {
				Italy_lock.SetActive (false);
				Destroy (gameObject);
				MenuManager._instance.EnableFadePanel ();
				MenuManager._instance.levelPanel.SetActive (true);
			} 

		}
	}

	void PurchaseLevel(string toSetPlayerPref)
	{
		MenuManager.golds-=20;
		PlayerPrefs.SetString("Golds",EncryptionHandler64.Encrypt (MenuManager.golds.ToString ()));
		totalGoldText.text = MenuManager.golds.ToString ();
	
		PlayerPrefs.SetInt (toSetPlayerPref,1);
		if(PlayerPrefs.HasKey ("ChinaOpen")) {
			China_lock.SetActive (false);
		}

		 if(PlayerPrefs.HasKey ("ItalyOpen")) {
			Italy_lock.SetActive (false);
		}
	 if(PlayerPrefs.HasKey ("AusOpen")) {
			Aus_lock.SetActive (false);
		}
		MenuManager._instance.EnableFadePanel ();
		MenuManager._instance.popupPanel.gameObject.SetActive (false);
		//MenuPopup();
		//MenuManager._instance.popupPanel.EnablePopup ("Successfully unlocked the environment!",false);
	}


	public void ChinaLevel()
	{
		//2
	
		MenuManager._instance.Level_loaded = 2 ;
		MenuManager.envNo = "China";
		int noOfUSLevelsOpen = (int)EncryptionHandler64.Decrypt (PlayerPrefs.GetString("USLevels"));
		//Debug.Log ("NO of opne US " +noOfUSLevelsOpen);
		if(noOfUSLevelsOpen >= 10 || PlayerPrefs.HasKey ("ChinaOpen"))
			{
			China_lock.SetActive (false);
			Destroy (gameObject);
			MenuManager._instance.EnableFadePanel ();
			MenuManager._instance.levelPanel.SetActive (true);
		}
	
	}

	public void Cross()
	{
		MenuManager._instance.EnableFadePanel ();
		Destroy (gameObject);
	}
}
