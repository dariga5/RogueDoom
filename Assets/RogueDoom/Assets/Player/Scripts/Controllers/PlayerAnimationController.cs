// Hero/Scripts/Controllers/PlayerAnimationController.cs
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private HeroState _heroState;
    private Animator _anim;
    private SpriteRenderer _spriteRenderer;

    private static readonly int SpeedParam = Animator.StringToHash("Speed");
    private static readonly int IsDeadParam = Animator.StringToHash("IsDead");

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (_heroState == null) _heroState = GetComponentInParent<HeroState>();
    }

    private void OnEnable()
    {
        _heroState.OnMoveInputChanged += HandleMoveInput;
        _heroState.OnLookDirectionChanged += HandleLookDirection;
        _heroState.OnDeath += HandleDeath;
    }

    private void OnDisable()
    {
        _heroState.OnMoveInputChanged -= HandleMoveInput;
        _heroState.OnLookDirectionChanged -= HandleLookDirection;
        _heroState.OnDeath -= HandleDeath;
    }

    private void HandleMoveInput(Vector2 input)
    {
        _anim.SetFloat(SpeedParam, input.magnitude);
    }

    private void HandleLookDirection(Vector2 direction)
    {
        if (direction.x != 0)
            _spriteRenderer.flipX = direction.x < 0;
    }

    private void HandleDeath()
    {
        _anim.SetBool(IsDeadParam, true);
    }
}