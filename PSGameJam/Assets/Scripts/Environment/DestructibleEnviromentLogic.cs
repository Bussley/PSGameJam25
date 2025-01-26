using UnityEngine;

public class DestructibleEnviromentLogic : MonoBehaviour
{
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
            Destroy(gameObject);
        else if (canBeDestroyedByShotgun && collision.gameObject.tag == "SeedPellete")
            Destroy(gameObject);
        else if(canBeDestroyedByFireHose && collision.gameObject.tag == "WaterShot")
            Destroy(gameObject);
        else if(canBeDestroyedByHarvestBlade && collision.gameObject.tag == "HarvestBlade")
            Destroy(gameObject);
        else if (canBeDestroyedByFlameThrower && collision.gameObject.tag == "FlameThrower")
            Destroy(gameObject);
        else if (canBeDestroyedByPlayer && collision.gameObject.tag == "Player")
            Destroy(gameObject);
    }
}
