using UnityEngine;
using System;
using System.Collections;

	public class Enemy : DamageParent {

	public float hp = 100f;
	public float speed;
	public float rotSpeed;
	public float tilt;

	private MainBehaviour mb;
	private Transform thisTr;
	private bool stabiling;
	private Transform planeTrans;


	void Start () {
		thisTr = transform;
		mb = Camera.main.GetComponent<MainBehaviour>();
		planeTrans = thisTr.Find("Plane");
		}


	void Update () 
		{
		thisTr.Translate(Vector3.forward*speed*Time.deltaTime);
	
		if(stabiling)
			{
			if(planeTrans.localRotation !=  Quaternion.Euler(0, 180, 0))
				{
				planeTrans.localRotation = Quaternion.Lerp(planeTrans.localRotation, Quaternion.Euler(0, 180, 0), tilt/20*Time.deltaTime);
				}
			}

		/*if()
			{
			stabiling = true;
			}*/
		}

	void turnRight()
		{
		stabiling = false;
		thisTr.Rotate(Vector3.up, rotSpeed*Time.deltaTime);
		if (planeTrans.rotation.eulerAngles.z < 30 || planeTrans.rotation.eulerAngles.z > 327) 
			{
			planeTrans.Rotate(0, 0, tilt*Time.deltaTime);
			}
		}

	void TurnLeft()
		{
		stabiling = false;
		thisTr.Rotate(Vector3.up, -rotSpeed*Time.deltaTime);
		if (planeTrans.rotation.eulerAngles.z > 330 || planeTrans.rotation.eulerAngles.z < 33) 
			{
			planeTrans.Rotate(0, 0, -tilt*Time.deltaTime);
			}
		}
	

	public override void SetDamage(float damage, string name)
		{
		hp -= damage;
		if(hp > 0) 
			{

			}
		else
			{
			mb.Scores(name);
			mb.StartCor();
			Destroy(gameObject);
			}
		}

	}
