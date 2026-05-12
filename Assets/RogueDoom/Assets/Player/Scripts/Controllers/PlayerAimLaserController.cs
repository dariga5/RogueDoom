using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class PlayerAimLaser : MonoBehaviour
{
    [SerializeField] private HeroState _heroState;
    [SerializeField] private Transform _firePoint;
    
    [Tooltip("Если true, луч упрётся в препятствие на пути к мыши.")]
    [SerializeField] private bool _stopOnObstacles = true;
    [SerializeField] private LayerMask _obstacleMask;

    [Tooltip("Максимальная дистанция, если мышь дальше. 0 = без ограничений.")]
    [SerializeField] private float _maxDistance = 0f;  // 0 — без ограничения

    private LineRenderer _lr;
    private Camera _mainCamera;

    private void Awake()
    {
        _lr = GetComponent<LineRenderer>();
        if (_heroState == null) _heroState = GetComponentInParent<HeroState>();
        if (_firePoint == null) _firePoint = transform;
        _mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        _heroState.OnLookDirectionChanged += UpdateLaser;
        _heroState.OnDeath += HideLaser;
    }

    private void OnDisable()
    {
        _heroState.OnLookDirectionChanged -= UpdateLaser;
        _heroState.OnDeath -= HideLaser;
    }

    private void UpdateLaser(Vector2 lookDirection)
    {
        if (_heroState.IsDead) return;

        Vector3 mouseScreen = Input.mousePosition;
        Vector3 mouseWorld = _mainCamera.ScreenToWorldPoint(mouseScreen);
        mouseWorld.z = 0f;

        Vector2 start = _firePoint.position;
        Vector2 targetPoint = mouseWorld;

        // Ограничение максимальной дистанции, если задано
        if (_maxDistance > 0f)
        {
            Vector2 toMouse = targetPoint - start;
            if (toMouse.sqrMagnitude > _maxDistance * _maxDistance)
            {
                targetPoint = start + toMouse.normalized * _maxDistance;
            }
        }

        Vector2 end = targetPoint;

        // Проверка препятствий между началом и конечной точкой
        if (_stopOnObstacles)
        {
            Vector2 dir = (targetPoint - start).normalized;
            float dist = Vector2.Distance(start, targetPoint);
            RaycastHit2D hit = Physics2D.Raycast(start, dir, dist, _obstacleMask);
            if (hit.collider != null)
            {
                end = hit.point;
            }
        }

        _lr.enabled = true;
        _lr.SetPosition(0, start);
        _lr.SetPosition(1, end);
    }

    private void HideLaser()
    {
        _lr.enabled = false;
    }
}