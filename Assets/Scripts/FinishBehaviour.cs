using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishBehaviour : MonoBehaviour
{
    public GameManager gameManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameManager.GameWin();
            foreach (GameObject column in gameManager.columns)
            {
                column.SetActive(false);
            }
            
        }
    }
}
