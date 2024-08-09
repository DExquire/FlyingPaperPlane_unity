using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningElem : ElementBehavior
{
    public override void ActivateBonus(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            BallBehaviour player = collider.gameObject.GetComponent<BallBehaviour>();
            gameManager.scoreNumSpeed = 4;
            gameManager.isLightningEffect = true;
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
