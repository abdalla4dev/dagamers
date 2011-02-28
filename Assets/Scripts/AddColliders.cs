using UnityEngine;
using System.Collections;

public class AddColliders : MonoBehaviour {

	// Use this for initialization
	void Start () {
		foreach (Transform child in transform)
		{
			child.gameObject.AddComponent("MeshCollider");
		}
	}
}
