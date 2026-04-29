using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;   
    
    private Rigidbody2D rb;    
    private Vector2 moveDirection;
    private Animator animator;

    private float moveX = 0;
    private float moveY = 0;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal"); // -1 (влево), 0, 1 (вправо)
        moveY = Input.GetAxisRaw("Vertical");   // -1 (вниз), 0, 1 (вверх)
        
        if(moveX == 1)
        {
            animator.SetBool("IsRunningRigth", true);        
        } else
        {
            animator.SetBool("IsRunningRigth", false);  
        }
         

        moveDirection = new Vector2(moveX, moveY);

        rb.linearVelocity = moveDirection * speed;

    }
}