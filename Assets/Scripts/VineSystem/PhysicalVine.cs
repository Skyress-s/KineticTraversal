using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnityTemplateProjects
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class PhysicalVine : MonoBehaviour
    {
        private MeshFilter mf;

        private Vine vine;
        private int lastNumberOfPoints = -1;

        private void Awake()
        {
            mf = GetComponent<MeshFilter>();
        }

        private void Start()
        {
            mf.mesh = new Mesh();
            mf.mesh.MarkDynamic();
        }

        public void SetVine(Vine vine)
        {
            this.vine = vine;
        }
        

        private void Update()
        {
            if (vine.points.Count > lastNumberOfPoints && vine.points.Count > 1)
            {
                lastNumberOfPoints = vine.points.Count;

                mf.mesh.Clear();
                Vector3[] vertices = new Vector3[vine.points.Count * 2];
                int[] triangles = new int[(vine.points.Count * 2 - 2) * 6];
                Vector2[] uvs = new Vector2[vine.points.Count * 2];
                int i = 0;
                foreach (var p in vine.points)
                {
                    int vi = i * 2;
                    Vector3 direction;
                    if (i == vine.points.Count - 1)
                    {
                        direction = vine.points[i - 1].position - p.position;
                    }
                    else
                    {
                        direction = vine.points[i + 1].position - p.position;
                    }

                    var rotation = Quaternion.AngleAxis(-90, direction);
                    vertices[vi] = p.position + p.normal.normalized * 0.06f + (rotation * p.position).normalized * 0.1f;
                    //vertices[vi] = p.position;
                    //vertices[vi+1] = p.position;
                    
                    rotation = Quaternion.AngleAxis(90, direction);
                    vertices[vi+1] = p.position + p.normal.normalized * 0.06f + (rotation * p.position).normalized * 0.1f;

                    i++;
                }

                // for each 4 vertices use 0,1,2, 0,2,3
                for (int t = 0; t < vine.points.Count-1; t++)
                {
                    int ti = t * 6;
                    int vi = t * 2;
                    triangles[ti+0] = vi;
                    triangles[ti+1] = vi+1;
                    triangles[ti+2] = vi+2;
                    
                    triangles[ti+3] = vi+1;
                    triangles[ti+4] = vi+3;
                    triangles[ti+5] = vi+2;
                }
                
                mf.mesh.SetVertices(vertices);
                mf.mesh.SetTriangles(triangles, 0);

                for (int u = 0; u < uvs.Length; u++)
                {
                    uvs[u] = new Vector2(0, 0);
                }

                mf.mesh.SetUVs(0, uvs);
                
                mf.mesh.MarkModified();
            }
        }

        private void OnDrawGizmos()
        {
            Color old = Gizmos.color;
            Gizmos.color = Color.red;
            int i = 0;
            foreach (Vector3 v in this.mf.mesh.vertices)
            {
                var colors = new Color[]
                {
                    Color.red,
                    Color.blue,
                    Color.green,
                    Color.yellow
                };

                Gizmos.color = colors[i % 4];
                Gizmos.DrawCube(this.transform.TransformPoint(v), new Vector3(0.15f, 0.15f, 0.15f));
                i++;
            }
            Gizmos.color = old;
        }
    }
}