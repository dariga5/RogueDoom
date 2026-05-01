using UnityEngine;

public class Animatoins : MonoBehaviour
{   
    [SerializeField] 
    private HeroState state;

    private Animator animator;
    

    void Awake()
    {
        animator = GetComponent<Animator>();

        if (state == null) 
            state = GetComponent<HeroState>();
    }

    void OnEnable()
    {
        state.OnDirectionChanged += UpdateMovement;
    }
    void OnDisable()
    {
        state.OnDirectionChanged -= UpdateMovement;
    }

    private void UpdateMovement(Vector2 vec)
    {
        if(vec.x == 1f)
        {
            animator.SetBool("IsRunningRigth", true);
        } 
        else
        {
            animator.SetBool("IsRunningRigth", false);
            
        }

    }
}
