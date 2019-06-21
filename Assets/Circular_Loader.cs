using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Circular_Loader : MonoBehaviour {


	public static Circular_Loader _instance ;
	public GameObject circular_parent ;
	public Image circular_loader ;
	
	void Awake()
	{
		_instance = this;
	}
	void OnEnable()
	{
		circular_loader.fillAmount = 0f;
	}
	// Use this for initialization
	void Start () {
	

	}
	
	// Update is called once per frame
	void Update () {
	
		circular_loader.fillAmount += 0.0001f;


	}
}
