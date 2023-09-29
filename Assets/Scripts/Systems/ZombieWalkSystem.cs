using Unity.Burst;
using Unity.Entities;
using UnityEngine.Rendering;

namespace TMG.Zombies
{
    [BurstCompile]
    [UpdateAfter(typeof(ZombieRiseSystem))]
    public partial struct ZombieWalkSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;

            new ZombieWalkJob
            {
                DeltaTime = deltaTime
            }.ScheduleParallel();
        }
    }
    
    [BurstCompile]
    public partial struct ZombieWalkJob : IJobEntity
    {
        public float DeltaTime;
        
        [BurstCompile]
        public void Execute(ZombieWalkAspect zombie)
        {
            zombie.Walk(DeltaTime);
        }
    }
    
}