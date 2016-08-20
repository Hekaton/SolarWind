using UnityEngine;
using System.Collections;

public class FollowObject : MonoBehaviour {

	public GameObject target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (target != null) {
			transform.position = target.transform.position;
		}
	}
}
