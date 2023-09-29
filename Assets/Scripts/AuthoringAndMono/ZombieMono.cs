using Unity.Entities;
using UnityEngine;

namespace TMG.Zombies
{
    public class ZombieMono : MonoBehaviour
    {
        //Rise
        public float RiseRate;
        
        //Walk
        public float WalkSpeed;
        public float WalkAmplitude;
        public float WalkFrequency;
        
        //Eat
        public float EatDamage;
        public float EatAmplitude;
        public float EatFrequency;
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
            
            AddComponent(new ZombieEatProperties
            {
                EatDamagePerSecond = authoring.EatDamage,
                EatAmplitude = authoring.EatAmplitude,
                EatFrequency = authoring.EatFrequency
            });
            
            AddComponent<ZombieTimer>();
            AddComponent<ZombieHeading>();
            AddComponent<NewZombieTag>();
        }
    }
}