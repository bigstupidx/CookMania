using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class VideoAds : MonoBehaviour
{
	public static VideoAds _instance;

	void Awake()
	{
		_instance = this;

	}
	// Use this for initialization
	void Start()
	{
	
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}

	public void GetmoreCoins()
	{
//		if (Advertisement.isReady ()) {
//			Advertisement.Show (null, new ShowOptions {
//				pause = true,
//				resultCallback = result => {
//					Debug.Log("video result = "+result.ToString());
//					if(result.ToString ().Contains("Finished"))
//					{
//						
//						MenuManager.totalscore += 10 ;
//						PlayerPrefs.SetString("TotalScore",EncryptionHandler64.Encrypt (MenuManager.totalscore.ToString ()));
//
//					}
//				}
//			});
//		}
	}

}
