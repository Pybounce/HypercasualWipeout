using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEditor;

public class PropCreator
{
    private readonly PropData[] propData;
    private readonly OrientedPoint[] evenPoints;
    private readonly int laneCount;
    private readonly float laneWidth;
    private bool useAddressables;
    private LevelManager levelManager;

    private List<GameObject> props = new List<GameObject>();
    public List<GameObject> GetProps()
    {
        return props;
    }


    public PropCreator(PropData[] _data, OrientedPoint[] _evenPoints, float _laneWidth, int _laneCount, bool _useAddressables = true)
    {
        this.propData = _data;
        this.evenPoints = _evenPoints;
        this.laneWidth = _laneWidth;
        this.laneCount = _laneCount;
        this.useAddressables = _useAddressables;
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
                                Debug.LogWarning("Prop lane out of bounds - Prop = "  + prop.assetPath);
                                continue;
                            }
                        }
                        
                        SpawnProp(prop.assetPath, evenIndex, horizontalPos);
                        

                    }
                    else
                    {
                        Debug.LogWarning("Prop even index out of bounds - Prop = " + prop.assetPath);
                    }
                }
            }
        }



    }

    private void SpawnProp(string _assetPath, int _evenIndex, float _horizontalPos)
    {
        if (useAddressables)
        {
            GameObject addressableProp = levelManager.GetLoadedPrefab(_assetPath);
            addressableProp.transform.SetPositionAndRotation((evenPoints[_evenIndex].rotation * new Vector3(_horizontalPos, 0f, 0f)) + evenPoints[_evenIndex].point, evenPoints[_evenIndex].rotation);
            return;
        }
        GameObject prefabProp = (GameObject)AssetDatabase.LoadAssetAtPath(_assetPath, typeof(object));
        GameObject newProp = GameObject.Instantiate(prefabProp);
        newProp.transform.SetPositionAndRotation((evenPoints[_evenIndex].rotation * new Vector3(_horizontalPos, 0f, 0f)) + evenPoints[_evenIndex].point, evenPoints[_evenIndex].rotation);
        props.Add(newProp);
    }



}
