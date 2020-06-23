using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]


public class snekmesh : MonoBehaviour
{

    public GameObject SNakePartOb;

    public List<Transform> snek1;
    public List<Transform> snek2;


    public List<Vector3> vertices; // vertices2;
    List<int> triangles;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < snek1.Count; i++)
        {
            vertices.Add(snek1[i].position);
        }
        for (int i = 0; i < snek2.Count; i++)
        {
            vertices.Add(snek2[i].position);
        }

        triangles = new List<int>
        {
            // 1 QUAD - 0,11,6
            vertices.Count -12, vertices.Count -1, vertices.Count -6,
            vertices.Count -12, vertices.Count -7, vertices.Count -1,

            // 2 QUAD - 5,10,11 - 5,4,10
            vertices.Count -7, vertices.Count -2, vertices.Count -1,
            vertices.Count -7, vertices.Count -8, vertices.Count -2,

            // 3 QUAD - 4,9,10 - 4,3,9
            vertices.Count -8, vertices.Count -3, vertices.Count -2,
            vertices.Count -8, vertices.Count -9, vertices.Count -3,
                         
            //// 4 QUAD - 8,3,2 - 8,9,3
            vertices.Count -4, vertices.Count -9, vertices.Count -10,
            vertices.Count -4, vertices.Count -3, vertices.Count -9,

            // 5 QUAD - 7,2,1 - 7,8,2
            vertices.Count -5, vertices.Count -10, vertices.Count -11,
            vertices.Count -5, vertices.Count -4, vertices.Count -10,

            // 6 QUAD - 6,1,0 - 6,7,1
            vertices.Count -6, vertices.Count -11, vertices.Count -12,
            vertices.Count -6, vertices.Count -5, vertices.Count -11,
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


       // GameObject snekpartClone = SNakePartOb;  
      GameObject snekpartClone = Instantiate(SNakePartOb,new Vector3 (transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
      snekpart sp = snekpartClone.GetComponent<snekpart>();
      transform.Translate(transform.forward *= 1);
        //maybe childaa äsknöne obu
        snekpartClone.transform.parent = gameObject.transform;


        for (int i = 0; i < sp.snekparts.Count; i++)
        {
            vertices.Add(sp.snekparts[i].position);
        }

        int[] newFaces = new[] {

        
            // 1 QUAD - 0,11,6
            vertices.Count -12, vertices.Count -1, vertices.Count -6,
            vertices.Count -12, vertices.Count -7, vertices.Count -1,

            // 2 QUAD - 5,10,11 - 5,4,10
            vertices.Count -7, vertices.Count -2, vertices.Count -1,
            vertices.Count -7, vertices.Count -8, vertices.Count -2,

            // 3 QUAD - 4,9,10 - 4,3,9
            vertices.Count -8, vertices.Count -3, vertices.Count -2,
            vertices.Count -8, vertices.Count -9, vertices.Count -3,
                         
            //// 4 QUAD - 8,3,2 - 8,9,3
            vertices.Count -4, vertices.Count -9, vertices.Count -10,
            vertices.Count -4, vertices.Count -3, vertices.Count -9,

            // 5 QUAD - 7,2,1 - 7,8,2
            vertices.Count -5, vertices.Count -10, vertices.Count -11,
            vertices.Count -5, vertices.Count -4, vertices.Count -10,

            // 6 QUAD - 6,1,0 - 6,7,1
            vertices.Count -6, vertices.Count -11, vertices.Count -12,
            vertices.Count -6, vertices.Count -5, vertices.Count -11,
        };
        triangles.AddRange(newFaces);

        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.Optimize();
        mesh.RecalculateNormals();
    }
        //  Update is called once per frame
        void Update()
    {
        if (Input.GetKeyDown("space"))
        {

            AddPart();


        }
        //Mesh mesh = GetComponent<MeshFilter>().mesh;
        //mesh.Clear();
        //mesh.vertices = vertices.ToArray();
        //mesh.triangles = triangles.ToArray();
        //mesh.Optimize();
        //mesh.RecalculateNormals();
    }
}
