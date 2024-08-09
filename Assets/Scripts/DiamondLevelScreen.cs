using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondLevelScreen : MonoBehaviour
{
    public GameManager gameManager;
    public float timeSet;
    public float timeRemain;
    public Transform textPointParent;

    private void OnEnable()
    {
        gameManager.isMiniGameActive = true;
        timeRemain = 0;
    }

    private void OnDisable()
    {
        gameManager.isMiniGameActive = false;
    //    textPointParent.GetComponent<TextSpawner>().textPool.pool.Clear();
        for (int i = 1; i < textPointParent.childCount; i++)
        {
            textPointParent.GetChild(i).GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            textPointParent.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        timeRemain += Time.deltaTime;
        if(timeRemain >= timeSet)
        {
            gameManager.MiniGameResume();
            gameObject.SetActive(false);
        }
    }
}
