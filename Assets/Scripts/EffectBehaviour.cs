using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementBehaviour : MonoBehaviour
{
    public BallBehaviour player;
    public GameManager gameManager;
    public float armorTime;
    public bool activeArmor;
    
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        
    }

    
    void Update()
    {
        if (activeArmor)
        {
            ArmorActivate();
            ArmorDeactivate();
        }
    }

    public void ArmorActivate()
    {
        if (armorTime > 0)
        {
            armorTime -= Time.deltaTime;
            player.jin.SetActive(true);
            for(int i = 0; i < gameManager.effectObject.transform.childCount; i++)
            {
                if(i < armorTime/3)
                    gameManager.effectObject.transform.GetChild(i).gameObject.SetActive(true);
                else
                    gameManager.effectObject.transform.GetChild(i).gameObject.SetActive(false);
            }
            
        }
    }

    public void ArmorDeactivate()
    {
        if (armorTime <= 0)
        {
            player.jin.SetActive(false);
            activeArmor = false;
            gameManager.armorTime = 0;
            PlayerPrefs.SetInt("armor", 0);
        }
    }



    public void ArmorTrigger()
    {
        armorTime = gameManager.armorTime;
        if(armorTime > 0)
        {
            activeArmor = true;
        }
        
    }
}
