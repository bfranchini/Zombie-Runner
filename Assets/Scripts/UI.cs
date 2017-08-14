using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Text HealthText;
    public Text MagText;
    public Text AmmoText;
    public Text NotificationText;

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

    public void SetNotificationText(string text)
    {
        NotificationText.text = text;
        Invoke("ResetNotificationText", 5);
    }

    public void ResetNotificationText()
    {
        NotificationText.text = "";
    }

    public void DisableCrosshair()
    {
        var crosshair = GetComponentInChildren<RawImage>();
        crosshair.enabled = false; 
    }

    public void EnableBackToMenuButton()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        var button = transform.Find("BackToMenuButton");
        button.gameObject.SetActive(true);
    }
}
