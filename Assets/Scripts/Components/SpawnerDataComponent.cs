using Unity.Entities;
public struct SpawnerDataComponent : IComponentData
{
    public Entity SpawnObject;
    public Entity SpawnPrefab;
}
