using UnityEngine;

public class ScareCrowLogic : MonoBehaviour
{
    [SerializeField]
    private int hitPoints;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Crop")
            collision.gameObject.GetComponent<CropLogic>().scareCrowProtected = true;
        else if (collision.gameObject.tag == "Weapon")
        {
            hitPoints -= 1;

            if (hitPoints == 0)
                Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Crop")
            collision.gameObject.GetComponent<CropLogic>().scareCrowProtected = false;
    }
}
