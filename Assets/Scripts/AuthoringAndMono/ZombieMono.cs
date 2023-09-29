using Unity.Entities;
using UnityEngine;

namespace TMG.Zombies
{
    public class ZombieMono : MonoBehaviour
    {
        public float RiseRate;
        public float WalkSpeed;
        public float WalkAmplitude;
        public float WalkFrequency;
    }

    public class ZombieBaker : Baker<ZombieMono>
    {
        public override void Bake(ZombieMono authoring)
        {
            AddComponent(new ZombieRiseRate
            {
                Value = authoring.RiseRate
            });
            AddComponent(new ZombieWalkProperties
            {
                WalkSpeed = authoring.WalkSpeed,
                WalkAmplitude = authoring.WalkAmplitude,
                WalkFrequency = authoring.WalkFrequency
            });    
            
            AddComponent<ZombieTimer>();
            AddComponent<ZombieHeading>();
        }
    }
}