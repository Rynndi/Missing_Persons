using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    float movementX;
    float movementY;
    [SerializeField] float speed = 5.0f;
    Rigidbody2D rb;

    //comment out for now 
    // Animator animator;


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
        //comment out for now
        // animator = GetComponent<Animator>();
        
      
    }

    void OnMove(InputValue value)
    {
        Vector2 v = value.Get<Vector2>();
        movementX = v.x;
        movementY = v.y;
        Debug.Log("Movement X = " + movementX);
        Debug.Log("Movement Y = " + movementY);

    }

        void FixedUpdate(){
            //comment out for now
        // if (paused) return;

        // animator.SetBool("up", movementY > 0);
        // animator.SetBool("down", movementY < 0);
        // animator.SetBool("right", movementX > 0);
        // animator.SetBool("left", movementX < 0);

        float movementDistanceX = movementX * speed * Time.deltaTime;
        float movementDistanceY = movementY * speed * Time.deltaTime;

        transform.position = new Vector2(
            transform.position.x + movementDistanceX,
            transform.position.y + movementDistanceY
        );
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
