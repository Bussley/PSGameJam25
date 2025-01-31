using System;
using UnityEngine;

public class CrowLogic : MonoBehaviour
{
    [SerializeField]
    private float flyInSpeed;

    [SerializeField]
    private Vector3 spawnPoint;

    [SerializeField]
    private AudioSource sfx;

    private Animator animator;
    private GameObject cropTarget;
    private bool scaredAway;
    private bool onSpot;

    private void Awake()
    {
        scaredAway = false;
        transform.position = spawnPoint;
        animator = GetComponent<Animator>();
        animator.SetBool("OnSpot", false);
    }

    private void Start()
    {
        cropTarget.GetComponent<CropLogic>().targeted = true;
    }

    void FixedUpdate()
    {
        // Crop could be destroyed before crow gets to it 
        if (cropTarget != null && !scaredAway)
        {
            transform.position = Vector3.MoveTowards(transform.position, cropTarget.transform.position, flyInSpeed * Time.deltaTime);

            if (transform.position == cropTarget.transform.position && !onSpot)
            {
                cropTarget.GetComponent<CropLogic>().CrowOnCrop(true);
                animator.SetBool("OnSpot", true);
                onSpot = true;
                sfx.Play();
                CawRandomly();
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, spawnPoint, flyInSpeed * Time.deltaTime);

            animator.SetBool("OnSpot", false);
            transform.localScale = Vector3.one;
            if (transform.position == spawnPoint)
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10 || collision.gameObject.tag == "Player")
        {
            sfx.Play();
            animator.SetBool("OnSpot", false);
            transform.localScale = Vector3.one;
            scaredAway = true;
            onSpot = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == cropTarget)
        {
            collision.gameObject.GetComponent<CropLogic>().targeted = false;
            collision.gameObject.GetComponent<CropLogic>().CrowOnCrop(false);
        }
    }

    public void TargetCrop(GameObject target)
    {
        cropTarget = target;
    }

    private void CawRandomly()
    {
        Action action = () =>
        {
            if(this!=null)
                sfx.Play();

            CawRandomly();
        };
        TimerManager.AddTimer(action, UnityEngine.Random.Range(10.0f, 25.0f));
    }
}
