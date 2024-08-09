using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MovingTextController : MonoBehaviour
{
    public float moveDistance = 200f; // Расстояние, на которое будет перемещаться текст
    public float moveDuration = 1f;   // Длительность перемещения
    public Text textElement;          // Ссылка на текстовый элемент

    private Vector3 startPosition;
    public bool isMoving = false;

    void Start()
    {
        // Сохраняем стартовую позицию текста
        startPosition = textElement.rectTransform.anchoredPosition;
        // Изначально текст скрыт
        textElement.gameObject.SetActive(false);
    }

    void Update()
    {
        // Проверка клика мыши
        if (Input.GetMouseButtonDown(0) && !isMoving)
        {
            // Запуск перемещения текста при клике
            StartCoroutine(MoveText());
        }
    }

    private IEnumerator MoveText()
    {
        isMoving = true;
        textElement.gameObject.SetActive(true);

        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        Vector2 targetPosition = (Vector2)startPosition + randomDirection * moveDistance;
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            // Интерполяция позиции текста
            textElement.rectTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Устанавливаем конечную позицию, чтобы избежать погрешностей
        textElement.rectTransform.anchoredPosition = targetPosition;

        // Скрываем текст после перемещения
        textElement.gameObject.SetActive(false);

        // Возвращаем текст на стартовую позицию
        textElement.rectTransform.anchoredPosition = startPosition;

        isMoving = false;
    }
}
