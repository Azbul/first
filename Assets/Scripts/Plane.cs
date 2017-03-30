using UnityEngine;
using System;
using System.Collections;

	public class Plane: DamageParent {

	public float hp = 100f;
	public float speed;
	public float rotSpeed;
	public float tilt;
	//public Animation planeAnim;
	[SerializeField]
	private float camfollowSmooth = 5f;
	[Range(0.0f, 5.0f)]
	public float camrotSmooth;
	public float camHeight;

	private Transform planeTrans; 
	private Transform camtr;
	protected Transform thisTr;
	private bool stabiling;
	protected MainBehaviour bahaha;

	//ВРЕМЕННЫЕ ПЕРЕМЕННЫЕ
	public float viewRadius;
	public float viewAngle;
	public LayerMask viewRadiusMask;

	void Start() 
		{
		camtr = Camera.main.GetComponent<Transform>();
		bahaha = camtr.GetComponent<MainBehaviour>();
		thisTr = transform;
		planeTrans = thisTr.Find("Plane");
		}


	void Update () {
	
		thisTr.Translate(Vector3.forward*speed*Time.deltaTime);
		camtr.position = Vector3.Lerp(camtr.position, new Vector3(thisTr.position.x, thisTr.position.y + camHeight, thisTr.position.z), camfollowSmooth*Time.deltaTime);
		//camtr.eulerAngles = Vector3.Lerp(camtr.eulerAngles, new Vector3(90, thisTr.eulerAngles.y, 0), 1*Time.deltaTime); //relaise camera rotation with Vector3
		camtr.rotation = Quaternion.Lerp(camtr.rotation, Quaternion.Euler(90, thisTr.rotation.eulerAngles.y, 0), camrotSmooth*Time.deltaTime);

		////////////////////////////////
		if(Input.GetKey(KeyCode.RightArrow))
			{
			stabiling = false;
			thisTr.Rotate(Vector3.up, rotSpeed*Time.deltaTime);
			if (planeTrans.rotation.eulerAngles.z < 30 || planeTrans.rotation.eulerAngles.z > 327) 
				{
				planeTrans.Rotate(0, 0, tilt*Time.deltaTime);
				}
			}

		if(Input.GetKey(KeyCode.LeftArrow))
			{
			stabiling = false;
			thisTr.Rotate(Vector3.up, -rotSpeed*Time.deltaTime);
			if (planeTrans.rotation.eulerAngles.z > 330 || planeTrans.rotation.eulerAngles.z < 33) 
				{
				planeTrans.Rotate(0, 0, -tilt*Time.deltaTime);
				}
			}	

		if(Input.GetKeyUp(KeyCode.LeftArrow)||Input.GetKeyUp(KeyCode.RightArrow)) 
			{
			stabiling = true;
			}

		if(stabiling)
			{
			if(planeTrans.localRotation !=  Quaternion.Euler(0, 180, 0))
				{
				planeTrans.localRotation = Quaternion.Lerp(planeTrans.localRotation, Quaternion.Euler(0, 180, 0), tilt/20*Time.deltaTime);
				}
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
			bahaha.Scores(name);
			bahaha.StartCor();
			Destroy(gameObject);
			}
		}

	void OnDrawGizmos()
		{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, viewRadius);
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

	}
