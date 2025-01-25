using UnityEngine;

public class FlameThrowerLogic : MonoBehaviour
{
    [SerializeField]
    private GameObject flameVFX;

    private void Awake()
    {
        Instantiate(flameVFX, transform);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Soil to destroy crop
        if (collision.gameObject.tag == "Soil")
            collision.gameObject.GetComponent<SoilLogic>().RemoveCrop();
    }
}
