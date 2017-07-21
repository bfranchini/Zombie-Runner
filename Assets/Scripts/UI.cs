using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Text HealthText;
    public Text MagText;
    public Text AmmoText;

    public void UpdateHealth(float health)
    {
        HealthText.text = health >= 0 ? health.ToString() : "0";
    }

    public void UpdateMagCount(int bulletCount)
    {
        MagText.text = bulletCount.ToString();
    }

    public void UpdateAmmoCount(int ammoCount)
    {
        AmmoText.text = ammoCount.ToString();
    }
}
