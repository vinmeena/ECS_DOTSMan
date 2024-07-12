using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Collections;

public class OnKillDataAuthoringComponent : MonoBehaviour, IConvertGameObjectToEntity
{
    public string clipName;
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {

        

        BlobBuilder blobBuilder = new BlobBuilder(Allocator.Temp);

        ref OnKillUsableData OnKillUsableData = ref blobBuilder.ConstructRoot<OnKillUsableData>();

        blobBuilder.AllocateString(ref OnKillUsableData.customString, clipName);


        BlobAssetReference<OnKillUsableData> blobAssetRef = 
            blobBuilder.CreateBlobAssetReference<OnKillUsableData>(Allocator.Persistent);

        dstManager.AddComponentData(entity, new OnKillDataComponent { customKillDataBlobAssetsRef = blobAssetRef });

        blobBuilder.Dispose();

    }

}
