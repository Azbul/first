using UnityEngine;
using System.Collections;

public class Rocket : BulletsParent 
	{

	[Header("Aim Rocket")]
	public bool aiming;
	public GameObject target;
	public float rotatingSpeed;
	[Space(20)]
	public float speed;
	public float damage;
	public float Ballistic;

	private float ballistic;
	private float timeout;
	private Transform tr;
	private Rigidbody rb;

	void Start () {
		tr = transform;
		rb = GetComponent<Rigidbody>();
		}


	void Update () {

		timeout += Time.deltaTime;
		if(timeout>0.1f)
			{
			ballistic = Random.Range(-Ballistic, Ballistic);
			timeout = 0;
			}

		if(!aiming)
			{
		tr.Translate(Vector3.forward*speed*Time.deltaTime);
			}
		rb.AddForce(tr.right*ballistic, ForceMode.Impulse);
		//transform.RotateAround(tr.position, tr.TransformDirection(Vector3.up), Scatter*Time.deltaTime);
		}

	public override bool isAiming()
		{
		if(aiming)
			return true;
		Debug.Log("Returned false");
		return false;
		}

	public override void GetTarget(GameObject target)
		{
		this.target = target;
		}

	public override void Shellsetting(float sp, float dm)
	{
		damage = dm;
		speed = sp;
	}

	void OnTriggerEnter(Collider col)
		{
		if (col.gameObject.name != tr.name)
			{
			DamageParent planeScript = col.gameObject.GetComponent<DamageParent>();
			if(planeScript)
				{
				string name = tr.name;
				planeScript.SetDamage(damage, name);
				}
				Destroy(gameObject);
			}
		}

	void FixedUpdate () 
		{

		rb.AddForce(tr.right*ballistic, ForceMode.Impulse); //если тормозит, сделать через интервал
		if(aiming)	
			{
			if(target == null)
				{
				aiming = false;
				return;
				}
		Vector3 point2Target = tr.position - target.transform.position;

		point2Target.Normalize ();

		float value = Vector3.Cross (point2Target, transform.forward).y;

		/*
				if (value > 0) {
				
						rb.angularVelocity = rotatingSpeed;
				} else if (value < 0)
						rb.angularVelocity = -rotatingSpeed;
				else
						rotatingSpeed = 0;
*/

		rb.angularVelocity = tr.up * rotatingSpeed * value;
		rb.velocity = transform.forward * speed;
			}

		}
	}
