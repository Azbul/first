using System;
using UnityEngine;
using System.Collections;

public class BulletsParent : MonoBehaviour 
	{

	public virtual bool isAiming() 
		{
		return false;
		} 
		
	public virtual void GetTarget(GameObject target) {}

	public virtual void Shellsetting(float sp, float dm)
		{

		}
	}


