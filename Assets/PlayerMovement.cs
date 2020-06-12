using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    bool facingRight = true;
    public float jumpForce = 400f;
    [Range(0, 1)]
    public float crouchSpeed = .36f;

    public bool airControl = false;
    public LayerMask whatisGround;

    Transform groundCheck;
    float groundRadius = .2f;
    bool grounded = false;
    Transform ceilingCheck;
    float ceilingRadius = .01f;
    Animator anim;
    Rigidbody2D rigidbody2D;
    Transform playerGraphic;
    // Start is called before the first frame update
    void Start()
    {
        groundCheck = transform.Find("GroundCheck");
        ceilingCheck = transform.Find("CeilingCheck");
        anim = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        playerGraphic = transform.Find("Graphic");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatisGround);
        anim.SetBool("Ground", grounded);
        anim.SetFloat("vSpeed", rigidbody2D.velocity.y);
    }
    public void Move(float move,bool crouch,bool jump)
    {
        if (!crouch && anim.GetBool("Crouch"))
        {
            if (Physics2D.OverlapCircle(ceilingCheck.position, ceilingRadius, whatisGround))
                crouch = true;
        }
        anim.SetBool("Crouch", crouch);
        if (grounded || airControl)
        {
            move = (crouch ? move * crouchSpeed : move);
            anim.SetFloat("Speed", Mathf.Abs(move));
            rigidbody2D.velocity = new Vector3(move * PlayerData.speed, rigidbody2D.velocity.y);
            if (move > 0 && !facingRight)
                Flip();
            else if (move < 0 && facingRight)
                Flip();
        }
        
        if (grounded && jump)
        {
            anim.SetBool("Ground", false);
            rigidbody2D.AddForce(new Vector2(0f, jumpForce));
        }    
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = playerGraphic.localScale;
        theScale.x *= -1;
        playerGraphic.localScale = theScale;
    }
}
