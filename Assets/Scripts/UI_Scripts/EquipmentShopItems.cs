using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EquipmentShopItems : MonoBehaviour {

	public static EquipmentShopItems _instance;
	public GameObject []upgradeNoImages;

	public Sprite []upgradeImages;

	public int []coinsToUpgradeLevel;

	public int []goldToUpgradeLevel;

	public string []upgradeValues;

	public EquipmentPanel equipmentPanel;

	public string myName;

	int upgradeValue;
	public int equ_value ;
	public int equ_number ;
	// Use this for initialization
	void Awake () {
		_instance = this;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnEnable()
	{

		if (!myName.Contains ("ItalyFridge"))
			upgradeValue = (int)EncryptionHandler64.Decrypt (PlayerPrefs.GetString (myName + "Upgrade"));
		else {
			if(PlayerPrefs.HasKey (myName + "Upgrade"))
			{
				upgradeValue = (int)EncryptionHandler64.Decrypt (PlayerPrefs.GetString (myName + "Upgrade"));
				
			}
			else
				upgradeValue = 0;
		}
	//	Debug.Log("myname = "+ myName+"Upgrade");
	//	Debug.Log("upgradeValue = "+upgradeValue);
		for(int i= 0 ; i <= upgradeValue ; i++)
		{
			upgradeNoImages[i].SetActive (true);
		}

		if(myName == "Grills")
		{
			OnClickToShow();

		}
//			PlayerPrefs.SetInt (myName+"Upgrade",PlayerPrefs.GetInt (myName+"Upgrade")+1);

	}




	public void OnClickToShow()
	{

		EquipmentPanel._instance.help_bttn.GetComponent<Animator> ().enabled = true;
//		Debug.Log (equ_number);
		if(upgradeValue < 2)
		{
			equipmentPanel.upgradeImage.sprite = upgradeImages[upgradeValue];
			equipmentPanel.upgradeValueText.text = upgradeValues[upgradeValue].ToString ();
			equipmentPanel.purchaseButton.SetActive (true);
			if(equ_number == 10  && PlayerPrefs.GetInt("Fridge")==1 )
			{

				equipmentPanel.purchaseButton.SetActive (false);
				
			}
			if(goldToUpgradeLevel[upgradeValue] > 0)
			{
				equipmentPanel.goldObject.SetActive (true);
				equipmentPanel.goldText.text = goldToUpgradeLevel[upgradeValue].ToString ();
			}
			else
			{
				equipmentPanel.goldObject.SetActive (false);
			}
			
			equipmentPanel.coinsText.text = coinsToUpgradeLevel[upgradeValue].ToString ();
			equipmentPanel.purchaseButton.GetComponent<Button>().onClick.RemoveAllListeners ();
			equipmentPanel.purchaseButton.GetComponent<Button>().onClick.AddListener (()=>OnPurchase());
			
		}
		else
		{
			equipmentPanel.upgradeImage.sprite = upgradeImages[1];
			equipmentPanel.upgradeValueText.text = upgradeValues[2];
			equipmentPanel.purchaseButton.SetActive (false);
			
		}
		EquimentShowInfo._instance.itemNo = equ_number;

	}

	public void OnPurchase()
	{

//		int golds = (int)EncryptionHandler64.Decrypt (PlayerPrefs.GetString("Golds"));
//		int totalscore = (int)EncryptionHandler64.Decrypt (PlayerPrefs.GetString("TotalScore"));

		if(MenuManager.totalscore >= coinsToUpgradeLevel[upgradeValue] && MenuManager.golds >= goldToUpgradeLevel[upgradeValue])
		{

			MenuManager.golds-=goldToUpgradeLevel[upgradeValue];
			MenuManager.totalscore-=coinsToUpgradeLevel[upgradeValue];
			upgradeNoImages[upgradeValue+1].SetActive (true);
			PlayerPrefs.SetString("TotalScore",EncryptionHandler64.Encrypt (MenuManager.totalscore.ToString ()));
			PlayerPrefs.SetString("Golds",EncryptionHandler64.Encrypt (MenuManager.golds.ToString ()));
			upgradeValue++;
			PlayerPrefs.SetString(myName+"Upgrade",EncryptionHandler64.Encrypt (upgradeValue.ToString ()));
		
			if(myName == "Grills")
			{
				//StartCoroutine(FacebookHandler._instance.SendSingleKey("GrilUpLevel",upgradeValue.ToString())) ;
				FacebookHandler._instance.send_single_key("GrilUpLevel",upgradeValue.ToString());
			}
			if(myName == "USCoke")
			{
				//StartCoroutine(FacebookHandler._instance.SendSingleKey("ColaTraysLevel",upgradeValue.ToString())) ;
				FacebookHandler._instance.send_single_key("ColaTraysLevel",upgradeValue.ToString());
			}
			if(myName == "Plate")
			{
				//StartCoroutine(FacebookHandler._instance.SendSingleKey("HotTraysLevel",upgradeValue.ToString())) ;
				FacebookHandler._instance.send_single_key("HotTraysLevel",upgradeValue.ToString());
			}
			if(myName == "ChinaBowls")
			{
				//StartCoroutine(FacebookHandler._instance.SendSingleKey("SoupBowlLevel",upgradeValue.ToString())) ;
				FacebookHandler._instance.send_single_key("SoupBowlLevel",upgradeValue.ToString());
			}
			if(myName == "ChinaPans")
			{
				//StartCoroutine(FacebookHandler._instance.SendSingleKey("SkilletLevel",upgradeValue.ToString())) ;
				FacebookHandler._instance.send_single_key("SkilletLevel",upgradeValue.ToString());
			}
			if(myName == "ChinaSoupContainer")
			{
				//StartCoroutine(FacebookHandler._instance.SendSingleKey("StockPotLevel",upgradeValue.ToString())) ;
				FacebookHandler._instance.send_single_key("StockPotLevel",upgradeValue.ToString());
			}
			if(myName == "ChinaPlate")
			{
				//StartCoroutine(FacebookHandler._instance.SendSingleKey("NoodleDishesLevel",upgradeValue.ToString())) ;
				FacebookHandler._instance.send_single_key("NoodleDishesLevel",upgradeValue.ToString());
			}
			if(myName == "ItalyCoke")
			{
				//StartCoroutine(FacebookHandler._instance.SendSingleKey("ColaTraysItalyLevel",upgradeValue.ToString())) ;
				FacebookHandler._instance.send_single_key("ColaTraysItalyLevel",upgradeValue.ToString());
			}
			if(myName == "ItalyPlate")
			{
				//StartCoroutine(FacebookHandler._instance.SendSingleKey("DishesLevel",upgradeValue.ToString())) ;
				FacebookHandler._instance.send_single_key("DishesLevel",upgradeValue.ToString());
			}
			if(myName == "Oven")
			{
				//StartCoroutine(FacebookHandler._instance.SendSingleKey("OvenLevel",upgradeValue.ToString())) ;
				FacebookHandler._instance.send_single_key("OvenLevel",upgradeValue.ToString());
			}

			if(myName == "AusPlate")
			{
				//StartCoroutine(FacebookHandler._instance.SendSingleKey("DishesAusLevel",upgradeValue.ToString())) ;
				FacebookHandler._instance.send_single_key("DishesAusLevel",upgradeValue.ToString());
			}

			if(myName == "AusCoke")
			{
				//StartCoroutine(FacebookHandler._instance.SendSingleKey("ColaTraysAusLevel",upgradeValue.ToString())) ;
				FacebookHandler._instance.send_single_key("ColaTraysAusLevel",upgradeValue.ToString());
			}

			if(myName == "Fries")
			{
				//StartCoroutine(FacebookHandler._instance.SendSingleKey("DeepFrierLevel",upgradeValue.ToString())) ;
				FacebookHandler._instance.send_single_key("DeepFrierLevel",upgradeValue.ToString());
			}

			if(myName == "AusGrills")
			{
				//StartCoroutine(FacebookHandler._instance.SendSingleKey("GrillUpAusLevel",upgradeValue.ToString())) ;
				FacebookHandler._instance.send_single_key("GrillUpAusLevel",upgradeValue.ToString());
			}

		   
//			equipmentPanel.totalGoldText.text = golds.ToString ();
//			equipmentPanel.totalCoinsText.text = totalscore.ToString ();
			equipmentPanel.CallDecrementCoin();
			OnClickToShow();
			if(equ_number == 10)
			{
				PlayerPrefs.SetInt("Fridge" , 1);

				//StartCoroutine(FacebookHandler._instance.SendSingleKey("FridgeLevel","1")) ;
				FacebookHandler._instance.send_single_key("FridgeLevel","1");
				equipmentPanel.purchaseButton.SetActive (false);
			}



		}
		else
		{
			if((MenuManager.totalscore < coinsToUpgradeLevel[upgradeValue]) )
			{
				MenuManager._instance.lastPanel = equipmentPanel.gameObject;
				MenuManager._instance.lastPanelName = "EquipmentUpdrade";
				MenuManager._instance.Insufficinetcoin();	

			}
			else if((MenuManager.golds < goldToUpgradeLevel[upgradeValue]))
			{
				MenuManager._instance.Insufficinetgold();
				MenuManager._instance.lastPanel = equipmentPanel.gameObject;
				MenuManager._instance.lastPanelName = "EquipmentUpdrade";

			}
			//Debug.Log("insufficient funds");
		}
//		PlayerPrefs.GetInt ("Golds");
//		PlayerPrefs.SetInt ("TotalScore",PlayerPrefs.GetInt ("TotalScore"));
	}





}
