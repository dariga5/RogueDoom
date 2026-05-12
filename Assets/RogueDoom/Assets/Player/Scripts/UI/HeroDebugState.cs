// Hero/Scripts/UI/HeroDebugState.cs
using UnityEngine;

public class HeroDebugState : MonoBehaviour
{
    [SerializeField] private HeroState _heroState;

    private void OnGUI()
    {
        if (_heroState == null) return;
        GUILayout.BeginArea(new Rect(10, 10, 350, 200));
        GUILayout.Label($"HP: {_heroState.CurrentHealth}/{_heroState.MaxHealth}");
        GUILayout.Label($"Move Input: {_heroState.MoveInput}");
        GUILayout.Label($"Look Dir: {_heroState.LookDirection}");
        GUILayout.Label($"IsDead: {_heroState.IsDead}");
        GUILayout.EndArea();
    }
}