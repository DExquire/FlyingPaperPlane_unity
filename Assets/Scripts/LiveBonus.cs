using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveBonus : ElementBehavior
{
    public override void ActivateBonus(Collider2D collision)
    {
        BallBehaviour player = collision.gameObject.GetComponent<BallBehaviour>();
        if (player.lives < 3)
        {
            player.lives++;
            gameManager.livesText.text = player.lives.ToString();

            
        }
    }

    public override void Move()
    {
        if (transform.position.x <= limitPoint.position.x)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
    }
}
