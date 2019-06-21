using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class GoldPanel : MonoBehaviour {

	public static GoldPanel _instance ;
	public Text totalCoinsText;
	
	public Text totalGoldText;
	// Use this for initialization

	void Start () {
		totalGoldText.text = MenuManager.golds.ToString ();
		totalCoinsText.text = MenuManager.totalscore.ToString ();
	}
	
	// Update is called once per frame
	void Update()
	{
		totalGoldText.text = MenuManager.golds.ToString () ; 
		totalCoinsText.text = MenuManager.totalscore.ToString ();
	}
	void OnEnable () 
	{
		Debug.Log("GoldPanelEnable()");

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
			this.transform.Find("totalCoins").Find("Image (2)").GetComponent<RectTransform>().localPosition = new Vector3 (-65, 10, 0);
			
			Debug.Log("GoldPanelEnable()____iPad____");
			
		} else 
		{
			this.transform.Find("totalCoins").Find("Image (2)").GetComponent<RectTransform>().localPosition = new Vector3 (-98, 10, 0);
			
			Debug.Log("GoldPanelEnable()____iPhone____");
			
		}
	}

//	void MenuPopup(string messagePopup)
//	{
//		if(MenuManager._instance.popupPanel != null)
//		{
//			MenuManager._instance.popupPanel.gameObject.SetActive (true);
//			MenuManager._instance.popupPanel.EnablePopup (messagePopup,false);
//		}
//		else
//		{
//			GameObject popupPanel = GeneratePopupPanel();
//			MenuManager._instance.popupPanel = popupPanel.GetComponent<PopupPanel>();
//			MenuManager._instance.popupPanel.EnablePopup (messagePopup,false);
//		}
//		
//		
//		//		equipmentPanel.purchaseButton.GetComponent<Button>().onClick.RemoveAllListeners ();
//		//		equipmentPanel.purchaseButton.GetComponent<Button>().onClick.AddListener (()=>OnPurchase());
//	}
	public void Cross()
	{
#if UNITY_ANDROID
		GoogleMobileAdsDemoScript.bannerWasLoaded=true;

			if (GoogleMobileAdsDemoScript.bannerWasLoaded) {
			GoogleMobileAdsDemoScript._instance.bannerView.Show ();
		}
#endif
		if (UIManager._instance == null) {
			GameObject upgradePanel = null;

		upgradePanel = (GameObject)Instantiate (Resources.Load ("UpgradePanel"));
		upgradePanel.transform.SetParent (transform.parent, false);
			upgradePanel.transform.localScale = Vector3.one;
			upgradePanel.transform.localPosition = Vector3.zero;
			if (MenuManager._instance != null)
				MenuManager._instance.EnableFadePanel ();
			else
				UIManager._instance.EnableFadePanel ();
			Destroy (gameObject);
		} else {
			GameObject upgradePanel = ( GameObject )Instantiate(Resources.Load ("UpgradePanel"));
			upgradePanel.transform.SetParent(transform.parent,false);
			upgradePanel.transform.localScale = Vector3.one;
			upgradePanel.transform.localPosition = Vector3.zero;
			if(MenuManager._instance != null)
				MenuManager._instance.EnableFadePanel ();
			else
				UIManager._instance.EnableFadePanel ();
			Destroy (gameObject);
		}
	}
	public void Getcoins()
	{
//		if (Advertisement.isReady ()) {
//			Advertisement.Show (null, new ShowOptions {
//				pause = true,
//				resultCallback = result => {
//				
//					if(result.ToString ().Contains("Finished"))
//					{
//						
//						MenuManager.totalscore += 10 ;
//						if(GoldPanel._instance != null)
//							GoldPanel._instance.totalCoinsText.text = MenuManager.totalscore.ToString ();
//						
//						PlayerPrefs.SetString("TotalScore",EncryptionHandler64.Encrypt (MenuManager.totalscore.ToString ()));
//
//					}
//				}
//			});
//		}
	}

}
