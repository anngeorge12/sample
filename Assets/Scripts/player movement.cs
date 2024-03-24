
using System;
using UnityEngine;

public class playermovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    [SerializeField]private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private float walljumpCooldown;
    private float horizontalInput;

    private void Awake()
    {
        //help us grab referenecs for rigid body and animator
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        
        // changing the players direction while moving left
        if(horizontalInput>0.01f)
            transform.localScale = Vector3.one;
        else if(horizontalInput<-0.01f)
            transform.localScale = new Vector3(-1,1,1);

        //set animator parameters if zero it goes to idle state that is run is false
        //if run is true then the animator goes to its running state and becomes true
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());

        //wall jumping method
        if (walljumpCooldown > 0.2f)
        {
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
            if (onWall() && !isGrounded())
            {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
                //this means the payer will get stuck and he will not fall down
            }
            else
                body.gravityScale = 7;

            if (Input.GetKey(KeyCode.Space))
                Jump();
        }
        else
            walljumpCooldown += Time.deltaTime;
    }
    private void Jump()
    {
        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, 0);
            body.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetTrigger("jump");
        }
        else if(onWall() && !isGrounded())
        {
            if(horizontalInput==0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
            walljumpCooldown = 0;
                //transform.localScale.x means the scale of the player in x axis
                //mathf.sign which gives the sign of a number where 1(while facing right and -1(while facing left))
        }
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center,boxCollider.bounds.size,0,Vector2.down,0.1f,groundLayer);
        //try understanding what is raycastinng and boxcasting will be put in detail later
        return raycastHit.collider!=null;
    }
    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x,0), 0.1f, wallLayer) ;
        //raycastinng and boxcasting will be put in detail later
        //the center point is the origin of the box
        return raycastHit.collider != null;
    }
    public Boolean canAttack()
    {
        return horizontalInput == 0 && isGrounded() && !onWall();
    }
}
