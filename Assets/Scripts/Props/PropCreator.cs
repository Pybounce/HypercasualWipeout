using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropCreator : MonoBehaviour
{
    private PropData[] propData;
    private OrientedPoint[] evenPoints;

    public PropCreator(PropData[] _data, OrientedPoint[] _evenPoints)
    {
        this.propData = _data;
        this.evenPoints = _evenPoints;
    }
        
    public void CreateProps()
    {
        foreach (PropData prop in propData)
        {
            for (int i = 0; i < prop.positions.Length; i++)
            {
                if (prop.positions[i].x < evenPoints.Length)
                {
                    SpawnProp(prop.propObject, prop.positions[i].x, prop.positions[i].y);
                }
                else
                {
                    Debug.LogWarning("Prop even position out of bounds");
                }
            }
        }
    }

    private void SpawnProp(GameObject _prop, int _evenIndex, int _lane)
    {
        GameObject newObstacle = GameObject.Instantiate(_prop);
        newObstacle.transform.position = (evenPoints[_evenIndex].rotation * new Vector3(_lane, 0f, 0f)) + evenPoints[_evenIndex].point;
    }

}
