﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour
{
	[SerializeField] float playerHealth = 100f;
	[SerializeField] float overloadCharge = 0f;
	[SerializeField] float tempInvulnTime = 0.1f;
	[SerializeField] GameObject currentWeapon;
	public GameObject[] weapons;
    public bool isDodging = false;
    public bool isInvincible = false;
	public bool isOverloaded = false;
	private float cdInvuln = 0f;

	[Header("Effects")]
	[Range(0, 5)]
	[SerializeField] float cameraShakeOnHit = 1;
	public float CameraShakeOnHit { get => cameraShakeOnHit; }
	[SerializeField] UnityEvent OnHit;
	public UnityEvent OnDeath;
	bool firstWeaponObtained = false;
	[SerializeField] UnityEvent OnWeaponChanged;
	[SerializeField] float playerHealthLowThreshold = 1;
	float lastFramePlayerHealth;
	[SerializeField] UnityEvent OnHealthStartedBeingLow;
	[SerializeField] UnityEvent OnHealthStoppedBeingLow;
	[SerializeField] UnityEvent OnHealthIncreased;
	[SerializeField] UnityEvent OnOverloadChargeIncreased;


    private void Awake() {
		// Set references in game manager
		GameManager.player.obj = gameObject;
		GameManager.player.movement = GetComponent<PlayerMovement>();
		GameManager.player.controller = this;
    }

    private void Start()
	{
		SetWeapon(currentWeapon);
	}

	void Update()
    {
        InvokingHealthStartedOrStoppedBeingLowEvents();
    }

    private void InvokingHealthStartedOrStoppedBeingLowEvents()
    {
        bool currentlyLowHealth = playerHealth < playerHealthLowThreshold;
        bool lastFrameHealthWasLow = lastFramePlayerHealth < playerHealthLowThreshold;

        if (!lastFrameHealthWasLow && currentlyLowHealth)
            OnHealthStartedBeingLow.Invoke();
        else if (lastFrameHealthWasLow && !currentlyLowHealth)
            OnHealthStoppedBeingLow.Invoke();

        lastFramePlayerHealth = playerHealth;
    }

	public void DamagePlayer(float amount)
	{
        if (isDodging || isInvincible) //Not sure if dodging protects form environmental damage; if not, change this
        {
            return;
        }

		// Temporary player invulnerability on taking damage
		if (Time.time - cdInvuln > tempInvulnTime + 0.01f)
		{
			cdInvuln = Time.time;

			playerHealth -= amount;
			Debug.Log("<color=red>Player took " + amount + " damage!</color>");
			ScoreSystem.ResetCombo();

			if (playerHealth <= 0)
			{
				OnDeath.Invoke();
				Debug.Log("<color=red>Player died!</color>");
				GameManager.gm.LoseGame();
			}
			else
			{
				if (GameManager.player.movement.isHit)
					DialogueTrigger.TriggerPlayerObstacleDamamagedDialogue();
				else
					DialogueTrigger.TriggerPlayerDamagedDialogue();
				CameraShaker.instance.Shake(cameraShakeOnHit);
				OnHit.Invoke();
			}
			
		}

	}

	public void HealPlayer(float amount)
	{
		playerHealth = (playerHealth + amount >= 100) ? 100 : playerHealth + amount;

		OnHealthIncreased.Invoke();
	}

	public void IncreaseOverload(float amount)
	{
		overloadCharge = (overloadCharge + amount >= 100) ? 100 : overloadCharge + amount;

		OnOverloadChargeIncreased.Invoke();
	}

	public void SetOverload(float amount)
	{
		overloadCharge = amount;
	}

	public float GetPlayerHealth()
	{
		return playerHealth;
	}

	public float GetOverloadCharge()
	{
		return overloadCharge;
	}

	public void TogglePlayerOverloaded(bool state)
	{
		isOverloaded = state;
	}

	public bool IsPlayerOverloaded()
	{
		return isOverloaded;
	}

	public void SetWeapon(GameObject newWeapon)
	{
		// Find and activate/deactivate necessary weapons
		foreach (GameObject weapon in weapons)
		{
			if (weapon.GetComponent<WeaponBase>().weaponID == newWeapon.GetComponent<WeaponBase>().weaponID)
			{
				weapon.SetActive(true);
				currentWeapon = newWeapon;
			}
			else
			{
				// Stop overload if currently active
				weapon.GetComponent<WeaponBase>().DeactivateOverload();
				TogglePlayerOverloaded(false);

				// Deactivate GameObject
				weapon.SetActive(false);
			}
		}

		if (firstWeaponObtained) // Won't play sound at start of scene
			OnWeaponChanged.Invoke();
		else
			firstWeaponObtained = true;
	}

    public void ToggleDodging(bool dodge)
    {
        isDodging = dodge;
    }

    public void Toggleinvincibility(bool inv)
    {
        isInvincible = inv;
    }

	public GameObject GetCurrentWeapon()
	{
		return currentWeapon;
	}

	public string GetCurrentWeaponID()
	{
		return currentWeapon.GetComponent<WeaponBase>().weaponID;
	}
}
