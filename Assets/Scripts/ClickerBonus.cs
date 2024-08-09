using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickerBonus : ElementBehavior
{
    public override void ActivateBonus(Collider2D collision)
    {
        //   BallBehaviour player = collision.gameObject.GetComponent<BallBehaviour>();
        Debug.Log("bonus Activated");
        
        gameManager.MiniGamePause();
        gameManager.clickerLevel.SetActive(true);
    }

    public override void Move()
    {
        if (transform.position.x <= limitPoint.position.x)
        {
            Destroy(transform.gameObject);
        }
        else
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
    }
}
