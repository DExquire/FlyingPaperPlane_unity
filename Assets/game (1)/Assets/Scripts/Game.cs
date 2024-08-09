using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
	#region Singleton:Game

	public static Game Instance;

	void Awake ()
	{
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad (gameObject);
		} else {
			Destroy (gameObject);
		}
	}

	#endregion

	[SerializeField] Text[] allCoinsUIText;

	public int Coins;

	void Start ()
	{
		UpdateAllCoinsUIText ();
	}

	public void UseCoins (int amount)
	{
		Coins -= amount;
		PlayerPrefs.SetInt("coins", Coins);
	}

	public bool HasEnoughCoins (int amount)
	{
		return (Coins >= amount);
	}

	public void UpdateAllCoinsUIText ()
	{
		Coins = PlayerPrefs.GetInt("coins");
		print(Coins);
		for (int i = 0; i < allCoinsUIText.Length; i++) {
			allCoinsUIText [i].text = Coins.ToString ();
		}
	}

}
