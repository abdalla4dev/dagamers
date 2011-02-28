using UnityEngine;
using System.Collections;

public class AddColliders : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//if(transform.childCount>0)
		addColliders();
	}
	
	void addColliders() {
		if(transform.childCount==0)
			gameObject.AddComponent("MeshCollider");
		foreach (Transform child in transform)
		{
			child.gameObject.AddComponent("MeshCollider");
			if(child.childCount>0) 
				foreach( Transform child2 in child.transform)
				{
					child2.gameObject.AddComponent("MeshCollider");
				}
		}
	}
}
