using UnityEngine;
using System;
using System.Collections;
using Prime31;

public class InAppManager : MonoBehaviour
{
    private static InAppManager instance;
//    public GameObject InternetConnPopup;
//    public GameObject IapPurchased;
//	public GameObject partIcles;
    public static InAppManager Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {

        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(instance.gameObject);
//            InternetConnPopup.SetActive(false);
        }
    }
    void OnEnable()
    {
//        InternetConnPopup.SetActive(false);
//        IapPurchased.SetActive(false);
    }
    void OnDisable()
    {
    }
    // Use this for initialization
    void Start()
	{
        gameObject.SetActive(false);
    }

	//Need to change this methods according to new IAP
    public void Buy10GoldBars()
    {
		IAPComboUI.instance.InitialRequest();

		print("Buy10GoldBars");
        IAPComboUI.purchaseProductWithId(0);
    }
	public void Buy25GoldBars()
	{
		IAPComboUI.instance.InitialRequest();
		
		print("Buy25GoldBars");
		IAPComboUI.purchaseProductWithId(1);
	}
	public void Buy50GoldBars()
	{
		IAPComboUI.instance.InitialRequest();
		
		print("Buy50GoldBars");
		IAPComboUI.purchaseProductWithId(2);
	} 
	public void Buy100GoldBars()
	{
		IAPComboUI.instance.InitialRequest();
		
		print("Buy100GoldBars");
		IAPComboUI.purchaseProductWithId(3);
	} 
	public void Buy250GoldBars()
	{
		IAPComboUI.instance.InitialRequest();
		
		print("Buy25GoldBars");
		IAPComboUI.purchaseProductWithId(4);
	}
  
}