// Hero/Scripts/Controllers/PlayerMovementController.cs
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private HeroState _heroState;
    private Rigidbody2D _rb;
    private Vector2 _currentMoveInput;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        if (_heroState == null) _heroState = GetComponent<HeroState>();
    }

    private void OnEnable()
    {
        _heroState.OnMoveInputChanged += HandleMoveInputChanged;
        _heroState.OnDeath += HandleDeath;
    }

    private void OnDisable()
    {
        _heroState.OnMoveInputChanged -= HandleMoveInputChanged;
        _heroState.OnDeath -= HandleDeath;
    }

    private void HandleMoveInputChanged(Vector2 input)
    {
        _currentMoveInput = input;
    }

    private void HandleDeath()
    {
        _currentMoveInput = Vector2.zero;
    }

    private void FixedUpdate()
    {
        _rb.linearVelocity = _currentMoveInput * _heroState.MoveSpeed;
    }
}