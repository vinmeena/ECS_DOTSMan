using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
public partial class PlayerSystem : SystemBase
{
    protected override void OnUpdate()
    {
        float deltaTime = Time.DeltaTime;

        var entityCommandBuffer = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>().CreateCommandBuffer();

        float xDirection = Input.GetAxis("Horizontal");
        float zDirection = Input.GetAxis("Vertical");


        Entities.ForEach((ref MoveComponent moveComponent, in PlayerTagComponent tagComponent) =>
        {
            moveComponent.direction = new float3(-xDirection, 0f, -zDirection);

        }).Schedule();


        Entities
            .WithAll<PlayerTagComponent>()
            .ForEach((Entity entity, ref HealthDataComponent healthData,ref PowerPillDataComponent powerPill,ref DamageDataComponent damage) =>
            {
                damage.DamageValue = 100;
                powerPill.PowerPillTimer -= deltaTime;
                healthData.InvincibleTimer = powerPill.PowerPillTimer;

                if (powerPill.PowerPillTimer <= 0)
                {
                    AudioManager.instance.PlayMusic("game");
                    entityCommandBuffer.RemoveComponent<PowerPillDataComponent>(entity);
                    damage.DamageValue = 0;
                }

            }).WithoutBurst().Run();


    }
}
