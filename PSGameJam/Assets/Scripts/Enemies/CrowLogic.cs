using UnityEngine;

public class CrowLogic : MonoBehaviour
{
    [SerializeField]
    private float flyInSpeed;

    [SerializeField]
    private Vector3 spawnPoint;

    private GameObject cropTarget;
    private bool scaredAway;

    private void Awake()
    {
        scaredAway = false;
        transform.position = spawnPoint;
    }

    void Update()
    {
        // Crop could be destroyed before crow gets to it 
        if (cropTarget != null && !scaredAway)
            transform.position = Vector3.MoveTowards(transform.position, cropTarget.transform.position, flyInSpeed * Time.deltaTime);
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, spawnPoint, flyInSpeed * Time.deltaTime);

            if (transform.position == spawnPoint)
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Weapon" || collision.gameObject.tag == "Player")
            scaredAway = true;
    }

    public void TargetCrop(GameObject target)
    {
        cropTarget = target;
    }

}
