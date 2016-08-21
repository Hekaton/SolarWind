using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleSpeed : MonoBehaviour {

    public ParticleSystem pSystem;

    public Rigidbody shipVelocity;
    

	// Use this for initialization
	void Start () {
        pSystem = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {

        if (shipVelocity.velocity == Vector3.zero && !pSystem.isPaused)
        {
            pSystem.Pause();
        }
        else if(shipVelocity.velocity != Vector3.zero  && pSystem.isPaused)
        {
            pSystem.Play();
        }

	}
}
