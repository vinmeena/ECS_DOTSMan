using Unity.Entities;
public struct OnKillDataComponent : IComponentData
{
    //We cannot use string in ECS Ecosystem, So for that we have to use "BlobString" using BlobAssetReference.
    //BlobAssets Basically useful for using immutable data throughout the ecs ecosystem and it's not changeable at runtime.

    public BlobAssetReference<OnKillUsableData> customKillDataBlobAssetsRef;
}

public struct OnKillUsableData
{
    public BlobString customString;
}

