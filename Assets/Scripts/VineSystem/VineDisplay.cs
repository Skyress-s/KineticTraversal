using System;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using Random = System.Random;

public class VineDisplay : MonoBehaviour
{    
        public List<Vine.Point> processed = new List<Vine.Point>();
        public List<Vine> instantiatedVines = new List<Vine>();

        public GameObject flowerPrefab;
        public GameObject physicalVinePrefab;
        

        private void Update()
        {
            foreach (Vine vine in Vine.vines)
            {
                if (!instantiatedVines.Contains(vine)) // if no vines points created yet, create one
                {
                    instantiatedVines.Add(vine);
                    var go = Instantiate(physicalVinePrefab);
                    go.transform.position = vine.points[0].position;
                    go.transform.position = Vector3.zero;
                    go.GetComponent<PhysicalVine>().SetVine(vine);
                }
                foreach (Vine.Point point in vine.points)
                {
                    if (!processed.Contains(point))
                    {
                        processed.Add(point);

                        if (UnityEngine.Random.Range(0, 1f) < 0.23f)
                        {
                            GameObject flower = Instantiate(flowerPrefab, point.position, quaternion.identity);
                            flower.transform.up = point.normal;
                            flower.transform.localScale = Vector3.zero;
                            flower.transform.DOScale(1f, 0.87f).SetEase(Ease.OutBack).easeOvershootOrAmplitude = 1.7f;
                        }
                    }
                }
            }
        }

        private void OnDrawGizmos()
        {
            var old = Gizmos.color;
            Gizmos.color = Color.green;
            foreach (Vine vine in Vine.vines)
            {
                foreach (Vine.Point point in vine.points)
                {
                    Gizmos.DrawCube(point.position, Vector3.one*0.12f);
                }
            }

            Gizmos.color = old;
        }
    }