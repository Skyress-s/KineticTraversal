using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace UnityTemplateProjects
{
    public class VineProcessor : MonoBehaviour
    {
        internal class VineData
        {
            internal VineProcess process;
            internal Vine vine;
        }
        private List<VineData> processed = new List<VineData>();

        private void AddVine(Vine vine)
        {
            processed.Add(new VineData()
            {
                process = new VineProcess(vine),
                vine = vine
            });
        }
        private void Update()
        {
            foreach (Vine vine in Vine.vines)
            {
                if (!processed.Select(d => d.vine).Contains(vine))
                {
                    AddVine(vine);
                }
            }

            UpdateVines();
        }

        private void UpdateVines()
        {
            foreach (var process in processed.Select(d => d.process))
            {
                if (process.vine.points.Count < 2)
                {
                    continue;
                }

                if (process.lastWaypoint == process.vine.points.Count - 1)
                {
                    continue;
                }

                Vector3 targetPos = process.vine.points[process.lastWaypoint + 1].position;
                
            }
        }
    }

    internal class VineProcess
    {
        public Vine vine;
        public Vector3 currentPosition;
        public int lastWaypoint = 0;
        public VineProcess(Vine vine)
        {
            this.vine = vine;
        }
    }
}