using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace TMG.Zombies
{
    [BurstCompile]
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct SpawnTombstoneSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<GraveyardProperties>();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            state.Enabled = false;
            
            var graveyardEntity = SystemAPI.GetSingletonEntity<GraveyardProperties>();
            var graveyard = SystemAPI.GetAspectRW<GraveyardAspect>(graveyardEntity);

            var entityCommanBuffer = new EntityCommandBuffer(Allocator.Temp);
            var tombstoneOffset = new float3(0f, -2f, 1f);
            var spawnTombstoneCount = graveyard.NumberTombstonesToSpawn;
            var spawnPoints = new NativeArray<float3>(spawnTombstoneCount, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);

            for (int i = 0; i < spawnTombstoneCount; i++)
            {
                var newTombstone = entityCommanBuffer.Instantiate(graveyard.TombstonePrefab);
                var newTombstoneTransform = graveyard.GetRandomTombstoneTransform;
                entityCommanBuffer.SetComponent(newTombstone, new LocalToWorldTransform{Value = newTombstoneTransform});

                var newZombieSpawnPoint = newTombstoneTransform.Position + tombstoneOffset;
                spawnPoints[i] = newZombieSpawnPoint;
            }

            graveyard.ZombieSPawnPoints = spawnPoints;
            entityCommanBuffer.Playback(state.EntityManager);
        }
    }    
}