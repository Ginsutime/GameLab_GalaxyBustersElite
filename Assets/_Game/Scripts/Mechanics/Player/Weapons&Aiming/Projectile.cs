﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class for modifying projectiles and how they behave
public class Projectile : MonoBehaviour
{
	public float speed = 20f;
	public float damage = 2;
	public float lifeTime = 2f;
	private Rigidbody rb;

	private void Start()
	{
		// Move projectile forwards with set speed
		rb = GetComponent<Rigidbody>();
		rb.velocity = transform.forward * speed;
	}

	private void Awake()
	{
		// Destroy projectile after lifetime expires
		Destroy(gameObject, lifeTime);
	}

}
