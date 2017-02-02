using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour {

	[Tooltip("false - only first bsp")]
	public bool secondBsp;
	public Transform bulletSpawnPoint;
	public Transform bulletSpawnPointTwo;
	public Transform bullet;
	public float shootInterval;

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
				if(secondBsp)
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


		if(Input.GetKeyUp(KeyCode.Space))
			{
			shootinterTimer = 0;
			}
		}



	void Shoot(Transform bullet, Transform bulletSpawnPoint)
		{
		Transform bul = (Transform)Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
		bul.name = thisName; 
		}
}
