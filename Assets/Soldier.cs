using UnityEngine;
using System.Collections;

public class SoldierA : MonoBehaviour
{
	Animator Animator;

	void Start()
	{
		Animator = GetComponent<Animator>();

		for (int i = 1; i < Animator.layerCount; ++i)
			Animator.SetLayerWeight(i, 1);
	}
	
	void Update()
	{
	}
}
