using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private HeroState state;
    
    private Rigidbody2D rb;
    private Vector2 input;

    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (state == null) 
            state = GetComponent<HeroState>();
    }

    void Update()
    {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        state.Direction = input;
        state.OnDirectionChanged?.Invoke(input);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + input * speed * Time.fixedDeltaTime);
    }
}