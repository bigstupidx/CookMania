using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System ;
public class DecorationItems : MonoBehaviour {

	public GameObject myParent;

	public Image myImage;

	public Sprite []elementsToShow;

	public string []elementsName;

	public int []elementsCoinPrice;

	public int []elementsGoldPrice;

	public GameObject priceButton;

	public Text priceCoinText;

	public Text priceGoldText;

	public Text selectedButtonText;

	public GameObject selectButton;

	public string myName;

	public string myCountry;

	int clickedItem = 0;



	// Use this for initialization
	void Start () {
	
	}
	
	void OnEnable()
	{
		if (myName != "Italy_TableTop") {
			if (myName != "Aus_TableTop") {
//		Debug.LogError ("Logic Not done ");
				if (PlayerPrefs.HasKey (elementsName [0]) && PlayerPrefs.HasKey (elementsName [1]) && PlayerPrefs.HasKey (elementsName [2]) && PlayerPrefs.HasKey (elementsName [3])) {
					Debug.Log ("a");
					selectButton.SetActive (true);
					if (PlayerPrefs.GetString (myName) == elementsName [0]) {
						selectedButtonText.text = "SELECTED";
					} else {
						selectedButtonText.text = "SELECT";
					}
					priceButton.SetActive (false);
				} else {
					selectButton.SetActive (false);
					priceButton.SetActive (true);
					if (PlayerPrefs.HasKey (elementsName [0])) {
						if (!PlayerPrefs.HasKey (elementsName [1])) {
							priceCoinText.text = elementsCoinPrice [1].ToString ();
							priceGoldText.text = elementsGoldPrice [1].ToString ();
							myImage.sprite = elementsToShow [1];
						} else if (!PlayerPrefs.HasKey (elementsName [2])) {
					
							priceCoinText.text = elementsCoinPrice [2].ToString ();
							priceGoldText.text = elementsGoldPrice [2].ToString ();
							myImage.sprite = elementsToShow [2];
						} else if (!PlayerPrefs.HasKey (elementsName [3])) {
					
							priceCoinText.text = elementsCoinPrice [3].ToString ();
							priceGoldText.text = elementsGoldPrice [3].ToString ();
							myImage.sprite = elementsToShow [3];
						}
					} else {
						priceCoinText.text = elementsCoinPrice [0].ToString ();
						priceGoldText.text = elementsGoldPrice [0].ToString ();
						myImage.sprite = elementsToShow [0];
					}


				}
			} else {
				selectButton.SetActive (true);
				if (PlayerPrefs.GetString (myName) == elementsName [0]) {
					selectedButtonText.text = "SELECTED";
				} else {
					selectedButtonText.text = "SELECT";
				}
				priceButton.SetActive (false);
			}
		} else {
			selectButton.SetActive (true);
			if (PlayerPrefs.GetString (myName) == elementsName [0]) {
				selectedButtonText.text = "SELECTED";
			} else {
				selectedButtonText.text = "SELECT";
			}
			priceButton.SetActive (false);
		}

	}

	public void SelectImage(int imageNo)
	{
		clickedItem = imageNo;
		myImage.sprite = elementsToShow[imageNo];
		if(PlayerPrefs.HasKey (elementsName[imageNo]))
		{
			selectButton.SetActive (true);
			if(PlayerPrefs.GetString (myName) == elementsName[imageNo])
			{
				selectedButtonText.text = "SELECTED";
			}
			else
			{
				selectedButtonText.text = "SELECT";
			}
			priceButton.SetActive (false);
		}
		else
		{
			selectButton.SetActive (false);
			priceButton.SetActive (true);
			priceCoinText.text = elementsCoinPrice[imageNo].ToString ();
			priceGoldText.text = elementsGoldPrice[imageNo].ToString ();
		}
	}

	public string resultfinal ;
	public void PurchaseItem()
	{
		if((MenuManager.totalscore >= elementsCoinPrice[clickedItem]) && (MenuManager.golds >= elementsGoldPrice[clickedItem]))
		{
			MenuManager.golds-=elementsGoldPrice[clickedItem];
			MenuManager.totalscore-=elementsCoinPrice[clickedItem];

			PlayerPrefs.SetString("TotalScore",EncryptionHandler64.Encrypt (MenuManager.totalscore.ToString ()));
			PlayerPrefs.SetString("Golds",EncryptionHandler64.Encrypt (MenuManager.golds.ToString ()));

			selectButton.SetActive (true);

			PlayerPrefs.SetString (myName ,elementsName[clickedItem]);
			string a = elementsName[clickedItem].Substring(elementsName[clickedItem].Length - 1);
			Debug.Log("imp"+a);
			//Debug.LogError("name"+myName+"elemetsname"+elementsName[clickedItem]);
			//Debug.LogError("stroe"+PlayerPrefs.GetString("US_TableCover"));
			Debug.LogError("valueoff"+int.Parse(elementsName[clickedItem].Substring(elementsName[clickedItem].Length - 1)));
			PlayerPrefs.SetInt (elementsName[clickedItem],1);

		
			if(myName == "US_TableCover")
			{


				if(int.Parse(elementsName[clickedItem].Substring(elementsName[clickedItem].Length - 1)) == 2)
				{
					//StartCoroutine(FacebookHandler._instance.SendSingleKey("UsTableCover","1"));
					FacebookHandler._instance.send_single_key("UsTableCover","1");
				}
				 if(int.Parse(elementsName[clickedItem].Substring(elementsName[clickedItem].Length - 1)) == 3)
				{
					//StartCoroutine(FacebookHandler._instance.SendSingleKey("UsTableCover3","1"));
					FacebookHandler._instance.send_single_key("UsTableCover3","1");
				}
				 if(int.Parse(elementsName[clickedItem].Substring(elementsName[clickedItem].Length - 1)) == 4)
				{
					//StartCoroutine(FacebookHandler._instance.SendSingleKey("UsTableCover4","1"));
					FacebookHandler._instance.send_single_key("UsTableCover4","1");
				}
			}

			if(myName == "China_TableCover")
			{
				if(int.Parse(elementsName[clickedItem].Substring(elementsName[clickedItem].Length - 1)) == 2)
				{
					//StartCoroutine(FacebookHandler._instance.SendSingleKey("ChinaTableCover","1"));
					FacebookHandler._instance.send_single_key("ChinaTableCover","1");
				}
				if(int.Parse(elementsName[clickedItem].Substring(elementsName[clickedItem].Length - 1)) == 3)
				{
					//StartCoroutine(FacebookHandler._instance.SendSingleKey("ChinaTableCover3","1"));
					FacebookHandler._instance.send_single_key("ChinaTableCover3","1");
				}
				if(int.Parse(elementsName[clickedItem].Substring(elementsName[clickedItem].Length - 1)) == 4)
				{
					//StartCoroutine(FacebookHandler._instance.SendSingleKey("ChinaTableCover4","1"));
					FacebookHandler._instance.send_single_key("ChinaTableCover4","1");
				}
			}

			if(myName == "China_TableTop")
			{
				if(int.Parse(elementsName[clickedItem].Substring(elementsName[clickedItem].Length - 1)) == 2)
				{
					//StartCoroutine(FacebookHandler._instance.SendSingleKey("ChinaTableTop","1"));
					FacebookHandler._instance.send_single_key("ChinaTableTop","1");
				}
				if(int.Parse(elementsName[clickedItem].Substring(elementsName[clickedItem].Length - 1)) == 3)
				{
					//StartCoroutine(FacebookHandler._instance.SendSingleKey("ChinaTableTop3","1"));
					FacebookHandler._instance.send_single_key("ChinaTableTop3","1");
				}
				if(int.Parse(elementsName[clickedItem].Substring(elementsName[clickedItem].Length - 1)) == 4)
				{
					//StartCoroutine(FacebookHandler._instance.SendSingleKey("ChinaTableTop4","1"));
					FacebookHandler._instance.send_single_key("ChinaTableTop4","1");
				}
			}

			if(myName == "Italy_TableCover")
			{
				if(int.Parse(elementsName[clickedItem].Substring(elementsName[clickedItem].Length - 1)) == 2)
				{
					//StartCoroutine(FacebookHandler._instance.SendSingleKey("ItalyTableCover","1"));
					FacebookHandler._instance.send_single_key("ItalyTableCover","1");
				}
				if(int.Parse(elementsName[clickedItem].Substring(elementsName[clickedItem].Length - 1)) == 3)
				{
					//StartCoroutine(FacebookHandler._instance.SendSingleKey("ItalyTableCover3","1"));
					FacebookHandler._instance.send_single_key("ItalyTableCover3","1");
				}
				if(int.Parse(elementsName[clickedItem].Substring(elementsName[clickedItem].Length - 1)) == 4)
				{
					//StartCoroutine(FacebookHandler._instance.SendSingleKey("ItalyTableCover4","1"));
					FacebookHandler._instance.send_single_key("ItalyTableCover4","1");
				}
			}

			if(myName == "Italy_TableTop")
			{
				if(int.Parse(elementsName[clickedItem].Substring(elementsName[clickedItem].Length - 1)) == 2)
				{
					//StartCoroutine(FacebookHandler._instance.SendSingleKey("ItalyTableTop","1"));
					FacebookHandler._instance.send_single_key("ItalyTableTop","1");
				}
			
			}

			if(myName == "Aus_TableCover")
			{
				if(int.Parse(elementsName[clickedItem].Substring(elementsName[clickedItem].Length - 1)) == 2)
				{
					//StartCoroutine(FacebookHandler._instance.SendSingleKey("AusTableColor","1"));
					FacebookHandler._instance.send_single_key("AusTableColor","1");
				}
				if(int.Parse(elementsName[clickedItem].Substring(elementsName[clickedItem].Length - 1)) == 3)
				{
					//StartCoroutine(FacebookHandler._instance.SendSingleKey("AusTableColor3","1"));
					FacebookHandler._instance.send_single_key("AusTableColor3","1");
				}
				if(int.Parse(elementsName[clickedItem].Substring(elementsName[clickedItem].Length - 1)) == 4)
				{
					//StartCoroutine(FacebookHandler._instance.SendSingleKey("AusTableColor4","1"));
					FacebookHandler._instance.send_single_key("AusTableColor4","1");
				}
			}

			if(myName == "Aus_TableTop")
			{
				if(int.Parse(elementsName[clickedItem].Substring(elementsName[clickedItem].Length - 1)) == 2)
				{
					FacebookHandler._instance.send_single_key("AusTableTop","1");
					//StartCoroutine(FacebookHandler._instance.SendSingleKey("AusTableTop","1"));
				}

			}

			PlayerPrefs.SetInt (elementsName[clickedItem],1);
			Debug.Log("done this "+PlayerPrefs.GetInt(elementsName[clickedItem]));
		

			selectedButtonText.text = "SELECTED";
			DecorationPanel._instance.CallDecrementCoin ();
			priceButton.SetActive (false);

		}
		else
		{
		
			if((MenuManager.totalscore < elementsCoinPrice[clickedItem]) )
			{
				MenuManager._instance.lastPanel = myParent;
				MenuManager._instance.lastPanelName = "DecorationPanel";
				MenuManager._instance.Insufficinetcoin();
			}
			else if((MenuManager.golds < elementsGoldPrice[clickedItem]))
			{
				MenuManager._instance.lastPanel = myParent;
				MenuManager._instance.lastPanelName = "DecorationPanel";
				MenuManager._instance.Insufficinetgold() ;
			}
			//Debug.Log("insufficient coins");
		}
	}

	public void SelectItem()
	{
		if(PlayerPrefs.GetString (myName) != elementsName[clickedItem])
		{
			selectedButtonText.text = "SELECTED";
			PlayerPrefs.SetString (myName ,elementsName[clickedItem]);
		}
		
	}




}
