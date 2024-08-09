using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnBehaviour : MonoBehaviour
{
    GameManager gameManager;

    private void Start()
    {
        gameManager = GetComponentInParent<CoilBehaviour>().gameManager;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player" && !collision.gameObject.GetComponentInParent<ElementBehaviour>().activeArmor)
        {

            if (gameManager.lives > 1)
            {
                gameManager.lives--;
                gameManager.livesText.text = gameManager.lives.ToString();
                gameObject.GetComponent<Collider2D>().enabled = false;
            }
            else
            {
                gameManager.GameLose();
            }
        }
    }
}
