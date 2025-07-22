using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed;
    public float jumpForce;

    private Rigidbody2D rb;
    private bool isGrounded;

    //public Inventory inventory;
    public delegate void DynamiteHandler();
    public event DynamiteHandler onDynamite;
    void Start()
    {
        //inventory = new Inventory();

        speed = 4f;
        jumpForce = 5f;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), this.transform.position) < 5f)
            {
                onDynamite.Invoke();
                Debug.Log("Player has been placed a dynamite.");
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}