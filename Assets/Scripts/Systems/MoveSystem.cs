using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Physics;
using UnityEngine;

public partial class MoveSystem : SystemBase
{
    protected override void OnUpdate()
    {

        Entities
            .ForEach((ref PhysicsVelocity physicsVelocity, in MoveComponent moveComponent) => 
            {
                var move = moveComponent.direction * moveComponent.speed;

                physicsVelocity.Linear = move;

            }).Schedule();

    }
}
