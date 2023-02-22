using UnityEngine;
using System.Collections;

public class GrenadeSet : Equipment
{
	public Rigidbody grenade;
	private float force = 300;
	private GrenadeSetData data;

	public void Initialize(GameObject owner, bool canControl)
	{
		base.Initialize(owner, canControl, 4, 1);
	}

	void Start()
	{
		data = GetComponent<GrenadeSetData>();
	}
	
	void Update()
	{
		if (Time.timeScale == 0) return;
		if (!CanControl) return;

		if (Input.GetButtonUp("Grenade"))
		{
			if (NumLoaded >= 1)
			{
				var grenade = Instantiate(data.grenadePrefab, Camera.main.transform.position, Camera.main.transform.rotation) as Grenade;
				grenade.rigidbody.AddForce(Camera.main.transform.forward * force);
			}
		}
	}
}
