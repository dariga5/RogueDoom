using UnityEngine;

public class AimLaser : MonoBehaviour
{
    private LineRenderer laserLine; // сюда сохраним наш LineRenderer
    private Camera mainCamera;      // сюда сохраним камеру


    private Vector3 mouseScreen;
    private Vector3 mouseWorld;
    
    void Start()
    {
        // Находим компонент LineRenderer на этом же объекте (персонаже)
        laserLine = GetComponent<LineRenderer>();
        // Находим главную камеру
        mainCamera = Camera.main;
    }

    void Update()
    {
        // 1. Узнаём позицию мыши на экране (в пикселях)
        mouseScreen = Input.mousePosition;
        // 2. Переводим её в игровые координаты (где находятся персонажи)
        mouseWorld = mainCamera.ScreenToWorldPoint(mouseScreen);
        // 3. Обязательно обнуляем Z, чтобы луч был в 2D-плоскости
        mouseWorld.z = 0f;

        // 4. Задаём начало луча: позиция персонажа (transform.position)
        laserLine.SetPosition(0, transform.position);
        // 5. Задаём конец луча: позиция курсора в мире
        laserLine.SetPosition(1, mouseWorld);
    }
}