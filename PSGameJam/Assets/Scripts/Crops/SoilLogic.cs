using UnityEngine;

public class SoilLogic : MonoBehaviour
{
    [SerializeField]
    private GameObject potatoPrefab;

    private GameObject crop;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddSeed() {
        if(crop == null)
            crop = Instantiate(potatoPrefab, transform);
    }
}
