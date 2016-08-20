using UnityEngine;
using System.Collections.Generic;

public class Tunnel : MonoBehaviour {

    List<Vector3> points;

    [SerializeField]
    public Difficulty difficulty = Difficulty.easy;

    public int width, heigth, length;


	// Use this for initialization
	void Awake () {
        points = GeneratePoints(width, heigth, length,difficulty);
	}
	
	// Update is called once per frame
	void Update () {
        
        
    }


    private List<Vector3> GeneratePoints(int width, int height, int lenght, Difficulty difficulty)
    {
        var temp_points = new List<Vector3>();
        
        for (int z = 0; z < lenght; z++)
        {
            if (z % (int)difficulty == 0)
            {
                temp_points.Add(GeneratePoint(width, height, z));
            }
        }

        return temp_points;
    }

    public Vector3 GeneratePoint(int width, int height, int z)
    {
        return new Vector3(Random.Range(0,width), Random.Range(0, height), z);
    }

    public enum Difficulty : byte
    {

        easy = 60,
        normal = 40,
        hard = 20
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        foreach (var point in points)
        {
            Gizmos.DrawSphere(point, 20);
        }
    }
}
