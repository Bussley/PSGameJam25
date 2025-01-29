using UnityEngine;

public class DestroyParticaleTime : MonoBehaviour
{
    private float m_Time;

    private void Awake()
    {
        m_Time = Time.time + 0.4f;
    }

    void Update()
    {
        if(m_Time < Time.time)
            Destroy(gameObject);
    }
}
