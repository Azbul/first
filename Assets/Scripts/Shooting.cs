﻿using UnityEngine;
using System;
using System.Collections;

public class Shooting : MonoBehaviour {

	public enum Weapons { gun, hardGun, rocket, secondRocket }
	
	public Weapons weaponSelect;
	[Tooltip("false - only first bsp")]
	public bool secondBsp;
	public Transform bulletSpawnPoint;
	public Transform bulletSpawnPointTwo;
	public Transform bullet;
	public float shootInterval;

	public GunSetting gun;

	private string thisName;
	private float shootinterTimer = 0f;
	private Transform bsp;

		
		void Start () {
		thisName = gameObject.name;
	
		}
	
		void Update () {
	
		if(shootinterTimer < shootInterval)
			{
			shootinterTimer += Time.deltaTime;
			}
		
		if(shootinterTimer > shootInterval) 
			{
			if(Input.GetKey(KeyCode.Space))
				{
				// --- 2 BSP REALISATION ---
				if(gun.twoBsp)
					{
					if(bsp == bulletSpawnPointTwo)
						{
						bsp = null;
						}
					if(bsp == null) bsp = bulletSpawnPoint;
					else bsp = bulletSpawnPointTwo;
					}
				else 
					{ bsp = bulletSpawnPoint; }
		       // ---  END 2 BSP R-N  ---

				Shoot(bullet, bsp);
				shootinterTimer = 0f;


//        ---   THIS IS SECOND REALISATION OF SHOOTING   ---
				/* if(shootinterTimer == 0f)
					{
					if(bsp == null) bsp = bulletSpawnPoint;
					else bsp = bulletSpawnPointTwo;

					Shoot(bullet, bsp);
					}

				shootinterTimer += Time.deltaTime;

				if(shootInterval < shootinterTimer) 
					{
					if(bsp == bulletSpawnPointTwo)
						{
						bsp = null;
						}
					shootinterTimer = 0f;
					}*/

				}
			}
		}



	void Shoot(Transform bullet, Transform bulletSpawnPoint)
		{
		Transform bul = (Transform)Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
		bul.name = thisName; 
		}
}

[Serializable]
public class GunSetting
	{
	public Transform shell;
	public float interval;
	public Transform firstBsp;
	public Transform secondBsp;
	public bool twoBsp;
	}