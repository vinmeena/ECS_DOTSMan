using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class CameraAudioListenerAuthoringComponent : MonoBehaviour, IConvertGameObjectToEntity
{
    public AudioListener audioListener;
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {

        dstManager.AddComponentData(entity, new CameraTagDataComponent() { });

        //Useful for converions of Built in types into Hybrid Components for Companian link and other.
        dstManager.AddComponentObject(entity, audioListener);
        
    }
}
