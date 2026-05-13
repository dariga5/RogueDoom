// Hero/Scripts/Controllers/PlayerAnimationController.cs
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private HeroState _heroState;
    private Animator _anim;
    private SpriteRenderer _spriteRenderer;

    private static readonly int MoveXParam = Animator.StringToHash("MoveX");
    private static readonly int MoveYParam = Animator.StringToHash("MoveY");
    private static readonly int DeathTriggerParam = Animator.StringToHash("Death");

    // Для поворота, когда персонаж не двигается
    private Vector2 _lastLookDirection;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (_heroState == null)
            _heroState = GetComponentInParent<HeroState>();
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

    // Движение – задаём MoveX/MoveY для выбора анимации бега
    private void HandleMoveInput(Vector2 input)
    {
        _anim.SetFloat(MoveXParam, input.x);
        _anim.SetFloat(MoveYParam, input.y);

    }

    // Направление мыши – используем ТОЛЬКО когда стоим на месте
    private void HandleLookDirection(Vector2 direction)
    {
        _lastLookDirection = direction;

        if (_heroState.MoveInput == Vector2.zero && direction.sqrMagnitude > 0.001f)
        {
            if (direction.x != 0)
                _spriteRenderer.flipX = direction.x < 0;

   
        }
    }

    private void HandleDeath()
    {
        _anim.SetTrigger(DeathTriggerParam);
    }
}