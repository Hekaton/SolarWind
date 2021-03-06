﻿using UnityEngine;
using System;
using System.Collections.Generic;
using ProgressBar;

public class BezierSpline : MonoBehaviour {

	[SerializeField]
	private Vector3[] points;

	[SerializeField]
	private BezierControlPointMode[] modes;

	[SerializeField]
	private bool loop;

    public float deltaX = 75f;

    public float width = 100;
    public float height = 100;

	public bool Loop {
		get {
			return loop;
		}
		set {
			loop = value;
			if (value == true) {
				modes[modes.Length - 1] = modes[0];
				SetControlPoint(0, points[0]);
			}
		}
	}

	public int ControlPointCount {
		get {
			return points.Length;
		}
	}

	public Vector3 GetControlPoint (int index) {
		return points[index];
	}

	public void SetControlPoint (int index, Vector3 point) {
		if (index % 3 == 0) {
			Vector3 delta = point - points[index];
			if (loop) {
				if (index == 0) {
					points[1] += delta;
					points[points.Length - 2] += delta;
					points[points.Length - 1] = point;
				}
				else if (index == points.Length - 1) {
					points[0] = point;
					points[1] += delta;
					points[index - 1] += delta;
				}
				else {
					points[index - 1] += delta;
					points[index + 1] += delta;
				}
			}
			else {
				if (index > 0) {
					points[index - 1] += delta;
				}
				if (index + 1 < points.Length) {
					points[index + 1] += delta;
				}
			}
		}
		points[index] = point;
		EnforceMode(index);
	}

	public BezierControlPointMode GetControlPointMode (int index) {
		return modes[(index + 1) / 3];
	}

	public void SetControlPointMode (int index, BezierControlPointMode mode) {
		int modeIndex = (index + 1) / 3;
		modes[modeIndex] = mode;
		if (loop) {
			if (modeIndex == 0) {
				modes[modes.Length - 1] = mode;
			}
			else if (modeIndex == modes.Length - 1) {
				modes[0] = mode;
			}
		}
		EnforceMode(index);
	}

	private void EnforceMode (int index) {
		int modeIndex = (index + 1) / 3;
		BezierControlPointMode mode = modes[modeIndex];
		if (mode == BezierControlPointMode.Free || !loop && (modeIndex == 0 || modeIndex == modes.Length - 1)) {
			return;
		}

		int middleIndex = modeIndex * 3;
		int fixedIndex, enforcedIndex;
		if (index <= middleIndex) {
			fixedIndex = middleIndex - 1;
			if (fixedIndex < 0) {
				fixedIndex = points.Length - 2;
			}
			enforcedIndex = middleIndex + 1;
			if (enforcedIndex >= points.Length) {
				enforcedIndex = 1;
			}
		}
		else {
			fixedIndex = middleIndex + 1;
			if (fixedIndex >= points.Length) {
				fixedIndex = 1;
			}
			enforcedIndex = middleIndex - 1;
			if (enforcedIndex < 0) {
				enforcedIndex = points.Length - 2;
			}
		}

		Vector3 middle = points[middleIndex];
		Vector3 enforcedTangent = middle - points[fixedIndex];
		if (mode == BezierControlPointMode.Aligned) {
			enforcedTangent = enforcedTangent.normalized * Vector3.Distance(middle, points[enforcedIndex]);
		}
		points[enforcedIndex] = middle + enforcedTangent;
	}

	public int CurveCount {
		get {
			return (points.Length - 1) / 3;
		}
	}

	public Vector3 GetPoint (float t) {
		int i;
		if (t >= 1f) {
			t = 1f;
			i = points.Length - 4;
		}
		else {
			t = Mathf.Clamp01(t) * CurveCount;
			i = (int)t;
			t -= i;
			i *= 3;
		}
		return transform.TransformPoint(Bezier.GetPoint(points[i], points[i + 1], points[i + 2], points[i + 3], t));
	}
	
	public Vector3 GetVelocity (float t) {
		int i;
		if (t >= 1f) {
			t = 1f;
			i = points.Length - 4;
		}
		else {
			t = Mathf.Clamp01(t) * CurveCount;
			i = (int)t;
			t -= i;
			i *= 3;
		}
		return transform.TransformPoint(Bezier.GetFirstDerivative(points[i], points[i + 1], points[i + 2], points[i + 3], t)) - transform.position;
	}
	
	public Vector3 GetDirection (float t) {
		return GetVelocity(t).normalized;
	}

	public void AddCurve () {
		Vector3 point = points[points.Length - 1];
		Array.Resize(ref points, points.Length + 3);
		point.x += deltaX;
        point.z = UnityEngine.Random.Range(-width / 2, width / 2);
        point.y = UnityEngine.Random.Range(-height / 2, height / 2);
        points[points.Length - 3] = point;
		point.x += deltaX;
        point.z = UnityEngine.Random.Range(-width / 2, width / 2);
        point.y = UnityEngine.Random.Range(-height / 2, height / 2);
        points[points.Length - 2] = point;
		point.x += deltaX;
        point.z = UnityEngine.Random.Range(-width / 2, width / 2);
        point.y = UnityEngine.Random.Range(-height / 2, height / 2);
        points[points.Length - 1] = point;

		Array.Resize(ref modes, modes.Length + 1);
		modes[modes.Length - 1] = modes[modes.Length - 2];
		EnforceMode(points.Length - 4);
	}
	
	public void Reset () {
        
		points = new Vector3[] {
			new Vector3(1f, UnityEngine.Random.Range(-width/2,width/2), UnityEngine.Random.Range(-height/2,height/2)),
			new Vector3(1f + deltaX, UnityEngine.Random.Range(-width/2,width/2), UnityEngine.Random.Range(-height/2,height/2)),
			new Vector3(1f + (deltaX * 2), UnityEngine.Random.Range(-width/2,width/2), UnityEngine.Random.Range(-height/2,height/2)),
			new Vector3(1f + (deltaX * 3), UnityEngine.Random.Range(-width/2,width/2), UnityEngine.Random.Range(-height/2,height/2))
		};
		modes = new BezierControlPointMode[] {
			BezierControlPointMode.Aligned,
			BezierControlPointMode.Aligned
		};

        for (int i = 0; i < 10; i++)
        {
            AddCurve();
        }
	}

    
    public ProgressBarBehaviour obj;

    /// <summary>
    /// Trouver la plus courte distance / progression entre le point du vaisseau et la courbe.
    /// </summary>
    /// <param name="mypoint"> point central du vaisseau </param>
    /// <returns></returns>
    public float ShortestDistanceFromPoint(Vector3 mypoint, out float Progression)
    {

        
        /* Generate list of points on load scene to save some time */
        float Precision = 1000.0f;
        var bezierPoints = new List<Vector3>(Mathf.RoundToInt(Precision));
       
        for (int i = 0; i < Precision; i++)
        {
            bezierPoints.Add(GetPoint( i/Precision));
        }

        float minDistance = float.MaxValue;
        Progression = 0; //t [0-1]

        for (int i = 0; i < Precision; i++)
        {
            var d = Vector3.Distance(mypoint, bezierPoints[i]);
            if (d < minDistance)
            {
                minDistance = d;
                Progression = i/Precision; // TODO set gui current progression
            }
            bezierPoints.Add(GetPoint(i / Precision));
        }

        obj.Value = Progression * 100;
        
        //Debug.Log("minDistance: " + minDistance + ", progression " + Progression * 100 + "%");

        return minDistance;
    }

    

}