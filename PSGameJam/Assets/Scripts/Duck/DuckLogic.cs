using UnityEngine;

public class DuckLogic : MonoBehaviour
{
    [SerializeField]
    private float minTime;
    [SerializeField] 
    private float maxTime;
    [SerializeField]
    private Vector3 flySpot;
    [SerializeField]
    private Vector3 originSpot;
    [SerializeField]
    private float flyInSpeed;
    private Animator animator;

    private float flyInTime;
    private bool onSpot;
    private bool flyOut;


    private void Awake() {
        onSpot = false;
        flyOut = false;
        animator = GetComponent<Animator>();
        flyInTime = Time.time + Random.Range(minTime, maxTime);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!onSpot && flyInTime < Time.time) {
            FlyIn();
        }
        else if(flyOut) {
            FlyOut();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (onSpot)
        {
            if (collision.gameObject.tag == "SeedPellete")
                transform.localScale = transform.localScale * 1.01f;
            else
            {
                animator.SetBool("OnSpot", false);
                flyOut = true;
            }
        }
    }
    private void FlyIn()
    {
        transform.position = Vector3.MoveTowards(transform.position, flySpot, flyInSpeed * Time.deltaTime);

        if (transform.position == flySpot)
        {
            animator.SetBool("OnSpot", true);
            onSpot = true;
        }
    }

    private void FlyOut()
    {
        if (onSpot)
        {
            transform.position = Vector3.MoveTowards(transform.position, originSpot, flyInSpeed * Time.deltaTime);

            if (transform.position == originSpot)
            {
                flyInTime = Time.time + Random.Range(minTime, maxTime);
                onSpot = false;
                flyOut = false;
            }
        }
    }
}
