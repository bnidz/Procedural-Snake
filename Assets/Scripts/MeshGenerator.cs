using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class MeshGenerator : MonoBehaviour
{

	List<int> triangles;
	List<Vector3> vertices;
	// Start is called before the first frame update
	void Start()
    {
        CreateCube();
    }


	void Update()
	{
		if (Input.GetKeyDown("space"))
		{

			AddPart();


		}
	}
	private void CreateCube()
	{
		vertices = new List<Vector3>()
		{
			//front
			new Vector3 (0, 0, 0),
			new Vector3 (1, 0, 0),
			new Vector3 (1, 1, 0),
			new Vector3 (0, 1, 0),
			//back
			new Vector3 (0, 1, 1),
			new Vector3 (1, 1, 1),
			new Vector3 (1, 0, 1),
            new Vector3 (0, 0, 1),
		};

			triangles = new List<int> {

			0, 2, 1, //face front
			0, 3, 2,

			2, 3, 4, //face top
			2, 4, 5,

			1, 2, 5, //face right
			1, 5, 6,

			0, 7, 4, //face left
			0, 4, 3,

			5, 4, 7, //face back
			5, 7, 6,

			0, 6, 7, //face bottom
			0, 1, 6

		};

		Mesh mesh = GetComponent<MeshFilter>().mesh;
		mesh.Clear();
		mesh.vertices = vertices.ToArray();
		mesh.triangles = triangles.ToArray(); 
		mesh.Optimize();
		mesh.RecalculateNormals();
	}

	private void AddPart()
	{

        //remove bottom face
        for (int i = triangles.Count - 6; i < triangles.Count; i++)
        {
		triangles.RemoveAt(i);
        }

		// add new vertices belos deleted faces
		Vector3[] newVertices = new[] { new Vector3(0, -1, 0), new Vector3(1, -1, 0), new Vector3(0, -1, 1), new Vector3(1, -1, 1) };
		vertices.AddRange(newVertices);
		transform.Translate(transform.up * 1);

		//create new faces
		int[] newFaces = new[] {

		(vertices.Count -7), (vertices.Count -2), (vertices.Count -3),
        vertices.Count -3, vertices.Count -8, vertices.Count -7,

		vertices.Count -2, vertices.Count-7, vertices.Count -5,
		vertices.Count -2, vertices.Count -5, vertices.Count,

		vertices.Count -3, vertices.Count -1, vertices.Count -6,
		vertices.Count -3, vertices.Count -6, vertices.Count -8,

		vertices.Count -5, vertices.Count -6, vertices.Count -1,
		vertices.Count -5, vertices.Count -1, vertices.Count,

		//new bottom
		vertices.Count -3, vertices.Count, vertices.Count -1,
		vertices.Count -3, vertices.Count -2, vertices.Count

		//1,9,8,
		//8,0,1,

		//9,1,6,
		//9,6,11,

		//8,10,7,
		//8,7,3,

		//6,7,10,
		//6,10,11,

		//8,11,10,
		//8,9,11

		};

		triangles.AddRange(newFaces);


		//move SNEK UP 1 Y


		//hmm entä jos nostaa käärmettä ja kopioi viime vectorit :o
		Debug.Log(triangles.Count);

		Mesh mesh = GetComponent<MeshFilter>().mesh;
		mesh.Clear();
		mesh.vertices = vertices.ToArray();
		mesh.triangles = triangles.ToArray();
		mesh.Optimize();
        mesh.RecalculateNormals();

	}
	

}
