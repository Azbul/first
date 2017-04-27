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
	public Transform attackEnemy;

	private int direct;
	private float setAngle;
	private float timer;
	private float setRotation;
	private bool stabiling;
	private bool scoreSend;
	private bool canRot;
	private bool mapOutbool;
	private bool findEnemyCorBool;
	private MainBehaviour mb;
	private Transform thisTr;
	private Transform planeTrans;



	void Start () {
		timer = 0;
		thisTr = transform;
		setRotation = 0;
		mb = Camera.main.GetComponent<MainBehaviour>();
		planeTrans = thisTr.Find("Plane");
		scoreSend = true;
		StartCoroutine("FindEnemyCor", 0.2f);

		}


	void Update () 
		{
		/////// AI ROTATION //////////
		if (timer < 5f && !mapOutbool)   //если атакуют, то 1f; свободный полет - 5f;
			{
			timer += Time.deltaTime;
			}
		else if (!mapOutbool && attackEnemy == null)
			{
			canRot = true;
			timer = 0;
			direct = UnityEngine.Random.Range(0, 2);
			setAngle = UnityEngine.Random.Range(60, 180);
			}

		if (mapOutbool)
			Rotat(180, 1);
		else if (canRot)
			Rotat(setAngle, direct);
		/////// END AI ROTATION //////////
		thisTr.Translate(Vector3.forward*speed*Time.deltaTime);
		if(stabiling)
			{
			if(planeTrans.localRotation !=  Quaternion.Euler(0, 180, 0))
				{
				planeTrans.localRotation = Quaternion.Lerp(planeTrans.localRotation, Quaternion.Euler(0, 180, 0), tilt/20*Time.deltaTime);
				}
			}

		if (attackEnemy != null && !mapOutbool)
			{
			Debug.DrawLine(thisTr.position, attackEnemy.position, Color.cyan);
			float dist = Vector3.Distance(attackEnemy.position, thisTr.position);
			Vector3 dirToTarget = (attackEnemy.position - thisTr.position).normalized;
			if (dist < viewRadius)
				{
				float angle = GetAngle(thisTr.forward, dirToTarget);
				float dot = Vector3.Dot(thisTr.right, dirToTarget);
				//if (dist < 5)
					//Rotat(60, direct);
				//else
					//{	
				if (timer > 2f)
					{
					Rotat(10, direct);
					timer = timer>3f ? 0 : timer;
					}
				else if (dot != 0 && angle > 5) 
					{                  
					direct = UnityEngine.Random.Range(0, 2);
					if (dot > 0)
						turnRight();  
					if (dot < 0)
						TurnLeft();
					}
				else 
					stabiling = true;
					//}
				if (angle < 5)
					Debug.Log("Attack!!");
				}
			else 
				{
				stabiling = true;
				attackEnemy = null;
				findEnemyCorBool = false;
				}
			}
		else if (attackEnemy == null)
			{
			if (!findEnemyCorBool) //если не запущен
				{
				Debug.Log("Started");
				StartCoroutine("FindEnemyCor", 0.2f);
				findEnemyCorBool = true;
				}
			}

		
		}
	
	IEnumerator FindEnemyCor(float time)
		{
		while(true)
			{
			FindEnemy();
			yield return new WaitForSeconds(time);
			}
		}

	void FindEnemy()
		{
		Collider[] EnemyInRadius = Physics.OverlapSphere(thisTr.position, viewRadius, viewRadiusMask);
		for (int i = 0; i < EnemyInRadius.Length; i++)
			{
			Transform target = EnemyInRadius[i].transform;
			if (target.name != thisTr.name)
				{
				Vector3 dirToTarget = (target.position - thisTr.position).normalized;
				//Debug.Log("Angle =" + GetAngle(thisTr.forward, dirToTarget));
				//Debug.Log("Dot = " + Vector3.Dot(thisTr.right, dirToTarget));
				if (GetAngle(thisTr.forward, dirToTarget) <= viewAngle / 2)
					{
					attackEnemy = target;
					StopCoroutine("FindEnemyCor");
					findEnemyCorBool = false;
					}
				}
			}
		}

	float GetAngle(Vector3 v1, Vector3 v2)
		{
		return Vector3.Angle(v1, v2);
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
