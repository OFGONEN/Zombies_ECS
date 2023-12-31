using System;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace TMG.Zombies
{
    public partial class CameraControllerSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var brainEntity = SystemAPI.GetSingletonEntity<BrainTag>();
            var brainScale = SystemAPI.GetComponent<LocalToWorldTransform>(brainEntity).Value.Scale;

            var cameraSingleton = CameraSingleton.Instance;

            var positionFactor = (float)SystemAPI.Time.ElapsedTime * cameraSingleton.Speed;
            var height = cameraSingleton.HeightAtScale(brainScale);
            var radius = cameraSingleton.RadiusAtScale(brainScale);

            cameraSingleton.transform.position = new Vector3
            {
                x = MathF.Cos(positionFactor) * radius,
                y = height,
                z = MathF.Sin(positionFactor) * radius,
            };
            
            cameraSingleton.transform.LookAt(Vector3.zero, Vector3.up);
        }
    }    
}

