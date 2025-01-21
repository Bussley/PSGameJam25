using UnityEngine;

public class LaserBeamLogic : MonoBehaviour
{
    private float speed;
    private Vector3 dir;

    void Update()
    {
        transform.localScale = transform.localScale + new Vector3(speed, 0.0f, 0.0f);
        transform.position = transform.position + (dir * (speed/2));
        TileManager.ChangeTile(transform.position + (dir * (transform.lossyScale.x/2)));
    }

    public void ExpandBeam(float _speed, Vector3 _dir) {
        speed = _speed;
        dir = _dir;
    }
}
