// Hero/Scripts/Controllers/PlayerHealthController.cs
using System.Collections;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    [SerializeField] private HeroState _heroState;
    [SerializeField] private float _invincibilityDuration = 0.5f;
    private bool _isInvincible;

    private void Awake()
    {
        if (_heroState == null) _heroState = GetComponent<HeroState>();
    }

    private void OnEnable()
    {
        _heroState.OnDeath += HandleDeath;
    }

    private void OnDisable()
    {
        _heroState.OnDeath -= HandleDeath;
    }

    /// <summary> Вызывается извне (вражеские пули, ловушки) </summary>
    public void TakeDamage(int damage)
    {
        if (_heroState.IsDead || _isInvincible) return;

        _heroState.ApplyDamage(damage);

        if (!_heroState.IsDead)
            StartCoroutine(InvincibilityRoutine());
    }

    public void Heal(int amount)
    {
        _heroState.Heal(amount);
    }

    private IEnumerator InvincibilityRoutine()
    {
        _isInvincible = true;
        yield return new WaitForSeconds(_invincibilityDuration);
        _isInvincible = false;
    }

    private void HandleDeath()
    {
        GetComponent<Collider2D>().enabled = false;
    }
}