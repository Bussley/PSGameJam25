using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class SoilLogic : MonoBehaviour
{
    [SerializeField]
    private GameObject potatoPrefab;

    [SerializeField]
    private Sprite wateredSoil;

    [SerializeField]
    private Sprite drySoil;

    [SerializeField]
    private Sprite charredSoil;

    [SerializeField]
    private float charredTimer;

    private GameObject crop;
    private SpriteRenderer spr;
    private float invincibilityTime;

    private bool charred;

    void Awake()
    {
        // This controls charred time timer invicibilty so newly spawn soil doesn't go away
        invincibilityTime = Time.time + 1.0f;
        charred = false;
        spr = GetComponent<SpriteRenderer>();
    }

    public void AddSeed() {
        if(!charred && crop == null)
            crop = Instantiate(potatoPrefab, transform);
    }

    public void Watered()
    {
        spr.sprite = wateredSoil;
    }

    public void Dry()
    {
        spr.sprite = drySoil;
    }

    public void CharTile() {
        // Make sure not to char tile if just spawned
        if(Time.time > invincibilityTime && !charred)
        {
            charred = true;
            if(crop != null)
                Destroy(transform.GetChild(0).gameObject);

            spr.sprite = charredSoil;

            Action func = () =>
            {
                TileManager.ResetTile(transform.position);
            };

            TimerManager.AddTimer(func, charredTimer);
        }

    }
}
