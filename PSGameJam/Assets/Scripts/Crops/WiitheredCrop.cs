using UnityEngine;

public class WiitheredCrop : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !collision.gameObject.GetComponent<PlayerController>().GetUsingJets())
        {
            transform.parent.gameObject.GetComponent<SoilLogic>().RemoveCrop();
        }
    }
}
