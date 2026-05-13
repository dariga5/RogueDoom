// Hero/Scripts/Core/HeroState.cs
using UnityEngine;
using System;

public class HeroState : MonoBehaviour
{
    // ---------- Здоровье ----------
    [Header("Health")]
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _currentHealth;

    public int MaxHealth => _maxHealth;
    public int CurrentHealth => _currentHealth;

    // События здоровья
    public event Action<int, int> OnHealthChanged;   // (current, max)
    public event Action OnDeath;

    // ---------- Движение ----------
    [Header("Movement")]
    [SerializeField] private float _moveSpeed = 8f;

    public float MoveSpeed => _moveSpeed;
    public Vector2 MoveInput { get; private set; }
    public event Action<Vector2> OnMoveInputChanged;

    // ---------- Прицеливание ----------
    public Vector2 LookDirection { get; private set; }
    public event Action<Vector2> OnLookDirectionChanged;

    // ---------- Огонь ----------
    public event Action OnFirePerformed;

    // ---------- Состояние ----------
    public bool IsDead { get; private set; }
    public bool CanMove { get; set; } = true;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    /// <summary> Вызывается из PlayerInputController для установки ввода движения </summary>
    public void SetMoveInput(Vector2 input)
    {
        if (MoveInput == input) return;
        MoveInput = input;
        OnMoveInputChanged?.Invoke(MoveInput);
    }

    /// <summary> Вызывается из PlayerAimController для установки направления взгляда </summary>
    public void SetLookDirection(Vector2 direction)
    {
        if (LookDirection == direction) return;
        LookDirection = direction;
        OnLookDirectionChanged?.Invoke(LookDirection);
    }

    /// <summary> Вызывается из PlayerInputController при нажатии кнопки огня </summary>
    public void Fire()
    {
        if (IsDead) return;
        OnFirePerformed?.Invoke();
    }

    /// <summary> Нанести урон (вызывается из PlayerHealthController после проверок) </summary>
    public void ApplyDamage(int damage)
    {
        if (IsDead) return;

        _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, _maxHealth);
        OnHealthChanged?.Invoke(_currentHealth, _maxHealth);

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    /// <summary> Восстановить здоровье </summary>
    public void Heal(int amount)
    {
        if (IsDead) return;
        _currentHealth = Mathf.Clamp(_currentHealth + amount, 0, _maxHealth);
        OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
    }

    public void Die()
    {
        IsDead = true;
        MoveInput = Vector2.zero;
        
        OnMoveInputChanged?.Invoke(Vector2.zero);
        OnDeath?.Invoke();
    }
}