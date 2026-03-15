using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    float movementX;
    float movementY;
    [SerializeField] float speed = 5.0f;
    Rigidbody2D rb;
    Animator animator;


    public int count = 6;

    bool paused;

		public void pause()
		{
			paused = true;
		}
		public void resume()
		{
			paused = false;
        }
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();
        
        // cm.ColCount = 2;
    }

    void OnMove(InputValue value)
    {
        Vector2 v = value.Get<Vector2>();
        movementX = v.x;
        movementY = v.y;
        Debug.Log("Movement X = " + movementX);
        Debug.Log("Movement Y = " + movementY);

    }

    // void FixedUpdate()
    // {
    //     if (paused) return;

    //     if (Input.GetKeyDown("w"))
    //     {
    //         animator.SetBool("up", true);
    //     }
    //     else if (Input.GetKeyUp("w")){
    //         animator.SetBool("up", false);
    //     }

    //     if (Input.GetKeyDown("s"))
    //     {
    //         animator.SetBool("down", true);
    //     }
    //     else if (Input.GetKeyUp("s")){
    //         animator.SetBool("down", false);
    //     }

    //     if (Input.GetKeyDown("a"))
    //     {
    //         animator.SetBool("up", true);
    //     }
    //     else if (Input.GetKeyUp("a")){
    //         animator.SetBool("up", false);
    //     }

    //     if (Input.GetKeyDown("d"))
    //     {
    //         animator.SetBool("right", true);
    //     }
    //     else if (Input.GetKeyUp("d")){
    //         animator.SetBool("right", false);
    //     }

    //     float movementDistanceX = movementX * speed * Time.deltaTime;
    //     float movementDistanceY = movementY * speed * Time.deltaTime;
    //     transform.position = new Vector2(transform.position.x + movementDistanceX, transform.position.y + movementDistanceY);
    // }
    void FixedUpdate(){
        if (paused) return;

        animator.SetBool("up", movementY > 0);
        animator.SetBool("down", movementY < 0);
        animator.SetBool("right", movementX > 0);
        animator.SetBool("left", movementX < 0);

        float movementDistanceX = movementX * speed * Time.deltaTime;
        float movementDistanceY = movementY * speed * Time.deltaTime;

        transform.position = new Vector2(
            transform.position.x + movementDistanceX,
            transform.position.y + movementDistanceY
        );
    }
    // Update is called once per frame
    //  void FixedUpdate() { 
    //     //because global, can take from any function 
    //     // float movementDistanceX = movementX * speed * Time.deltaTime; 
    //     // float movementDistanceY = movementY * speed * Time.deltaTime; 
    //     // transform.position = new Vector2(transform.position.x + movementDistanceX, transform.position.y + movementDistanceY);
    //     // rb.linearVelocity = new Vector2(movementX * speed, movementY * speed); 
    //     // rb.linearVelocity = new Vector2(movementX * speed, rb.linearVelocity.y);
      
    //     if (isSprinting) { 
    //         currSpeed = sprintSpeed; 
    //     }
    //     else { 
    //         currSpeed = walkspeed; 
    //     }
    //     rb.linearVelocity = new Vector2(movementX * currSpeed, rb.linearVelocity.y);


    //     //jumping code 
      
    //     // if (movementY >0 && isGrounded) 
    //     // { 
    //     //     rb.AddForce(new Vector2(0,100)); 
    //     //     jumpCount+=1; 
    //     //     animator.SetBool("isJumping", true); 
    //     // }
    //     // jumping code (FixedUpdate)
    //     if (jumpPressed && jumpCount < maxJumps){
    //         rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
    //         //Add an instant force impulse to the rigidbody2D, using its mass. 
    //         rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

    //         jumpCount++;
    //     }

    //     // prevent repeated jumps while holding input
    //     jumpPressed = false; 
    //     // else {
    //     //     animator.SetBool("isJumping", false);
    //     // }

    //     //is movement X close to 0? close to 0 -> player is not moving 
    //     //if movement x is not 0, set isRunning bool to true 
    //     if (!Mathf.Approximately(movementX, 0f)) { 
    //         animator.SetBool("isRunning", true);

    //         //flipX requires bool, if facing left flip the value
    //         //determine if facing left thru movementX is left, facing left  
    //         //set flipX to true 
    //         spriteRenderer.flipX = movementX < 0; 

    //     }
      
    //     else { 
    //         animator.SetBool("isRunning", false);
    //     }

    // }
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Collectible"))
        {
            Destroy(other.gameObject);
            count--;
            GlobalEvents.TriggerCollected();
        }
        // if (other.gameObject.CompareTag("Exit")) && (cm.ColCount == 0)
        // {
        // UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        // }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.gameObject.CompareTag("Killer"))
        {
            GlobalEvents.deathTriggered();
        }   
    }
}
