using Unity.Burst;
using Unity.Entities;
using UnityEngine;

namespace TMG.Zombies
{
    [BurstCompile]
    [UpdateAfter(typeof(ZombieWalkSystem))]
    public partial struct ZombieEatSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }

        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            var ecbSingleton = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
            var brainEntity = SystemAPI.GetSingletonEntity<BrainTag>();

            new ZombieEatJob
            {
                DeltaTime = deltaTime,
                ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter(),
                BrainEntity = brainEntity
            }.ScheduleParallel();
        }
    }    
    
    [BurstCompile]
    public partial struct ZombieEatJob : IJobEntity
    {
        public float DeltaTime;
        public EntityCommandBuffer.ParallelWriter ECB;
        public Entity BrainEntity;

        [BurstCompile]
        private void Execute(ZombieEatAspect zombie, [EntityInQueryIndex]int sortKey)
        {
            zombie.Eat(DeltaTime, ECB, sortKey, BrainEntity);
        }
    }
}