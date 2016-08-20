using UnityEngine;
using System.Collections;

public class BezierSplineWalker : MonoBehaviour {

    public BezierSpline spline;

    public float duration;

    public float progress;

	// Update is called once per frame
	void Update () {

        progress += Time.deltaTime / duration;

        if (progress > 1f)
        {
            progress -= 1f;
        }
        Vector3 position = spline.GetPoint(progress);
        transform.localPosition = position;
        transform.LookAt(position + spline.GetDirection(progress));

        transform.RotateAround(position,transform.forward, 1);
	}
}
