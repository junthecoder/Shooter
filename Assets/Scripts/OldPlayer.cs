using UnityEngine;
using System.Collections;

public class OldPlayer : PlayerBase
{
	public GunData[] defaultGunDatas;
	public Optics[] defaultSights;
	public Attachment[] defaultAccessories; // accessory
	public static GunData[] gunDatas;
	public GameObject hitBlood;
	public AudioClip hurtSound;

	void Start()
	{
		Screen.lockCursor = true;
		Health = 100;

		//if (gunDatas == null)
			gunDatas = defaultGunDatas;

		equipments = new Gun[gunDatas.Length];

		for (int i = 0; i < gunDatas.Length; ++i)
		{
			gunDatas[i].opticsPrefab = defaultSights[i];
			gunDatas[i].accessoryPrefab = defaultAccessories[i];

			var gunObject = gunDatas[i].Instantiate();
			gunObject.SetActive(false);
			gunObject.transform.parent = Camera.main.transform;

			var gun = gunObject.AddComponent<Gun>();
			gun.Initialize(gameObject, true);
			equipments[i] = gun;
		}

		SwitchEquipment(0);
	}

	void Update()
	{
		for (int i = 0; i < equipments.Length; ++i)
			if (Input.GetKeyDown("" + (i + 1)))
				SwitchEquipment(i);
	}

	void Hit(Damage damage)
	{
		if (damage.ownerTag == "Player")
			return;

		audio.PlayOneShot(hurtSound);

		Health -= damage.damage;

		// TODO: �O���l�[�h�̃q�b�g����blood�̌���������
		var blood = (Instantiate(hitBlood) as GameObject).GetComponent<HitBlood>();
		blood.bulletAngle = damage.damager.transform.eulerAngles.y;
		blood.playerTransform = transform;
		
		// TODO: ���񂾎��̏���
		if (Health <= 0)
		{ }
	}
}
