using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;   // скорость движения, видна в Инспекторе
    private Rigidbody2D rb;    // сюда сохраним наш Rigidbody2D

    void Start()
    {
        // Находим компонент Rigidbody2D на этом же объекте и запоминаем
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Каждый кадр спрашиваем: какие клавиши нажаты?
        float moveX = Input.GetAxisRaw("Horizontal"); // -1 (влево), 0, 1 (вправо)
        float moveY = Input.GetAxisRaw("Vertical");   // -1 (вниз), 0, 1 (вверх)

        // Создаём вектор (направление) движения
        Vector2 moveDirection = new Vector2(moveX, moveY);

        // Двигаем физическое тело (Rigidbody2D) с учётом скорости
        rb.linearVelocity = moveDirection * speed;
    }
}