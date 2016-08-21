using UnityEngine;
using System.Collections;

public class Starter : MonoBehaviour {

    public GameObject shipPrefab;

    public GameObject splinePrefab;

    public GameObject splineEmitterPrefab;
    

	// Use this for initialization
	void Start () { 

        shipPrefab.transform.position = transform.position;

        splineEmitterPrefab.transform.position = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
