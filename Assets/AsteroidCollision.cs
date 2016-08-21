using UnityEngine;
using System.Collections;

public class AsteroidCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnTriggerEnter(Collider otherCol)
    {


        if (otherCol.gameObject.tag == "Player")
        {
            /// TODO damage the player or sail

        }
        else if (otherCol.gameObject.tag == "Bullet")
        {
            Debug.Log("Asteroid hit");
            DestroyObject(this);
        }
    }
}
