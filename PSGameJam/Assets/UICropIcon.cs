using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;


public class UICropIcon : MonoBehaviour
{
    Image m_Image;

    private BountyLogic bLogic;

    private GameObject bontyObj;
    /*
    0 wheat
    1 tomato
    2 potato
    3 strawberry
    4 pepper
    5 eggplant
    6 balckberry
    */
    public List<Sprite> CropImages;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bontyObj = GameObject.FindGameObjectWithTag("CropMarketBoard");
        bLogic = bontyObj.GetComponent<BountyLogic>();
        m_Image = GetComponent<Image>();
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (bLogic.bountyCrop) {
            case "wheat":
                m_Image.sprite = CropImages[0];
                break;
            case "potatot":
                m_Image.sprite = CropImages[1];
                break;
            case "tomato":
                m_Image.sprite = CropImages[2];
                break;
            case "strawberry":
                m_Image.sprite = CropImages[3];
                break;
            case "pepper":
                m_Image.sprite = CropImages[4];
                break;
            case "eggplant":
                m_Image.sprite = CropImages[5];
                break;
            case "blackberry":
                m_Image.sprite = CropImages[6];
                break;
            default:
                m_Image.sprite = null;
                break;                                              
        }
        
        
    }
}
