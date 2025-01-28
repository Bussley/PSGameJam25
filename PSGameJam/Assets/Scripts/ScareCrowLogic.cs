using UnityEngine;

public class ScareCrowLogic : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Crop")
            collision.gameObject.GetComponent<CropLogic>().scareCrowProtected = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Crop")
            collision.gameObject.GetComponent<CropLogic>().scareCrowProtected = false;
    }
}
