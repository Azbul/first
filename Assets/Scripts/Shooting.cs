using UnityEngine;
using System;
using System.Collections;

public class Shooting : MonoBehaviour {

	public enum Weapons { gun, hardGun, rocket, secondRocket }
	
	public Weapons weaponSelect;

	public GunSetting gun;
	public GunSetting hardGun;
	public GunSetting rocket;
	public GunSetting secondRocket;


	private float shootInterval = 0.1f;
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
				switch (weaponSelect)
					{
					case Weapons.gun : ShootRealisation(gun); 
						break;
					case Weapons.hardGun : ShootRealisation(hardGun); 
						break;
					case Weapons.rocket : ShootRealisation(rocket); 
						break;
					case Weapons.secondRocket : ShootRealisation(secondRocket); // нужно реализовать if target != 0, то стреляем,
						break;                                                  // т.е. только тогда, когда есть цель. Поведение aim
					}                                                           // ракеты после исчезновения цели делать в Rocket.cs.
				
				}
			}
		}


	void ShootRealisation(GunSetting gs)
		{
		// --- 2 BSP REALISATION ---
		if(gs.twoBsp)
			{
			if(bsp == gs.secondBsp)
				{
				bsp = null;
				}
			if(bsp == null) bsp = gs.firstBsp;
			else bsp = gs.secondBsp;
			}
		else 
			{ bsp = gs.firstBsp; }
		// ---  END 2 BSP R-N  ---

		shootInterval = gs.interval;
		//Shoot(gs.shell, bsp);
		Transform bul = (Transform)Instantiate(gs.shell, bsp.position, bsp.rotation);
		bul.name = thisName; 
		BulletsParent bp = bul.gameObject.GetComponent<BulletsParent>();
		bp.Shellsetting(gs.shellSpeed, gs.shellDamage);
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


	/*void Shoot(Transform bullet, Transform bulletSpawnPoint)
		{
		Transform bul = (Transform)Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
		bul.name = thisName; 
		}*/
}


[Serializable]
public class GunSetting
	{
	public Transform shell;
	public float shellDamage;
	public float shellSpeed;
	public float interval;
	public Transform firstBsp;
	public Transform secondBsp;
	[Tooltip("false - only first bsp")]
	public bool twoBsp;
	public AudioClip shotSound;
	}