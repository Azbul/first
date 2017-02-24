using UnityEngine;
using System.Collections;

public class HomingRocket : BulletsParent
	{

	public float damage;
	public Transform target;
	public float rocketTurnSpeed = 90.0f;
	public float speed = 10.0f;
	public float turbulence = 10.0f;
	public float push;


	private float pushtime;
	private Rigidbody rb;
	private TrailRenderer trr;
	private bool postEnd;
	private Transform tr;

	void Start()
		{
		target = GameObject.FindGameObjectWithTag("Player").transform;
		trr = GetComponent<TrailRenderer>();
		trr.enabled = false;
		rb = GetComponent<Rigidbody>();
		tr = transform;
		postEnd = false;
		rb.AddForce(tr.forward*speed, ForceMode.Impulse);
		}

	public override void Shellsetting(float sp, float dm)
	{
		damage = dm;
		speed = sp;
	}

	void OnCollisionEnter(Collision col)
		{

		Plane planeScript = col.gameObject.GetComponent<Plane>();

		if(planeScript)
			{
			string name = tr.name;
			planeScript.SetDamage(damage, name);
			}
		Destroy(gameObject);
		}

	void Update ( )
		{
		if(!postEnd)
			{
			pushtime += Time.deltaTime;
			if(pushtime>0.7f)
				{
				trr.enabled = true;
				postEnd = true;
				}
			}

		if(postEnd)
			{
			Vector3 direction = Distance( ) + Wiggle( );
			direction.Normalize( );
			tr.rotation = Quaternion.RotateTowards( tr.rotation, Quaternion.LookRotation( direction ), rocketTurnSpeed * Time.deltaTime );
			tr.Translate( Vector3.forward * speed * Time.deltaTime );
			}
		}
	private Vector3 Distance ( )
		{
		return target.position - tr.position;
		}
	private Vector3 Wiggle ( )
		{
		return new Vector3(Random.Range(-1.0f, 2.0f), 0, Random.Range(-1.0f, 2.0f)) * turbulence;
		}

	}