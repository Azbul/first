using UnityEngine;
using System;
using System.Collections;

	public class Enemy : DamageParent {

	public float hp = 100f;
	public float speed;
	public float rotSpeed;
	public float tilt;
	public float viewAngle;
	public float viewRadius;
	public LayerMask viewRadiusMask;

	private int direct;
	private float setAngle;
	private float timer;
	private float setRotation;
	private MainBehaviour mb;
	private Transform thisTr;
	private Transform planeTrans;
	private bool stabiling;
	private bool scoreSend;
	private bool canRot;
	private bool mapOutbool;


	void Start () {
		timer = 0;
		thisTr = transform;
		setRotation = 0;
		mb = Camera.main.GetComponent<MainBehaviour>();
		planeTrans = thisTr.Find("Plane");
		scoreSend = true;
		}


	void Update () 
		{
		if (timer < 3f && !mapOutbool)
			{
			timer += Time.deltaTime;
			}
		else if (!mapOutbool)
			{
			canRot = true;
			timer = 0;
			direct = UnityEngine.Random.Range(0, 2);
			setAngle = UnityEngine.Random.Range(10, 360);
			}

		if (mapOutbool)
			Rotat(180, 1);
		else if (canRot)
			Rotat(setAngle, direct);

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
	
	void FindEnemy()
		{
		Collider[] EnemyInRadius = Physics.OverlapSphere(thisTr.position, viewRadius, viewRadiusMask);
		for (int i = 0; i < EnemyInRadius.Length; i++)
			{
			Transform target = EnemyInRadius[i].transform;
			Vector3 dirToTarget = (target.position - thisTr.position).normalized;
			if (Vector3.Angle(thisTr.forward, dirToTarget) <= viewAngle / 2)
				{

				}
			}
		}

	void Rotat(float angle, int direct) 
		{
		if (setRotation < angle)
			{
			setRotation += rotSpeed*Time.deltaTime;
			if (direct == 1)
				turnRight();
			else
				TurnLeft();
			}
		else
			{
			stabiling = true;
			canRot = false;
			mapOutbool = false;
			setRotation = 0;
			}
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

	public override void mapOut()
		{
		mapOutbool = true;
		setRotation = 0;
		}

	public override void SetDamage(float damage, string name)
		{
		hp -= damage;
		if(hp > 0) 
			{

			}
		else if(scoreSend)
			{
			scoreSend = false;
			mb.Scores(name);
			mb.StartCor();
			Destroy(gameObject);
			}
		}

	void OnDrawGizmos()
		{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, viewRadius);
		}

	}
