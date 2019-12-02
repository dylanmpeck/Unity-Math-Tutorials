using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneRayIntersection : MonoBehaviour
{
    public GameObject sphere;
    public GameObject quad;

    public GameObject[] fences;

    Plane mPlane;
    Plane[] fencePlanes;

    // Start is called before the first frame update
    void Start()
    {
        Vector3[] vertices = quad.GetComponent<MeshFilter>().mesh.vertices; 
        mPlane = new Plane(quad.transform.TransformPoint(vertices[0]) + new Vector3(0,0.3f,0), 
                           quad.transform.TransformPoint(vertices[1]) + new Vector3(0, 0.3f, 0), 
                           quad.transform.TransformPoint(vertices[2]) + new Vector3(0, 0.3f, 0));

        fencePlanes = new Plane[fences.Length];
        for (int i = 0; i < fences.Length; i++)
        {
            Vector3[] fenceVertices = fences[i].GetComponent<MeshFilter>().mesh.vertices;
            fencePlanes[i] = new Plane(fences[i].transform.TransformPoint(fenceVertices[0]),
                                       fences[i].transform.TransformPoint(fenceVertices[1]),
                                       fences[i].transform.TransformPoint(fenceVertices[2]));

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float t = 0.0f;


            if (mPlane.Raycast(ray, out t))
            {
                Vector3 hitPoint = ray.GetPoint(t);
                for (int i = 0; i < fencePlanes.Length; i++)
                {
                    if (Vector3.Dot(fencePlanes[i].normal, hitPoint - fences[i].transform.position) > 0)
                    {
                        //Debug.Log("Fence " + i + " " + Vector3.Dot(fencePlanes[i].normal, hitPoint - fences[i].transform.position));
                        return;
                    }
      
                }
                sphere.transform.position = hitPoint;
                //sphere.transform.position = new Vector3(Mathf.Clamp(sphere.transform.position.x, -7.0f, 7.0f),
                                                        //sphere.transform.position.y,
                                                        //Mathf.Clamp(sphere.transform.position.z, -7.0f, 7.0f));
            }
        }
    }
}
