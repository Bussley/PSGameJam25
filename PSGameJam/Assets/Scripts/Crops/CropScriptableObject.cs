using UnityEngine;

[CreateAssetMenu(fileName = "CropScriptableObject", menuName = "Scriptable Objects/CropScriptableObject")]
public class CropScriptableObject : ScriptableObject
{
    public string cropType;

    // Time it takes to grow by 1%
    public float growSpeedTime;

    public int growToYoungCrop;
    public int growToGrowingCrop;

    public float amountWaterNeeded;
    public float maxHealth;

    public Sprite youngCropSprite;
    public Sprite growingCropSprite;
    public Sprite ripeCropSprite;
}
