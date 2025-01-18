using UnityEngine;

[CreateAssetMenu(fileName = "CropScriptableObject", menuName = "Scriptable Objects/CropScriptableObject")]
public class CropScriptableObject : ScriptableObject
{
    public string cropType;

    public float growSpeed;
    public float amountWaterNeeded;
    public float maxHealth;

    public Sprite youngCropSprite;
    public Sprite growingCropSprite;
    public Sprite ripeCropSprite;
}
