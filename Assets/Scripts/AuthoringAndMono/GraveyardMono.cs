using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace TMG.Zombies
{
    public class GraveyardMono : MonoBehaviour
    {
        public float2 FieldDimensions;
        public int NumberTombstonesToSpawn;
        public GameObject TombstonePrefab;
        
        public class GraveyardBaker : Baker<GraveyardMono>
        {
            public override void Bake(GraveyardMono authoring)
            {
                AddComponent(new GraveyardProperties
                {
                    FieldDimensions = authoring.FieldDimensions,
                    NumberTombstonesToSpawn = authoring.NumberTombstonesToSpawn,
                    TombstonePrefab = GetEntity(authoring.TombstonePrefab)
                });
            }
        }
    }   
}