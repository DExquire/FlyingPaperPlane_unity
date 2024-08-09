using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextSpawner : MonoBehaviour
{
    public ObjectPool textPool;            // Ссылка на пул объектов
    public RectTransform canvasTransform;  // Ссылка на Canvas

    public float moveDistance = 200f;      // Расстояние, на которое будет перемещаться текст
    public float moveDuration = 1f;        // Длительность перемещения

    void Update()
    {
        // Проверка клика мыши
        if (Input.GetMouseButtonDown(0))
        {
            // Получение нового текстового элемента из пула
            GameObject newText = textPool.GetObject();
            RectTransform rectTransform = newText.GetComponent<RectTransform>();

            // Устанавливаем стартовую позицию в центре Canvas
            rectTransform.SetParent(canvasTransform, false);
            rectTransform.anchoredPosition = Vector2.zero;

            // Запуск корутины для перемещения текста
            StartCoroutine(MoveText(newText));
        }
    }

    private IEnumerator MoveText(GameObject textObject)
    {
        RectTransform rectTransform = textObject.GetComponent<RectTransform>();
        Vector2 startPosition = rectTransform.anchoredPosition;

        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        Vector2 targetPosition = startPosition + randomDirection * moveDistance;
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            // Интерполяция позиции текста
            rectTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Устанавливаем конечную позицию, чтобы избежать погрешностей
        rectTransform.anchoredPosition = targetPosition;

        // Возвращаем текст на стартовую позицию и отключаем его
        rectTransform.anchoredPosition = startPosition;
        textPool.ReturnObject(textObject);
    }
}
