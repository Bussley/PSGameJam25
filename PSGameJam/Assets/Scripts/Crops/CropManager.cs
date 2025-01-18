using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class CropManager : MonoBehaviour
{
    private List<GameObject> cropList;

    public void Start() {
        cropList = new List<GameObject>();
    }

    public void CreateCrop(CropScriptableObject crop) {

    }

    public void AddCrop(GameObject crop) {
        cropList.Add(crop);
    }

    public void DamageCrop(GameObject crop, float damage) {

    }

    private void CalculateQuality(GameObject crop) {
        // Formula to calculate crop on a scale from 1 to 10
    }
}
