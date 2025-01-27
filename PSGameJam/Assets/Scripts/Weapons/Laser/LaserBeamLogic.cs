using UnityEngine;

public class LaserBeamLogic : MonoBehaviour
{
    private float speed;
    private Vector3 dir;

    void FixedUpdate()
    {
        transform.localScale = transform.localScale + new Vector3(speed, 0.0f, 0.0f);
        transform.position = transform.position + (dir * (speed/2));
        TileManager.LaserTile(transform.position + (dir * (transform.lossyScale.x/2)));
    }

    public void ExpandBeam(float _speed, Vector3 _dir) {
        speed = _speed;
        dir = _dir;
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Soil")
            collision.gameObject.GetComponent<SoilLogic>().CharTile();

        if (collision.gameObject.tag == "DestructibleEnviroment")
            Destroy(gameObject);
    }
}
