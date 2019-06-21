using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Prime31;


public class IAPComboUI : MonoBehaviourGUI
{
	#if UNITY_IPHONE || UNITY_ANDROID
	
	public static IAPComboUI instance;
	
//	public GameObject IAPPanel;
//	public Transform ErrorMsgText, SlowConnText, processingForIAPText;
	
	public static string[] androidSkus = new string[] {"com.foodcookingmania.letscook.10","com.foodcookingmania.letscook.25","com.foodcookingmania.letscook.50","com.foodcookingmania.letscook.100","com.foodcookingmania.letscook.250"};
	public static string[] iosProductIds = new string[] {"com.foodcookingmania.letscook.10","com.foodcookingmania.letscook.25","com.foodcookingmania.letscook.50","com.foodcookingmania.letscook.100","com.foodcookingmania.letscook.250"};
	//public static string[] iosProductIds = new string[] {"com.slotspartyhouse.230000","com.slotspartyhouse.2700000removeads","com.slotspartyhouse.450000removeads","com.slotspartyhouse.6000000removeads","com.slotspartyhouse.90000"};

	private bool productsReceived = false, requestingProducts = true;
	private static List<IAPProduct> _products;
	
	void Start()
	{
		instance = this;
		InitialRequest();

	}

	public void InitialRequest()
	{
//		Debug.Log( "Product list received ");
		#if UNITY_ANDROID
		var key = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAmDyHYCirtedxLdvp6VJ2iK1B9f7DLxNBr6cWp7hq52aB7G5MlnFGe+HVH4RwwX3PrjpZcQhod+6qeYCxz+7P0dkvi0izb29OEqNjVIgu6DOu2ghRewdhensxgZV5R+odbTx8rA177pAMuYtsZx5nOF+XvlCkOrsDo2LOXJuayNf1+GcF2FQrK6ekPcD5FvQzFUGFjl5OsebK6u9/WDrozItmpQXSr9UgG81BUj6zHyRtyspU6sXohdDTrhfXptB2nTR4+9syFuudA5fF6uRVHvYBaXoZYXYfqnAOUqDN3BG/uGK8edPsxkax4I20tvWVla7LhboqmqdpfDWoZ5MbaQIDAQAB";
		key = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAmDyHYCirtedxLdvp6VJ2iK1B9f7DLxNBr6cWp7hq52aB7G5MlnFGe+HVH4RwwX3PrjpZcQhod+6qeYCxz+7P0dkvi0izb29OEqNjVIgu6DOu2ghRewdhensxgZV5R+odbTx8rA177pAMuYtsZx5nOF+XvlCkOrsDo2LOXJuayNf1+GcF2FQrK6ekPcD5FvQzFUGFjl5OsebK6u9/WDrozItmpQXSr9UgG81BUj6zHyRtyspU6sXohdDTrhfXptB2nTR4+9syFuudA5fF6uRVHvYBaXoZYXYfqnAOUqDN3BG/uGK8edPsxkax4I20tvWVla7LhboqmqdpfDWoZ5MbaQIDAQAB";
		IAP.init( key );
		#endif
		
//		processingForIAPText.gameObject.SetActive (true);
		
		IAP.requestProductData( iosProductIds, androidSkus, productList =>
		                       {
			Debug.Log( "Product list received 2");
			Utils.logObject( productList );
			if (productList != null)
			{
				productsReceived = true;
				_products = productList;
			}
			else
				productsReceived = false;
		});
	}

	public static void purchaseProductWithId(int index)
	{
		if(Application.internetReachability == NetworkReachability.NotReachable)
		{
			Debug.LogError("No Internet");
		}
		else if(_products!=null && Application.internetReachability != NetworkReachability.NotReachable)
		{
			#if UNITY_ANDROID
			var productId = androidSkus[index];
			#elif UNITY_IPHONE
			var productId = iosProductIds[index];
			#endif
			IAP.purchaseConsumableProduct( productId, didSucceed =>
			                              {{
				Debug.Log( "purchasing product " + productId + " result: " + didSucceed );

				}
				
			});
		}
		else if(_products==null)
		{
			Debug.LogError("Products not available");
		}
		else
		{
			Debug.LogError("Slow Net Connection");
		}

	}
	#endif
}