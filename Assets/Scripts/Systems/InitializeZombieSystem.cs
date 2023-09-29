using TMG.Zombies;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

[BurstCompile]
[UpdateInGroup(typeof(InitializationSystemGroup))]
public partial struct InitializeZombieSystem : ISystem
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
        var ECB = new EntityCommandBuffer(Allocator.Temp);
        
        foreach (var zombie in SystemAPI.Query<ZombieWalkAspect>().WithAll<NewZombieTag>())
        {
            ECB.RemoveComponent<NewZombieTag>(zombie.Entity);
            ECB.SetComponentEnabled<ZombieWalkProperties>(zombie.Entity, false);
        }
        
        ECB.Playback(state.EntityManager);
    }
}