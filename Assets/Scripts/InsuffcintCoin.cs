using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class InsuffcintCoin : MonoBehaviour
{

	// Use this for initialization
	void Start()
	{
	
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}

	void OnEnable()
	{
	
		transform.SetAsLastSibling();
		
	}

	public void Gold()
	{
		GameObject specialPanel = (GameObject)Instantiate(Resources.Load("GoldPanel"));
		specialPanel.transform.SetParent(transform.parent, false);
		specialPanel.transform.localScale = Vector3.one;
		specialPanel.transform.localPosition = Vector3.zero;
		if (MenuManager._instance != null)
			MenuManager._instance.EnableFadePanel();
		else
			UIManager._instance.EnableFadePanel();
		Destroy(gameObject);

		Destroy(MenuManager._instance.lastPanel);
	    
	}

	public void Close()
	{
		MenuManager._instance.lastPanel = null;
		MenuManager._instance.lastPanelName = "";
		Destroy(gameObject);
	}

	public void GetMoreCoins()
	{
//		if (Advertisement.isReady()) {
//			Advertisement.Show(null, new ShowOptions {
//				pause = true,
//				resultCallback = result => {
//					Debug.Log("video result = " + result.ToString());
//					if (result.ToString().Contains("Finished")) {
//						
//						MenuManager.totalscore += 10;
//						PlayerPrefs.SetString("TotalScore", EncryptionHandler64.Encrypt(MenuManager.totalscore.ToString()));
//
//					}
//				}
//			});
//		}
		gameObject.SetActive(false);
	}
}
