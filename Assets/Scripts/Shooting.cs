using UnityEngine;
using System;
using System.Collections;

public class Shooting : MonoBehaviour {

	public enum Weapons { gun, rocket, secondRocket }
	
	public Weapons weaponSelect;

	public float aimingTime = 5f;
	public float rayDistance;
	public Transform aimSprite;
	public LayerMask aimMask;
	//public GameObject aimPlane;
	public GunSetting gun;   // = new GunSetting();???
	public GunSetting rocket;
	public GunSetting secondRocket;

	private float shootInterval = 0.1f;
	private float shootinterTimer = 0f;
	//public float aimRocketTimer;
	private string thisName;
	private Transform bsp;
	private Transform thisTrans;


	void Start () 
		{
		aimSprite.parent = null;
		aimSprite.gameObject.SetActive(false);
		thisTrans = transform;
		thisName = gameObject.name;
		}
	
	IEnumerator Reload(GunSetting gs)
		{
		yield return new WaitForSeconds(3.0f);
		gs.clipcounter = 0;
		gs.startReload = false;
		//сообщение о перезарядке
		}

	void Update () 
		{

		if(Input.GetKeyDown("up"))
			{
			shootInterval = 0.1f;
			weaponSelect = weaponSelect + 1;
			if((int)weaponSelect == 1 && rocket.shell == null)
				weaponSelect = weaponSelect + 1;
			if((int)weaponSelect == 2 && secondRocket.shell == null)
				weaponSelect = weaponSelect + 1;
			if((int)weaponSelect > Enum.GetNames(typeof(Weapons)).Length - 1)
				weaponSelect = 0;
			}
		if(Input.GetKeyDown("down"))
			{
			shootInterval = 0.1f;
			weaponSelect = weaponSelect - 1;
			if((int)weaponSelect < 0)
				weaponSelect = (Weapons)Enum.GetNames(typeof(Weapons)).Length - 1;
			if((int)weaponSelect == 2 && secondRocket.shell == null)
				weaponSelect = weaponSelect - 1;
			if((int)weaponSelect == 1 && rocket.shell == null)
				weaponSelect = weaponSelect - 1;
			}

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
					case Weapons.rocket : ShootRealisation(rocket); 
						break;
					case Weapons.secondRocket : ShootRealisation(secondRocket); // нужно реализовать if target != 0, то стреляем,
						break;                                                  // т.е. только тогда, когда есть цель. Поведение aim
					}                                                           // ракеты после исчезновения цели делать в Rocket.cs.
				}
			}
		if(aimingTransform)
			{
			aimingTime -= Time.deltaTime;
			float distance = Vector3.Distance(thisTrans.position, aimingTransform.position);
			if(distance < 100f && aimingTime > 0)
				{
				//aimSprite.position = new Vector3(aimingTransform.position.x, 21, aimingTransform.position.z);
				aimSprite.position = Vector3.Lerp(aimSprite.position, new Vector3(aimingTransform.position.x, 21f, aimingTransform.position.z), 70*Time.deltaTime);
				}
			else
				{
				aimingTransform = null;
				aimingTime = 5f;
				}
			}
		else 
			aimSprite.gameObject.SetActive(false);
		
		}

    RaycastHit hit;
	public Transform aimingTransform;

	void FixedUpdate()
		{
		if(rocket.aiming && weaponSelect == Weapons.rocket || secondRocket.aiming && weaponSelect == Weapons.secondRocket)
			{
			if(aimingTransform == null)
				{
				if(Physics.Raycast(thisTrans.position, thisTrans.forward, out hit, rayDistance, aimMask))
					{
					Debug.DrawLine(thisTrans.position, hit.transform.position, Color.blue);
					Debug.Log("Contact");
					aimingTransform = hit.transform;
					aimSprite.gameObject.SetActive(true);
					}
				}
			}		
		}

	void ShootRealisation(GunSetting gs)
		{
	
		if(gs.clipcounter >= gs.clip && gs.canReload || gs.aiming && aimingTransform == null) 
			{
			return;
			}
		
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

		/// --- SHOT ---
		Transform bul = (Transform)Instantiate(gs.shell, bsp.position, bsp.rotation);
		bul.name = thisName; 
		BulletsParent bp = bul.gameObject.GetComponent<BulletsParent>();
		bp.Shellsetting(gs.shellSpeed, gs.shellDamage);
		if(bp.isAiming())
			bp.GetTarget(aimingTransform.gameObject);
		gs.clipcounter++;
		shootinterTimer = 0f;


		if(!gs.startReload && gs.canReload && gs.clipcounter >= gs.clip)
			{
			StartCoroutine(Reload(gs));
			gs.startReload = true;	
			}
	
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
	public bool canReload;
	public int clip;
	public bool aiming;
	public Transform firstBsp;
	public Transform secondBsp;
	[Tooltip("false - only first bsp")]
	public bool twoBsp;
	public AudioClip shotSound;
	[HideInInspector]
	public int clipcounter;
	[HideInInspector]
	public bool startReload = false;
	}