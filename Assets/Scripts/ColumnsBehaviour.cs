using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnsBehaviour : MonoBehaviour
{
    public GameManager gameManager;
    public List<GameObject> columnsPref;
    public Transform columnsParent;
    public Transform positionInstance;
    public Transform limitPosition;
    
    public float upperLimit;
    public float lowerLimit;

    public float time;
    public float timeLimit;

    private void Update()
    {
        CreateColumn();
    }

    public void CreateColumn()
    {
        time += Time.deltaTime;
        if (time >= timeLimit)
        {
            var newColumn = Instantiate(columnsPref[Random.Range(0, columnsPref.Count)], positionInstance.position, Quaternion.identity, columnsParent);
            if(newColumn.tag != "Coin")
            { 
                newColumn.transform.localPosition = new(newColumn.transform.localPosition.x, Random.Range(lowerLimit, upperLimit), -1);
            }
            else
            {
                newColumn.transform.localPosition = new(newColumn.transform.localPosition.x, Random.Range(lowerLimit, upperLimit), -1);
            }
            newColumn.GetComponent<ElementBehavior>().limitPoint = limitPosition;
            newColumn.GetComponent<ElementBehavior>().gameManager = gameManager;

            SetElementSpeed(newColumn);

            time = 0;
        }

        
    }

    public void SetElementSpeed(GameObject elem)
    {
        if(gameManager.currentLevelSpeed == GameManager.LevelSpeed.zero)
        {
            elem.GetComponent<ElementBehavior>().initSpeed = 2f;
        }
        else if (gameManager.currentLevelSpeed == GameManager.LevelSpeed.first)
        {
            elem.GetComponent<ElementBehavior>().initSpeed = 4f;
        }
        else if (gameManager.currentLevelSpeed == GameManager.LevelSpeed.second)
        {
            elem.GetComponent<ElementBehavior>().initSpeed = 5f;
        }
        else if (gameManager.currentLevelSpeed == GameManager.LevelSpeed.third)
        {
            elem.GetComponent<ElementBehavior>().initSpeed = 6f;
        }
        else if (gameManager.currentLevelSpeed == GameManager.LevelSpeed.fourth)
        {
            elem.GetComponent<ElementBehavior>().initSpeed = 8f;
        }
        else if (gameManager.currentLevelSpeed == GameManager.LevelSpeed.fifth)
        {
            elem.GetComponent<ElementBehavior>().initSpeed = 10f;
        }
    }


}
