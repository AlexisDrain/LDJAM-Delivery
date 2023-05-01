using UnityEngine;

public static class Vector3Func {

	/*
	 * 
	 * quicker to type than mathf.Round
	 * 
	 */

	public static Vector3 Round(this Vector3 vector3) {

		return new Vector3(Mathf.Round(vector3.x), Mathf.Round(vector3.y), Mathf.Round(vector3.z));
	}
	public static Vector3 Clamp(this Vector3 vector3, float min, float max) {

		return new Vector3(Mathf.Clamp(vector3.x, min, max), Mathf.Clamp(vector3.y, min, max), Mathf.Clamp(vector3.z, min, max));
	}
}
