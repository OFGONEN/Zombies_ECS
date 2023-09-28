using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

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

            for (int i = 0; i < graveyard.NumberTombstonesToSpawn; i++)
                entityCommanBuffer.Instantiate(graveyard.TombstonePrefab);
            
            entityCommanBuffer.Playback(state.EntityManager);
        }
    }    
}