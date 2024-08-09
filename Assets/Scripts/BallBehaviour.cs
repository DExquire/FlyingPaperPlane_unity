using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    public Joystick joystick;
    public float lowestPosition;
    public float highestPosition;
    public float speed;
    public float lives;
    public float startLives = 3;
    public bool moveDown;
    public bool isLongTap;
    public Rigidbody2D rb;
    public List<GameObject> charSkins;
    public ParticleSystem projectiles;
    public GameObject jin;
    public GameManager GM;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        projectiles = transform.GetChild(0).GetComponent<ParticleSystem>();
        //    Destroy(transform.GetChild(0).gameObject);
        lives = startLives;
    }


    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            moveDown = !moveDown;
        }

        if (moveDown)
        {
            //   if(!isLongTap)
            {
                if (transform.position.y > lowestPosition)
                {
                    transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
                }
                else if(transform.position.y <= lowestPosition)
                {
                    transform.position = new Vector3(transform.position.x, lowestPosition, 0);
                }
            }

        }

        else
        {
            //   if(isLongTap)
            if (transform.position.y < highestPosition)
            {
                transform.position += new Vector3(0, speed * Time.deltaTime, 0);
            }
            else if (transform.position.y >= highestPosition)
            {
                transform.position = new Vector3(transform.position.x, highestPosition, 0);
            }
        }
    /*    transform.position = new Vector2(transform.position.x,//Mathf.Clamp(joystick.Horizontal * Time.deltaTime * speed + transform.position.x, -10f, 10f),
           Mathf.Clamp(joystick.Vertical * Time.deltaTime * speed + transform.position.y, -3.5f, 5.5f)
           );*/

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Coin"))
        {
            int coins = PlayerPrefs.GetInt("coins");
            GM.gameShop.Coins = coins + 5;
            //shop.Coins += coins;
            PlayerPrefs.SetInt("coins", GM.gameShop.Coins);
            //    GM.coinsText.text = GM.gameShop.Coins.ToString();
            GM.SetCoinText();
            Destroy(collision.gameObject);
        }
        else if(collision.CompareTag("Road"))
        {
            Debug.Log("GameOver");
            GM.GameLose();
        }
    }
}
