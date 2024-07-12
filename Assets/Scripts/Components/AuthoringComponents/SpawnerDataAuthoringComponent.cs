using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class SpawnerDataAuthoringComponent : MonoBehaviour, IConvertGameObjectToEntity,IDeclareReferencedPrefabs
{
    public GameObject SpawnPrefab;
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new SpawnerDataComponent() { SpawnPrefab = conversionSystem.GetPrimaryEntity(SpawnPrefab) });
    }

    public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
    {
        referencedPrefabs.Add(SpawnPrefab);
    }
}
