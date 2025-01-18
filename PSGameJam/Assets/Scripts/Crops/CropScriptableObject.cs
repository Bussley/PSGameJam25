using UnityEngine;

[CreateAssetMenu(fileName = "CropScriptableObject", menuName = "Scriptable Objects/CropScriptableObject")]
public class CropScriptableObject : ScriptableObject
{
    public string cropType;

    public float growTime;
    public float optimalWaterLevel;
    public float maxHealth;

    public Sprite cropSprite;
}
