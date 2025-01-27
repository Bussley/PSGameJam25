using System.Collections;
using UnityEngine;

public class DestructibleEnviromentLogic : MonoBehaviour
{
    [SerializeField]
    private int health;
    [SerializeField]
    private float shakeTime;
    [SerializeField] 
    private AnimationCurve shakeCurve;

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

    private bool shaking = false;

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
        else if (!shaking)
        {
            shaking = true;
            StartCoroutine(ShakeObject());
        }


    }

    IEnumerator ShakeObject()
    {
        var start_pos = transform.position;
        float elapsed = 0f;

        while (elapsed < shakeTime)
        {
            elapsed += Time.deltaTime;
            float stength = shakeCurve.Evaluate(elapsed / shakeTime);
            transform.position = start_pos + (Vector3)UnityEngine.Random.insideUnitCircle * stength;
            yield return null;
        }

        shaking = false;
        transform.position = start_pos;
    }
}
