using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBonus : ElementBehavior
{
    public override void ActivateBonus(Collider2D collider)
    {
        gameManager.scoreNum += 5;
        gameManager.scoreTxt[0].text = gameManager.scoreNum.ToString();
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
