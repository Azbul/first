using UnityEngine;
using System;
using System.Collections;

public class Shooting : MonoBehaviour {

	public enum Weapons { gun, hardGun, rocket, secondRocket }
	
	public Weapons weaponSelect;
	public GunSetting gun;
	public GunSetting hardGun;

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
					case Weapons.gun : ShootRealisation(gun.shell, gun.firstBsp, gun.secondBsp, gun.twoBsp,  gun.interval); 
						break;
					case Weapons.hardGun : ShootRealisation(hardGun.shell, hardGun.firstBsp, hardGun.secondBsp, hardGun.twoBsp,  hardGun.interval); 
						break;
					}
				
				}
			}
		}


	void ShootRealisation(Transform bullet, Transform bspone, Transform bsptwo, bool twobsp, float Interval)
		{
		// --- 2 BSP REALISATION ---
		if(twobsp)
			{
			if(bsp == bsptwo)
				{
				bsp = null;
				}
			if(bsp == null) bsp = bspone;
			else bsp = bsptwo;
			}
		else 
			{ bsp = bspone; }
		// ---  END 2 BSP R-N  ---
		shootInterval = Interval;
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
	[Tooltip("false - only first bsp")]
	public bool twoBsp;
	}