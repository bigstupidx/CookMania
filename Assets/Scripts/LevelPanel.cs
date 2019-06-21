using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelPanel : MonoBehaviour {


	public Text totalCoinsText;
	
	public Text totalGoldText;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
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
			this.transform.Find("totalCoins").Find("Image (2)").GetComponent<RectTransform>().localPosition = new Vector3 (-67, 10, 0);
			
			Debug.Log("LevelPanelEnable()____iPad____");
			
		} else 
		{
			this.transform.Find("totalCoins").Find("Image (2)").GetComponent<RectTransform>().localPosition = new Vector3 (-90, 10, 0);
			
			Debug.Log("LevelPanelEnable()____iPhone____");
			
		}
		totalGoldText.text = MenuManager.golds.ToString ();
		totalCoinsText.text = MenuManager.totalscore.ToString ();
		TutorialPanel.popupPanelActive = true;
	}
}
