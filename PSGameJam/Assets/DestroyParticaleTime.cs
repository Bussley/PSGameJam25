using UnityEngine;

public class DestroyParticaleTime : MonoBehaviour
{
    private float m_Time;

    private void Awake()
    {
        m_Time = Time.time + 2.0f;
    }

    void Update()
    {
		float scale = m_Time - Time.time;
		GetComponent<AudioSource>().volume = scale * 0.05125f;
        if(m_Time < Time.time)
            Destroy(gameObject);
    }
}
