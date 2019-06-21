using UnityEngine;
using System.Collections;
using UnityEngine.UI ;
public class Loader : MonoBehaviour {


	public static Loader _instance ;
	int rand ;
	public Text loader_text ;
	public Slider loader;
	bool started=true ;
	public static string levelToLoad;
	bool a=false;
	// Use this for initialization
	void Awake()
	{
		_instance = this;
		DontDestroyOnLoad (gameObject);
	}

	AsyncOperation asyncOp;

	void  OnEnable()
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
			this.transform.Find ("toScale").Find ("man").GetComponent<RectTransform> ().localPosition = new Vector3 (50, 0, 0);

		} else 
		{
			this.transform.Find ("toScale").Find ("man").GetComponent<RectTransform> ().anchoredPosition = new Vector3 (-20, 0, 0);

		}



		started = true;
		loader.value = 0.01f;
		rand = Random.Range (0, 8);
		if (rand == 0) {
			loader_text.text = "Use handcuffs to catch thieves";
		}
		else if (rand == 1) {
			loader_text.text = "Use the bell to call more customers.";
		}
		else if (rand == 2) {
			loader_text.text = "The customer waits longer when radio is switched on";
		}
		else if (rand == 3) {
			loader_text.text = "The whistle blows when the customer leaves without paying.";
		}
		else if (rand == 4) {
			loader_text.text = "Buy upgrades to increase the equipments capacity.";
		}
		else if (rand == 5) {
			loader_text.text = "The cupcake refills the customer waiting bar.";
		}
		else if (rand == 6) {
			loader_text.text = "Login to facebook to save your data on server.";
		}
		else if (rand == 7) {
			loader_text.text = "Better decorated stall fetches higher bonus.";
		}
	  if (a) 
		{
		//	asyncOp = Application.LoadLevelAsync (levelToLoad);

			//yield return asyncOp;
	
		}


	}


	// Update is called once per frame
	void Update () {

		loader.value += 0.009f ;
	   if (loader.value >= 0.9f) {
			a= true;
			Application.LoadLevel(levelToLoad);
		}
	//	if (asyncOp != null)
		{

		//	if(!asyncOp.isDone)
			{
				//loader.value  = asyncOp.progress/0.9f;
				//loader.value = asyncOp.progress ;

			
			}
//
//			//Debug.Log("loader"+loader.value);
//			if(loader.value >= 1f)
//			{
//				//gameObject.SetActive(false);
//				//started = false ;
//
//			}
		}
	    
	}
}
