using UnityEngine;
using System.Collections;

public class MultiplayerShipController : MonoBehaviour {

    private bool isShipInTunnel;


	public float windForce = 100;

	public GameObject P1SailLeft;
	public string P1SailLeftButton = "P1_sailLeft";

	public GameObject P1SailRight;
	public string P1SailRightButton = "P1_sailRight";

	public GameObject P2SailLeft;
	public string P2SailLeftButton = "P2_sailLeft";

	public GameObject P2SailRight;
	public string P2SailRightButton = "P2_sailRight";

	public float openAngle = 0;
	public float closedAngle = 70;

	private GameObject[] sails;
	private string[] sails_buttons;
	private Vector3[] sails_initialValues;

	// Use this for initialization
	void Start () {
		sails = new GameObject[]{ P1SailLeft, P1SailRight, P2SailLeft, P2SailRight };
		sails_buttons = new string[]{ P1SailLeftButton, P1SailRightButton, P2SailRightButton, P2SailLeftButton };
		sails_initialValues = new Vector3[sails.Length];

		for (int i = 0, ii = sails.Length; i < ii; i++) {
			sails_initialValues[i] = sails[i].transform.eulerAngles;
		}

        isShipInTunnel = true;
	}                                                                
	
	// Update is called once per frame
	void Update () {

		Vector3 fwd = transform.TransformDirection (new Vector3 (-1, 0, 0));
		Vector3 u = transform.TransformDirection (new Vector3 (0, 1, 0));
		Vector3 r = transform.TransformDirection (new Vector3 (0, 0, 1));

		for (int i = 0, ii = sails.Length; i < ii; i++) {
			if (sails [i] != null) { // if sail exists
				string tweakAxis = (i<2)?"P1Horizontal":"P2Horizontal";

				float tweak = Input.GetAxis (tweakAxis);
				float intencity = 1-Input.GetAxis (sails_buttons [i]);

				sails[i].transform.localEulerAngles = sails_initialValues[i] + new Vector3(0, tweak * 30, Mathf.LerpAngle(closedAngle, openAngle, intencity));
				// adjust forces based on sail deployment (just lerp it)
				Rigidbody rb = GetComponent<Rigidbody>();
				if (rb != null) {
					Vector3 f = fwd * intencity * windForce;
					Vector3 p = transform.position + (u - r - u * Mathf.Floor (i / 2) * 2 + r * (i==1||i==2?1:0) * 2) * 10;
					rb.AddForceAtPosition (f, p);
					Debug.DrawRay (p, f*100);
				}
			}
		}

	    
	}

<<<<<<< HEAD
    public void ExitTunnel()
    {
        isShipInTunnel = false;
        Debug.Log("Exiting Tunnel");
    }

    public void EnterTunnel()
    {
        isShipInTunnel = true;
        Debug.Log("Entering Tunnel");
    }

=======
     
    public void FixedUpdate()
    {
        var spline = GameObject.FindObjectOfType<BezierSpline>();
        var distance = spline.ShortestDistanceFromPoint(transform.position);
    }
>>>>>>> 9596b5254afe6d1f30ddfd7a8aa3ba6e4ea09688
}
