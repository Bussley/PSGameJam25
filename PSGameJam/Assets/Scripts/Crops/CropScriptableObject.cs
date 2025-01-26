using UnityEngine;

[CreateAssetMenu(fileName = "CropScriptableObject", menuName = "Scriptable Objects/CropScriptableObject")]
public class CropScriptableObject : ScriptableObject
{
    public string cropType;

    public int baseMarketValue;

    // Time it takes to grow by 1%
    public float growSpeedTime;

    public int growToYoungCrop;
    public int growToGrowingCrop;

    public float cropDurabilityTime;
    
    public Sprite CropSprite;
    public Sprite seedSprite;
    public Sprite youngCropSprite;
    public Sprite growingCropSprite;
    public Sprite ripeCropSprite;


    public static float DehydrationSpeedTime = 0.1f;
}
