using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class PropCreator
{
    private readonly PropData[] propData;
    private readonly OrientedPoint[] evenPoints;
    private readonly int laneCount;
    private readonly float laneWidth;
    private LevelManager levelManager;

    public PropCreator(PropData[] _data, OrientedPoint[] _evenPoints, float _laneWidth, int _laneCount)
    {
        this.propData = _data;
        this.evenPoints = _evenPoints;
        this.laneWidth = _laneWidth;
        this.laneCount = _laneCount;
        this.levelManager = GameObject.FindObjectOfType<LevelManager>();
    }
        
    public void CreateProps()
    {

        foreach (PropData prop in propData)
        {
            //Loops through each prop type
            for (int clusterDataIndex = 0; clusterDataIndex < prop.clusterData.Length; clusterDataIndex++)
            {
                //Loops through each cluster of a prop type
                for (int i = 0; i < prop.clusterData[clusterDataIndex].amount; i++)
                {
                    //Loops through each individual prop in a cluster and places it
                    int evenIndex = prop.clusterData[clusterDataIndex].startIndex + (prop.clusterData[clusterDataIndex].interval * i);
                    if (evenIndex < evenPoints.Length && evenIndex >= 0)
                    {
                        float horizontalPos = prop.clusterData[clusterDataIndex].horizontalPosition;
                        if (prop.clusterData[clusterDataIndex].useLaneWidth)
                        {
                            if (horizontalPos <= laneCount && horizontalPos >= -laneCount)
                            {
                                horizontalPos = prop.clusterData[clusterDataIndex].horizontalPosition * laneWidth;
                            }
                            else
                            {
                                Debug.LogWarning("Prop lane out of bounds - Prop = "  + prop.propAssetRef);
                                continue;
                            }
                        }
                        
                        SpawnProp(prop.propAssetRef, evenIndex, horizontalPos);
                        

                    }
                    else
                    {
                        Debug.LogWarning("Prop even index out of bounds - Prop = " + prop.propAssetRef);
                    }
                }
            }
        }



    }

    private void SpawnProp(AssetReference _propAssetRef, int _evenIndex, float _horizontalPos)
    {
       
        GameObject newProp = levelManager.GetLoadedPrefab(_propAssetRef);
        newProp.transform.SetPositionAndRotation((evenPoints[_evenIndex].rotation * new Vector3(_horizontalPos, 0f, 0f)) + evenPoints[_evenIndex].point, evenPoints[_evenIndex].rotation);
    }



}
