﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{
	public TextMeshProUGUI hudScoreText;
	public TextMeshProUGUI hudMultiplierText;
	public Slider playerHealthSlider;
    public Slider overloadMeterSlider;
    public GameObject blaster1Image;
    public GameObject blaster2Image;
    public GameObject blaster3Image;

    [Header("Health Flash")]
    public int numberOfFlashes;
    public float timeBtwnFlashes;
    public Color flashColor;
    public Image healthBar;

    [Header("Hud Debugging")]
    public float hudScore;
    public float playerHealth = 100;
    public float overloadMeter = 100;
    public string currentHUDWeapon;

    public GameObject referencedGM;
    public GameObject referencedPlayer;
    public static void SetHUDActive()
    {
        GameObject.Find("Game Manager").SetActive(true);
    }
    public static void SetHUDDeactive()
    {
        GameObject.Find("Game Manager").SetActive(false);
    }
    public void OnEnable()
    {
        referencedGM = GameManager.gm.gameObject;
        referencedPlayer = GameManager.player.obj;
        GameManager.gm.HUD = gameObject;
        if (GameManager.gm.currentState == GameState.Gameplay)
        {
            //its good to stay active
        }
        else
        {
            gameObject.SetActive(false);
            GameManager.gm.OnBriefingEnd.AddListener(() => gameObject.SetActive(true));
        }
    }
    void Update()
    {
        //update score
        /* now handled by seperate script
        if (referencedGM != null)
        {
            hudScore = ScoreSystem.GetScore();
			hudScoreText.text = hudScore.ToString("00000");
			hudMultiplierText.text = hudScore.ToString("(x" + ScoreSystem.GetComboMultiplier() + ")");
		}
        */
        if (referencedPlayer != null)
        {
            playerHealth = referencedPlayer.GetComponent<PlayerController>().GetPlayerHealth();
            overloadMeter = referencedPlayer.GetComponent<PlayerController>().GetOverloadCharge();
            currentHUDWeapon = referencedPlayer.GetComponent<PlayerController>().GetCurrentWeaponID();

            switch (currentHUDWeapon)
            {
                case "Blaster":
                    blaster1Image.SetActive(true);
                    blaster2Image.SetActive(false);
                    blaster3Image.SetActive(false);
                    break;
                case "Energy Burst":
                    blaster1Image.SetActive(false);
                    blaster2Image.SetActive(true);
                    blaster3Image.SetActive(false);
                    break;
                case "Laser Beam":
                    blaster1Image.SetActive(false);
                    blaster2Image.SetActive(false);
                    blaster3Image.SetActive(true);
                    break;
            }
        }

        playerHealthSlider.value = playerHealth;
        overloadMeterSlider.value = overloadMeter;

        //update weapon icon


    }

    public void FlashHPHUD()
    {
        StartCoroutine(FlashHPHUDCoroutine());
    }

    IEnumerator FlashHPHUDCoroutine()
    {
        Color originalColor = healthBar.color;

        for (int i = 0; i < numberOfFlashes; i++)
        {
            print("hit");
            healthBar.color = flashColor;

            yield return new WaitForSeconds(timeBtwnFlashes);

            healthBar.color = originalColor;

            yield return new WaitForSeconds(timeBtwnFlashes);
        }
    }
}
