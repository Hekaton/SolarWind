using UnityEngine;
using System.Collections;

public class MultiplayerShipController : MonoBehaviour {

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
		sails_buttons = new string[]{ P1SailLeftButton, P1SailRightButton, P2SailLeftButton, P2SailRightButton };
		sails_initialValues = new Vector3[sails.Length];

		for (int i = 0, ii = sails.Length; i < ii; i++) {
			sails_initialValues[i] = sails[i].transform.eulerAngles;
		}
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0, ii = sails.Length; i < ii; i++) {
			if (sails [i] != null) { // if sail exists
				string tweakAxis = (i<2)?"P1Horizontal":"P2Horizontal";
				sails[i].transform.localEulerAngles = sails_initialValues[i] + new Vector3(0, Input.GetAxis (tweakAxis) * 30, Mathf.LerpAngle(closedAngle, openAngle, Input.GetAxis (sails_buttons [i])));
				// adjust forces based on sail deployment (just lerp it)
			}
		}
		Debug.Log ("Axes: P1L: " + Input.GetAxis (sails_buttons [0]) + "; P1R: " + Input.GetAxis (sails_buttons [1]));
	}
}
