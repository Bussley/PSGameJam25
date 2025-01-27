using UnityEngine;

public class DestructibleEnviromentLogic : MonoBehaviour
{
    [SerializeField]
    private int health;

    [SerializeField]
    private bool canBeDestroyedByLaser;
    [SerializeField]
    private bool canBeDestroyedByShotgun;
    [SerializeField]
    private bool canBeDestroyedByFireHose;
    [SerializeField]
    private bool canBeDestroyedByHarvestBlade;
    [SerializeField]
    private bool canBeDestroyedByFlameThrower;
    [SerializeField]
    private bool canBeDestroyedByPlayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canBeDestroyedByLaser && collision.gameObject.tag == "Laser")
            HurtObject();
        else if (canBeDestroyedByShotgun && collision.gameObject.tag == "SeedPellete")
            HurtObject();
        else if (canBeDestroyedByFireHose && collision.gameObject.tag == "WaterShot")
            HurtObject();
        else if (canBeDestroyedByHarvestBlade && collision.gameObject.tag == "HarvestBlade")
            HurtObject();
        else if (canBeDestroyedByFlameThrower && collision.gameObject.tag == "FlameThrower")
            HurtObject();
        else if (canBeDestroyedByPlayer && collision.gameObject.tag == "Player")
            HurtObject();

    }

    private void HurtObject()
    {
        health -= 1;

        //Also object shake


        if (health <= 0)
            Destroy(gameObject);
    }
}
