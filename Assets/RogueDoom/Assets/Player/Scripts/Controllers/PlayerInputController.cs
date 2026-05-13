// Hero/Scripts/Controllers/PlayerInputController.cs
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] private HeroState _heroState;
    [SerializeField] private Camera _mainCamera;

    private void Awake()
    {
        if (_heroState == null) _heroState = GetComponent<HeroState>();
        if (_mainCamera == null) _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (_heroState.IsDead) return;

        // Движение
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")).normalized;
        
        _heroState.SetMoveInput(input);

        // Прицеливание по мыши
        Vector3 mouseWorld = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0f;
        Vector2 lookDir = (mouseWorld - transform.position).normalized;
        _heroState.SetLookDirection(lookDir);

        // Огонь
        if (Input.GetButton("Fire1"))   // задай в Input Manager или используй GetMouseButton(0)
        {
            _heroState.Fire();
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            _heroState.Die();
        }
    }
}