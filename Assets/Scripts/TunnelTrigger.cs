using UnityEngine;
using System.Collections;

public class TunnelTrigger : MonoBehaviour {




	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "ShipCore")
        {
            //Debug.Log("Trigger Enter");
            col.GetComponentInParent<MultiplayerShipController>().ExitTunnel();
        }
    }
    

}
