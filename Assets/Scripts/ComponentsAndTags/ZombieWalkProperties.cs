using Unity.Entities;

namespace TMG.Zombies 
{
    public struct ZombieWalkProperties : IComponentData, IEnableableComponent
    {
        public float WalkSpeed;
        public float WalkAmplitude;
        public float WalkFrequency;
    }

    public struct ZombieTimer : IComponentData
    {
        public float Value;
    }

    public struct ZombieHeading : IComponentData
    {
        public float Value;
    }

    public struct NewZombieTag : IComponentData {}
}
