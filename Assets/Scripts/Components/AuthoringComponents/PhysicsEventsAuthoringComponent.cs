using Unity.Entities;
using Unity.Entities.Hybrid;
using UnityEngine;

[DisallowMultipleComponent]
public class PhysicsEventsAuthoringComponent : MonoBehaviour, IConvertGameObjectToEntity
{
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {

        

        dstManager.AddBuffer<CollisionBufferDataComponent>(entity);
        dstManager.AddBuffer<TriggerBufferDataComponent>(entity);

    }
}
