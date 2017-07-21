using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Text HealthText;
    public Text MagText;
    public Text AmmoText;

    public void updateHealth(float health)
    {
        HealthText.text = health >= 0 ? health.ToString() : "0";
    }
}
