using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
    [Header("Настройки луча")]
    [SerializeField]
    private float maxRayDistance = 10f; // Максимальная дальность луча

    [SerializeField]
    private LayerMask layerMask;         // Слои, с которыми луч будет взаимодействовать
    private LineRenderer lineRenderer;  // Ссылка на компонент LineRenderer

    void Start()
    {
        // Получаем компонент LineRenderer, который мы добавили к персонажу
        lineRenderer = GetComponent<LineRenderer>();
        
        // Говорим LineRenderer, что наша линия будет состоять из 2 точек
        lineRenderer.positionCount = 2;
        
        // По умолчанию, лазер никуда не наведён, поэтому линию лучше сделать невидимой
        lineRenderer.enabled = false;
    }

    void Update()
    {
        // 1. Узнаём позицию мыши на экране
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f; // Убираем глубину для 2D мира

        // 2. Вычисляем направление луча: от персонажа к курсору
        Vector2 direction = (mouseWorldPos - transform.position).normalized;

        // 3. Запускаем невидимый луч (логика!)
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxRayDistance, layerMask);

        // 4. Готовим данные для отрисовки видимой линии (Line Renderer)
        Vector3 startPoint = transform.position;

        // 5. Отрисовываем видимую линию
        lineRenderer.SetPosition(0, startPoint); // Устанавливаем начальную точку
        lineRenderer.SetPosition(1, endPoint);   // Устанавливаем конечную точку
        
        // 6. Делаем линию видимой
        lineRenderer.enabled = true;

        // (Опционально) Рисуем невидимый луч в редакторе для отладки
        Debug.DrawRay(transform.position, direction * maxRayDistance, Color.green);
    }
}