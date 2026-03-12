using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class NewMonoBehaviourScript : MonoBehaviour
{
    float movementX;
    float movementY;
    [SerializeField] float speed = 5.0f;
    Rigidbody2D rb;
    public ColManager cm;
    

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
            cm.ColCount--;
        }
        // if (other.gameObject.CompareTag("Exit")) && (cm.ColCount == 0)
        // {
            // UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        // }
        
        if (cm.ColCount == 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
        
    }
}
