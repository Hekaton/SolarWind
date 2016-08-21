using UnityEngine;
using System.Collections;

public class MenuSelector : MonoBehaviour {

	public GameObject[] SelectedList;
	public GameObject[] UnselectedList;
	public int[] StageList;

	public int index = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis ("Vertical") > 0) {
			Debug.Log ("down");
			index = Mathf.Min (SelectedList.Length-1, UnselectedList.Length-1, index + 1);
		} else if (Input.GetAxis ("Vertical") < 0) {
			Debug.Log ("up");
			index = Mathf.Max (0, index - 1);
		}

		for (int i = 0, ii = UnselectedList.Length; i < ii; i++) {
			SelectedList [i].SetActive (i==index);
			UnselectedList [i].SetActive (i!=index);
		}

		if (Input.GetButton ("Boost") || Input.GetButton ("P2_boost") || Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.Space)) {
            Debug.Log("Loading " + StageList[index]);
            Application.LoadLevel(StageList[index]);

		}
	}
}
