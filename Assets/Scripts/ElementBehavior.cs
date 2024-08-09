using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ElementBehavior : MonoBehaviour
{
    public GameManager gameManager;
    public Transform limitPoint;
    public float speed;
    public float initSpeed;

    public abstract void ActivateBonus(Collider2D collider);
    public abstract void Move();

    public void Update()
    {
        if(!gameManager.isMiniGameActive && !gameManager.isPaused)
        {
            speed = initSpeed;
        }
        else
        {
            speed = 0;
        }
        Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ActivateBonus(collision);
            //destroy
            Destroy(gameObject);
        }
    }
}
