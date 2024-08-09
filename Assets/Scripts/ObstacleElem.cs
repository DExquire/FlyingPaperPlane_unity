using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleElem : ElementBehavior
{
    public override void ActivateBonus(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            BallBehaviour player = collider.gameObject.GetComponent<BallBehaviour>();
            if (player.lives > 1)
            {
                player.lives--;
                gameManager.livesText.text = player.lives.ToString();
            }
            else if(player.lives == 1)
            {
                player.lives--;
                gameManager.GameLose();
            }
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
