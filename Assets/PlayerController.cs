using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class PlayerController : MonoBehaviour
{
    float movementX;
    float movementY;
    [SerializeField] float speed = 5.0f;
    [SerializeField]
    DialogueRunner dialogue;
    Rigidbody2D rb;

    
    Animator animator;


    public int count = 6;
    public bool nextScene = false;

    public bool paused = false;

		public void pause()
		{
			paused = true;
		}
		public void resume()
		{
			paused = false;
        }
    

  
    void Start(){

        PlayerInput playerInput = GetComponent<PlayerInput>(); 
        if (playerInput != null) { 
            playerInput.enabled = false; 
            playerInput.enabled = true;
        }
        else { 
            Debug.Log("not found");
        }

        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("PlayerController: Rigidbody2D not found.");
        }

        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("PlayerController: Animator not found.");
        }

        if (StateManager.Instance != null && StateManager.Instance.phase == 2 && StateManager.Instance.storedPos.x != 0)
        {
            transform.position = StateManager.Instance.storedPos;
        }
    }

    void OnMove(InputValue value)
    {
        Vector2 v = value.Get<Vector2>();
        movementX = v.x;
        movementY = v.y;
        Debug.Log("Movement X = " + movementX);
        Debug.Log("Movement Y = " + movementY);

    }

    //     void FixedUpdate(){
    //     Debug.Log("xdd");
    //         //comment out for now
    //     if (paused) return;

     

    //     float movementDistanceX = movementX * speed * Time.deltaTime;
    //     float movementDistanceY = movementY * speed * Time.deltaTime;

    //     var keyboard = Keyboard.current;
    //     var gamepad = Gamepad.current;

    //     Vector2 moveDirection = Vector2.zero;
    //     if (keyboard != null)
    //     {
    //         // Read WASD as a Vector2
    //         if (keyboard.wKey.isPressed) moveDirection.y += 1;
    //         if (keyboard.sKey.isPressed) moveDirection.y -= 1;
    //         if (keyboard.aKey.isPressed) moveDirection.x -= 1;
    //         if (keyboard.dKey.isPressed) moveDirection.x += 1;

    //         // Apply movement (example)
    //         transform.Translate(moveDirection * Time.deltaTime * 5f);
    //     }
    //     animator.SetBool("up", moveDirection.y > 0);
    //     animator.SetBool("down", moveDirection.y < 0);
    //     animator.SetBool("right", moveDirection.x > 0);
    //     animator.SetBool("left", moveDirection.x < 0);
    //     /*transform.position = new Vector2(
    //         transform.position.x + movementDistanceX,
    //         transform.position.y + movementDistanceY
    //     );*/
    // }

    void FixedUpdate()
{
    if (paused) return;

    // var keyboard = Keyboard.current;
    Vector2 moveDirection = new Vector2(movementX, movementY);

    transform.Translate(moveDirection.normalized * speed * Time.deltaTime); 
    if (animator != null)
    {
        animator.SetBool("up", moveDirection.y > 0);
        animator.SetBool("down", moveDirection.y < 0);
        animator.SetBool("right", moveDirection.x > 0);
        animator.SetBool("left", moveDirection.x < 0);
    }
}
   
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
        if (other.gameObject.CompareTag("GardenExit"))
        {
            if (dialogue != null)
            {
                nextScene = true;
                dialogue.StartDialogue("Warning");
            }
        }
        if (other.gameObject.CompareTag("HomeExit"))
        {
            if (StateManager.Instance.phase == 1)
            {
                dialogue.StartDialogue("CannotEnter");
            }
            else
            {
                nextScene = true;
                dialogue.StartDialogue("BackGarden");
            }
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
