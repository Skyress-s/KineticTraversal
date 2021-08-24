using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnityTemplateProjects
{
    public class Vine
    {
        public static List<Vine> vines = new List<Vine>();
        public List<Point> points = new List<Point>();

        public class Point
        {
            public float createdAt;
            public Vector3 position;
            public Vector3 normal;

            public Point(Vector3 position, Vector3 normal)
            {
                this.createdAt = Time.time;
                this.position = position;
                this.normal = normal;
            }
        }

        
        public Vine()
        {
            vines.Add(this);
        }

        public void AddPoint(Vector3 point, Vector3 normal)
        {
            if (points.Any())
            {
                var lastPoint = points.Last();
                var distance = Vector3.Distance(lastPoint.position, point);
                if (distance > 1.7f || distance < 0.7f)
                {
                    return;
                }
            }
            points.Add(new Point(point, normal));
        }
    }
}