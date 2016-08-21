using UnityEngine;
using System.Collections;

public class LaserScript : MonoBehaviour
{
    public float LaserHitForceMultiplier = 5.0f;
    public float LaserVelocity = 800.0f;
    public float LaserLength = 100.0f;
    public float LaserMaxDistance = 400.0f;

    private float TimeLastFired = 0.0f;
    public float BulletsPerSeconds = 6.0f;

	// Use this for initialization
	void Start ()
	{
        

	   Screen.lockCursor = true;
	}
	
	// Update is called once per frame
	void Update ()
	{

	    float TimeToNextFire = 1  / BulletsPerSeconds; 



	    if (Input.GetButtonDown("Fire1") && TimeLastFired + TimeToNextFire <= Time.realtimeSinceStartup)
	    {
	        
	        StartCoroutine(FireLaser());
	        TimeLastFired = Time.realtimeSinceStartup;
	    }
	}

    IEnumerator FireLaser()
    {
       


        var bullet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        var bulletrb = bullet.AddComponent<Rigidbody>();
        //var bulletcollider = bullet.AddComponent<Collider>(); //already exists with primitive.sphere
        

        var line = bullet.AddComponent<LineRenderer>();
            line.enabled = true;

        bullet.GetComponent<MeshRenderer>().enabled = false;
        //bullet.GetComponent<BoxCollider>().enabled = false;

        bullet.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        bullet.transform.forward = new Vector3(transform.forward.x, transform.forward.y, transform.forward.z);
        bulletrb.useGravity = false;
        bullet.tag = "Bullet";

        while(Vector3.Distance(bullet.transform.position, transform.position) <= LaserMaxDistance )
        {
            

            Ray ray = new Ray(bullet.transform.position, bullet.transform.forward);
            RaycastHit hit;
            line.SetPosition(0, ray.origin);
            
            if (Physics.Raycast(ray, out hit, LaserLength) )
            {
                line.SetPosition(1, hit.point);

                if (hit.rigidbody)
                {
                    hit.rigidbody.AddForceAtPosition(bullet.transform.forward * LaserHitForceMultiplier, hit.point);
                }
            }
            else
            {
                line.SetPosition(1, ray.GetPoint(LaserLength));
            }
 
            //move bullet towards the raycasted point
            bullet.transform.position = Vector3.MoveTowards(bullet.transform.position, ray.GetPoint(LaserLength), LaserVelocity * Time.deltaTime);

            yield return null;
        }

        DestroyObject(bullet);
        line.enabled = false;
    }
}
