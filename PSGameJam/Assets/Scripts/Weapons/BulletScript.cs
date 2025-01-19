using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletScript : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    public float forces;
    [SerializeField] private float destroyTime = 3f;
    [SerializeField] private LayerMask whatDestroysBullet;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;
        rb.linearVelocity = new Vector3(direction.x, direction.y).normalized * forces;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, 1);
        // NEED TO FIGURE OUT Z AXIS That is how the bullet drops...
        SetDestroyTime(); 

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnCollisionEnter2D (Collision2D collision)
    {
        Destroy(collision.gameObject);
        Destroy(gameObject);
    }
    void OnTriggerEnter2D (Collider2D collision)
    {
        if ((whatDestroysBullet.value & (1 << collision.gameObject.layer)) > 0)
        {
            Destroy(gameObject);
        }
    }
    private void SetDestroyTime()
    {
        Destroy(gameObject, destroyTime);
    }
    
}
