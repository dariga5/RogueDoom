using System;
using UnityEngine;

public class HeroState : MonoBehaviour
{   
    [Header("Движение")]
    public Vector2 Direction { get; set; } = Vector2.zero;
    public bool IsMoving {get; set;}

    public Action<Vector2> OnDirectionChanged;

}
