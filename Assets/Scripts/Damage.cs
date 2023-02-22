using UnityEngine;
using System.Collections;

public struct Damage {
	public GameObject damager;
	public string ownerTag;
	public float damage;
	public Vector3 point, direction;

	public Damage(GameObject damager, string ownerTag, float damage)
	{
		this.damager = damager;
		this.ownerTag = ownerTag;
		this.damage = damage;
		this.point = Vector3.zero;
		this.direction = Vector3.zero;
	}
}
