using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    public bool isGrounded;
    private Rigidbody2D m_rigidBody2D;
    private SpriteRenderer m_spriteRenderer;
    private Animator m_animator;
    public float horizontalForce = 1000.0f;
    public float verticalForce = 10.0f;
    private Vector2 direction;


    // Start is called before the first frame update
    void Start()
    {
        m_rigidBody2D = GetComponent<Rigidbody2D>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isGrounded)
        {
           // if (Keyboard.current.aKey.wasPressedThisFrame)
           // {
           m_rigidBody2D.AddForce(direction * horizontalForce * Time.deltaTime);
           // }
        }

        if(Mathf.Abs(m_rigidBody2D.velocity.x) > 0.09f)
        {
            m_animator.SetInteger("AnimState", 1);
        }


        if (Mathf.Approximately(m_rigidBody2D.velocity.x, 0.0f))
        {
            m_animator.SetInteger("AnimState", 0);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log("Move Activated");

        direction = context.ReadValue<Vector2>();

        switch (context.control.name)
        {
            case "a":
            case "leftArrow":
                m_spriteRenderer.flipX = true;
                //context.action.ReadValue<Vector2>();
                break;
            case "d":
            case "rightArrow":
                m_spriteRenderer.flipX = false;
                break;
        }
    }

    //private void _MoveLeft()
    //{
    //    m_rigidBody2D.AddForce(Vector2.left * horizontalForce * Time.deltaTime);
    //}
    //
    //private void _MoveRight()
    //{
    //    m_rigidBody2D.AddForce(Vector2.right * horizontalForce * Time.deltaTime);
    //}

    public void OnJump(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            Debug.Log("Jump Activated");
            m_rigidBody2D.AddForce(Vector2.up * verticalForce);
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        isGrounded = true;
    }

    public void OnCollisionExit2D(Collision2D other)
    {
        isGrounded = false;
    }
}
