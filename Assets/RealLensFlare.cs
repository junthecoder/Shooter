using UnityEngine;
using System.Collections;

public class RealLensFlare : MonoBehaviour
{
	//I actually don't know why this works, but it does! Some kind of glitch I presume.
	//For some reason, when the flare is visible to the camera, and you then change it's layer to one that's on its ignore list,
	//the flare will then permanently stay on. (It doesn't work if you do this before the flare is visible; it will just never appear.)
	//By then changing its brightness to zero when it's outside of the camera's frustrum or when it's occluded by a collider, 
	//it acts like a more realistic flare.
	//
	//Note that the first time you look at the flare, it will fade in it's usual unrealistic manner, but will change subsequently.

	//Coordinates for the right and left hand sides of the screen respectively 
	//Change to "1" and "0" if you want the flare to disappear as soon as its invisible to the camera's frustrum.

	private float coord1 = 1.2f;
	private float coord2 = -0.2f;

	//The strength of the flare relative to it's distance from the camera ("brightness = strength/distance")
	private float strength = 200000;

	//Simple counter to ensure that the flare is visible for a few frames before the layer is changed
	private int count = 0;

	void Start()
	{
		//Ensures that the flare's layer isn't part of its ingore list to begin with
		//Change to whatever you want, as long as it's not on the ignore list
		gameObject.layer = 0;
	}

	void Update()
	{
		var heading = gameObject.transform.position - Camera.main.transform.position;
		var heading2 = Camera.main.transform.position - gameObject.transform.position;
		var dist = Vector3.Dot(heading, Camera.main.transform.forward);
		var viewPos = Camera.main.WorldToViewportPoint(gameObject.transform.position);

		//Turns off the flare when it goes outside of the camera's frustrum
		if (viewPos.x > coord1 || viewPos.x < coord2 || viewPos.y < coord2 || viewPos.y > coord1)
			gameObject.GetComponent<LensFlare>().brightness = 0;
		//Turns off the flare when it's occluded by a collider.
		else if (Physics.Raycast(gameObject.transform.position, heading2.normalized, Mathf.Clamp(dist, 0.01f, 20f)))
			gameObject.GetComponent<LensFlare>().brightness = 0;
		else
		{
			//Sets the flares brightness to be an inverse function of distance from the camera
			gameObject.GetComponent<LensFlare>().brightness = strength / dist;

			if (count < 50)
				count = count + 1;
		}

		//Changes the layer of the flare to be "Transparent FX"
		//Change it to whatever you want as long as you include that layer in your ignore list of the flare 
		if (count > 20)
		{
			gameObject.layer = 1;
		}
	}
}
