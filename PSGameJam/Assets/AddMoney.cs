using System.ComponentModel;
using UnityEngine;

public class AddMoney : MonoBehaviour
{
    public ScoreSystem system;
    void OnTriggerEnter2D(Collider2D collision) {
        system.AddPoint();
        Debug.Log("Adding money");
    }

}
