using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Shop : MonoBehaviour
{
	#region Singlton:Shop

	public static Shop Instance;
	public GameManager gameManager;

	public Button shopButton;

	void Awake ()
	{
		if (Instance == null)
			Instance = this;
		else
			Destroy (gameObject);
	}

	#endregion

	[System.Serializable] public class ShopItem
	{
		public Sprite Image;
		public int Price;
		public bool IsPurchased = false;
		public int effectTime;
	}

	public List<ShopItem> ShopItemsList;
	[SerializeField] Animator NoCoinsAnim;
 

	[SerializeField] GameObject ItemTemplate;
	GameObject g;
	[SerializeField] Transform ShopScrollView;
	[SerializeField] GameObject ShopPanel;
	Button buyBtn;
	Button setBtn;
	string purchased;

	void Start ()
	{
		purchased = PlayerPrefs.GetString("bought");
		print("purchased " + PlayerPrefs.GetString("bought"));
		int len = ShopItemsList.Count;
		for (int i = 0; i < len; i++) {
			g = Instantiate (ItemTemplate, ShopScrollView);
			g.transform.GetChild (0).GetComponent <Image> ().sprite = ShopItemsList [i].Image;
			g.GetComponent<ShopElem>().price.text = ShopItemsList [i].Price.ToString ();
			buyBtn = g.transform.GetChild (2).GetComponent <Button> ();
			setBtn = g.transform.GetChild(3).GetComponent<Button>();
			/*if(purchased.Contains(i.ToString()))
            {
				ShopItemsList[i].IsPurchased = true;
            }*/
			if (ShopItemsList[i].IsPurchased)
			{
			//	DisableBuyButton();
			//	EnableSetButton(i);
			/*	if (!PlayerPrefs.GetString("bought").Contains(i.ToString()))
				{
					purchased += i.ToString();
					PlayerPrefs.SetString("bought", purchased);
				}*/
			}
			/*else
			{
				DisableSetButton();
			}*/
			buyBtn.AddEventListener (i, OnShopItemBtnClicked);
			setBtn.AddEventListener(i, OnSetSkinBtnClicked);

			for(int y = 0; y < g.transform.GetChild(0).GetChild(0).childCount; y++)
            {
				if (y+1 <= ShopItemsList[i].effectTime / 3)
				{
					g.transform.GetChild(0).GetChild(0).GetChild(y).gameObject.SetActive(true);
				}
				else
				{
					g.transform.GetChild(0).GetChild(0).GetChild(y).gameObject.SetActive(false);
				}
            }

			g.GetComponent<ShopElem>().timeProp.text = ShopItemsList[i].effectTime.ToString() + " s";
		}
		setBtn = null;
		print("bought " + PlayerPrefs.GetString("bought"));

		

	}

    public void CheckAvailable()
    {
		for (int i = 0; i < ShopScrollView.childCount; i++)
		{
			if (gameManager.armorTime + ShopItemsList[i].effectTime > 15)
			{
				ShopScrollView.GetChild(i).GetComponent<ShopElem>().buyButton.interactable = false;
			}
			else
            {
				ShopScrollView.GetChild(i).GetComponent<ShopElem>().buyButton.interactable = true;
			}
		}
	}

    void OnShopItemBtnClicked (int itemIndex)
	{
		print(itemIndex);
		if (Game.Instance.HasEnoughCoins (ShopItemsList [itemIndex].Price)) {
		//	Game.Instance.UseCoins (ShopItemsList [itemIndex].Price);
			//purchase Item
		//	ShopItemsList [itemIndex].IsPurchased = true;

			//disable the button
		//	buyBtn = ShopScrollView.GetChild (itemIndex).GetChild (2).GetComponent <Button> ();
		//	DisableBuyButton ();
		//	EnableSetButton(itemIndex);
			//change UI text: coins
		//	Game.Instance.UpdateAllCoinsUIText ();

		//	purchased += itemIndex.ToString();
		//	PlayerPrefs.SetString("bought", purchased);
			print(PlayerPrefs.GetString("bought"));

			//add avatar
			//			Profile.Instance.AddAvatar (ShopItemsList [itemIndex].Image);
			
			if(gameManager.armorTime + ShopItemsList[itemIndex].effectTime <= 15)
            {
				Game.Instance.UseCoins(ShopItemsList[itemIndex].Price);
				Game.Instance.UpdateAllCoinsUIText();
				gameManager.armorTime += ShopItemsList[itemIndex].effectTime;
				PlayerPrefs.SetInt("armor", gameManager.armorTime);
			}
			//int itemInd = 0;
			for(int i = 0; i < ShopScrollView.childCount; i ++)
            {
				if (gameManager.armorTime + ShopItemsList[i].effectTime > 15)
				{
					ShopScrollView.GetChild(i).GetComponent<ShopElem>().buyButton.interactable = false;
				//	itemInd += 1;
				}
			}
		} else {
			NoCoinsAnim.SetTrigger ("NoCoins");
			Debug.Log ("You don't have enough coins!!");
		}
	}

	void OnSetSkinBtnClicked(int itemIndex)
    {
		PlayerPrefs.SetInt("skinIndex", itemIndex);
		Debug.Log(PlayerPrefs.GetInt("skinIndex"));
    }

	void DisableBuyButton ()
	{
		buyBtn.interactable = false;
		buyBtn.transform.GetChild (0).GetComponent <Text> ().text = "PURCHASED";
	}

	void EnableSetButton(int btnNumber)
	{
		ShopScrollView.GetChild(btnNumber).GetChild(3).gameObject.SetActive(true);
	}

	void DisableSetButton()
	{
		setBtn.gameObject.SetActive(false);
	}

	/*---------------------Open & Close shop--------------------------*/
	public void OpenShop ()
	{
		ShopPanel.SetActive (true);
	}

	public void CloseShop ()
	{
		ShopPanel.SetActive (false);
	}

}
