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
        Debug.Log("Trigger Enter");
        if (col.tag == "ShipCore")
        {
            col.GetComponentInParent<MultiplayerShipController>().ExitTunnel();
        }
    }
    

}
