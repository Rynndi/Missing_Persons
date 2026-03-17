using UnityEngine;
using Pathfinding; 

//controller
public class Controller : MonoBehaviour
{
    Animator animator; 
    [SerializeField] Transform goal;
    AILerp aI;  

    void Start()
    {
        aI = GetComponent<AILerp>();
        animator = GetComponent<Animator>();

        if (animator == null) { 
            Debug.LogError("killer Controller.cs: animator not found"); 

        }
        
    }

    // Update is called once per frame
    void Update()
    {
        aI.destination = goal.position; 
        UpdateAnimation(); 

    }

      void UpdateAnimation()
    {
        if (animator == null || aI == null) return;

        Vector3 velocity3 = aI.velocity;
        Vector2 velocity = new Vector2(velocity3.x, velocity3.y);

        bool up = false;
        bool down = false;
        bool left = false;
        bool right = false;

       
        if (Mathf.Abs(velocity.x) > Mathf.Abs(velocity.y))
            {
                if (velocity.x > 0)
                    right = true;
                else if (velocity.x < 0)
                    left = true;
            }
            else
            {
                if (velocity.y > 0)
                    up = true;
                else if (velocity.y < 0)
                    down = true;
        }
        

        animator.SetBool("up", up);
        animator.SetBool("down", down);
        animator.SetBool("left", left);
        animator.SetBool("right", right);
    }
}
