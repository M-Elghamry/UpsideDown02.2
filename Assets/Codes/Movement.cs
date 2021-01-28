using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

 
    
    public DialogueManager dialogue;


    [SerializeField] private GameObject player;
    [SerializeField] private float timeOffset;
    [SerializeField] Vector2 posOffset;
    

    private Vector3 velocity;

    //movement related
    private float speed;
    [SerializeField] private float jumpForce;
    private float moveInput;
    private Rigidbody2D rb;
  private bool facingRight = true;
    [SerializeField] private float crouchSpeed;
    [SerializeField] private float normalSpeed;

    //Jumping related
    private bool isGrounded;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask whatIsGround;

    private int extraJumps;
    [SerializeField] private int extraJumpsValue;

    //TP related
    [SerializeField] private Camera cam;
    Vector2 mousePos;
    [SerializeField] private float afterTpSpeed;
    [SerializeField] private bool canTp;

    [SerializeField] private float TpcooldownTime = 2;
    private float TpnextFireTime = 0;

    //Crouching related
    private bool overHead;
    [SerializeField] private Transform ceilingCheck;
    [SerializeField] private float headCheckRadius;
    [SerializeField] private Collider2D normalCol;
    [SerializeField] private Collider2D crouchCol;
    private bool isCrouching = false;
   

    //animation related
    [SerializeField] private Animator anim;
    [SerializeField] private Animator DialogueAnim;
    


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        extraJumps = extraJumpsValue;
        normalCol.enabled = true;
        crouchCol.enabled = false;
        speed = normalSpeed;
        DialogueAnim.SetBool("DialogueOpen", false);
       
   
    }



    private void LateUpdate()
    {

        //Cam Follow
        //camera start position
        Vector3 startPos = cam.transform.position;
        //players current position
        Vector3 endPos = player.transform.position;
        endPos.x += posOffset.x;
        endPos.y += posOffset.y;
        endPos.z = -10;

        cam.transform.position = Vector3.SmoothDamp(startPos, endPos, ref velocity, timeOffset);


    }

    void Update()
    {

      


            //Crouching
            overHead = Physics2D.OverlapCircle(ceilingCheck.position, headCheckRadius, whatIsGround);
        

        if (Input.GetKeyDown(KeyCode.C))
        {
            
            isCrouching = true;
        }else if (Input.GetKeyUp(KeyCode.C))
        {
            
            isCrouching = false;
        }

        if (isCrouching)
        {
            anim.SetBool("crouching", true);

            normalCol.enabled = false;
            crouchCol.enabled = true;
            speed = crouchSpeed;
            
        }else if (!isCrouching && overHead == false)
        {

            anim.SetBool("crouching", false);

            normalCol.enabled = true;
            crouchCol.enabled = false;
            speed = normalSpeed;
            
        }


        //Teleporting
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Time.time > TpnextFireTime && canTp == true)
        {

            if (Input.GetButtonDown("Fire2"))
            {
                transform.position = mousePos;
                TpnextFireTime = Time.time + TpcooldownTime;
            }

        }
        
        //Jumping
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        if (isGrounded == true)
        {
            extraJumps = extraJumpsValue;
        }

        if (Input.GetButtonDown("Jump") && extraJumps > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        }else if (Input.GetButtonDown("Jump") && extraJumps > 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpForce;
        }

        //Moving
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if (facingRight == false && moveInput > 0)
        {
            Flip();
        }else if (facingRight == true && moveInput < 0)
        {
            Flip();
        }

    }


    //Flip Character
    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
        
    }



    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Dialogue1")
        {
          DialogueAnim.SetBool("DialogueOpen", true);
        }
        
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Dialogue1")
        {
            DialogueAnim.SetBool("DialogueOpen", false);
        }
        dialogue.index = 0;
    }

}


