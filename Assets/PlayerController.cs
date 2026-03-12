using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    float movementX;
    float movementY;
    [SerializeField] float speed = 5.0f;
    Rigidbody2D rb;

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
        // animator = GetComponent<Animator>();
        // spriteRenderer = GetComponent<SpriteRenderer>();
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

    void FixedUpdate()
    {
        if (paused) return;

        float movementDistanceX = movementX * speed * Time.deltaTime;
        float movementDistanceY = movementY * speed * Time.deltaTime;
        transform.position = new Vector2(transform.position.x + movementDistanceX, transform.position.y + movementDistanceY);
    }
    // Update is called once per frame
    
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
