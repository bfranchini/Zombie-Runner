using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class UI : MonoBehaviour
{
    public Text HealthText;
    public Text MagText;
    public Text AmmoText;
    public Text NotificationText;
    private bool gamePaused;
    private Transform backToMenuButton;
    private RawImage crosshair;
    private FirstPersonController firstPersonController;
    private Eyes eyes;
    private Gun gun;
    private bool lockCursor = true;

    private void Start()
    {
        backToMenuButton = transform.Find("BackToMenuButton");
        crosshair = GetComponentInChildren<RawImage>();
        firstPersonController = FindObjectOfType<FirstPersonController>();
        eyes = FindObjectOfType<Eyes>();
        gun = FindObjectOfType<Gun>();
    }

    private void Update()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
            
        if (Input.GetButtonDown("Pause") && !gamePaused)
        {
            PauseGame();
            return;
        }
            
        if(Input.GetButtonDown("Pause") && gamePaused)
            Unpause();
    }

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

    public void SetNotificationText(string text, int timeout = 5)
    {
        NotificationText.text = text;
        Invoke("ResetNotificationText", timeout);
    }

    public void ResetNotificationText()
    {
        NotificationText.text = "";
    }

    public void DisableCrosshair()
    {        
        crosshair.enabled = false;
    }

    public void EnableCrosshair()
    {
        crosshair.enabled = true;
    }

    public void EnableBackToMenuButton()
    {
        lockCursor = false;
        backToMenuButton.gameObject.SetActive(true);
    }

    public void DisableBacktoMenuButton()
    {
        lockCursor = true;
        backToMenuButton.gameObject.SetActive(false);
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        gamePaused = true;
        DisableCrosshair();
        EnableBackToMenuButton();
        firstPersonController.enabled = false;
        eyes.enabled = false;
        gun.enabled = false;
    }

    private void Unpause()
    {
        Time.timeScale = 1;
        gamePaused = false;
        EnableCrosshair();
        DisableBacktoMenuButton();
        firstPersonController.enabled = true;
        eyes.enabled = true;
        gun.enabled = true;
    }
}
