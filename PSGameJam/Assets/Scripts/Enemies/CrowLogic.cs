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

    void FixedUpdate()
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
        if (collision.gameObject.layer == 10 || collision.gameObject.tag == "Player")
            scaredAway = true;
        else if (collision.gameObject.tag == "Crop")
            collision.gameObject.GetComponent<CropLogic>().CrowOnCrop(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Crop")
        {
            collision.gameObject.GetComponent<CropLogic>().targeted = false;
            collision.gameObject.GetComponent<CropLogic>().CrowOnCrop(false);
        }
    }

    public void TargetCrop(GameObject target)
    {
        cropTarget = target;
    }


}
