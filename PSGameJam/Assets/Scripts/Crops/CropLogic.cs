using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CropLogic : MonoBehaviour
{
    [SerializeField]
    private CropScriptableObject cropSO;

    // Grows from 0 to 100, each crop has different stages based on percentage:
    // SEEDED (brand new crop, denotes early life. First 10% of life)
    // YOUNG (younger half of growing. 20-70% of growing)
    // ALMOST-RIPE (denotes that this will soon be ready 70-99% of growing)
    // RIPE (ready to harvest. 100% of growing)
    private float growthPercentage;

    private float health;

    private bool watered;

    private void Start() {
        growthPercentage = 0;
        health = cropSO.maxHealth;
    }
}
