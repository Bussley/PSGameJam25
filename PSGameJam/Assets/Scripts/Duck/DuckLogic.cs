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

    private float flyInTime;
    private bool onSpot;
    private bool flyOut;


    private void Awake() {
        onSpot = false;
        flyOut = false;
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
        if(onSpot) 
            flyOut = true;
    }
    private void FlyIn()
    {
        transform.position = Vector3.MoveTowards(transform.position, flySpot, flyInSpeed * Time.deltaTime);

        if(transform.position == flySpot)
            onSpot = true;
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
