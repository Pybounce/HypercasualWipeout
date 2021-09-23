using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelCreator))]
public class LevelCreatorEditor : Editor
{
    LevelCreator levelCreator;
    private Vector3[] curvePoints;
    private bool snapControlRot = true;
    private bool snapControlMag = true;
    private float newControlMag = 15f;

    private void OnEnable()
    {
        levelCreator = target as LevelCreator;
    }
    private void OnDisable()
    {
        levelCreator.ClearAll();

    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        using (new EditorGUI.DisabledScope(PybUtilityScene.GetCurrentScene().name != "LevelEditorScene" || levelCreator.level == null))
        {
            if (GUILayout.Button("Load Splines") == true)
            {
                levelCreator.LoadSplines();
                curvePoints = levelCreator.splineCreator.GetCurvePoints();
            }
            snapControlRot = GUILayout.Toggle(snapControlRot, "Snap Control Rotation");
            snapControlMag = GUILayout.Toggle(snapControlMag, "Snap Control Magnitude");
        }

    }

    private void OnSceneGUI()
    {
        Event guiEvent = Event.current;
        if (curvePoints != null && curvePoints.Length > 0)
        {
            for (int i = 0; i < curvePoints.Length; i += 3)
            {
                Handles.color = Color.red;
                Vector3 anchorDelta = MovePoint(i, guiEvent);
                PlaceControlPoints(i, anchorDelta, guiEvent);
                
            }
        }
        if (guiEvent.type == EventType.MouseDown && guiEvent.button == 0 && guiEvent.modifiers == EventModifiers.None)
        {
            AddAnchorPoint(GetMouesPosition(guiEvent, curvePoints[curvePoints.Length - 1].y));
            levelCreator.LoadSplines();
        }
        HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
    }

    private void PlaceControlPoints(int _index, Vector3 _anchorDelta, Event _guiEvent)
    {
        
        if (_index - 1 >= 0)
        {
            Handles.color = Color.white;
            Handles.DrawAAPolyLine(2, new Vector3[] { curvePoints[_index - 1], curvePoints[_index] });
            Handles.color = Color.blue;
            curvePoints[_index - 1] += _anchorDelta;
            if (MovePoint(_index - 1, _guiEvent) != default && snapControlRot)
            {
                AlignControlRotations(_index - 1, _index + 1);
            }
        }
        if (_index + 1 < curvePoints.Length)
        {
            Handles.color = Color.white;
            Handles.DrawAAPolyLine(2, new Vector3[] { curvePoints[_index + 1], curvePoints[_index] });
            Handles.color = Color.green;
            curvePoints[_index + 1] += _anchorDelta;
            if (MovePoint(_index + 1, _guiEvent) != default && snapControlRot)
            {
                AlignControlRotations(_index + 1, _index - 1);
            }
        }
    }

    private void AlignControlRotations(int _rootIndex, int _pairedIndex)
    {
        int anchorIndex = _rootIndex + 1;
        if (_rootIndex > _pairedIndex) { anchorIndex = _rootIndex - 1; }

        if (anchorIndex - 1 >= 0 && anchorIndex + 1 < curvePoints.Length)
        {
            Vector3 delta = curvePoints[anchorIndex] - curvePoints[_rootIndex];
            if (snapControlMag == false)
            {
                delta.Normalize();
                float magnitude = (curvePoints[_pairedIndex] - curvePoints[anchorIndex]).magnitude;
                delta *= magnitude;
            }
            
            
            curvePoints[_pairedIndex] = curvePoints[anchorIndex] + delta;

        }
    }

    private Vector3 MovePoint(int _index, Event _guiEvent)
    {
        Vector3 newPos = Handles.FreeMoveHandle(curvePoints[_index], Quaternion.identity, 2, Vector3.zero, Handles.SphereHandleCap);
        if (newPos != curvePoints[_index])
        {
            Vector3 originalPos = curvePoints[_index];
            //Handle being moved
            if (_guiEvent.modifiers == EventModifiers.Shift)
            {
                curvePoints[_index].y = newPos.y;
            }
            else
            {
                curvePoints[_index] = GetMouesPosition(_guiEvent, originalPos.y);
                curvePoints[_index].y = originalPos.y;
            }
            levelCreator.LoadSplines();

            return curvePoints[_index] - originalPos;
        }
        return Vector3.zero;
    }

    private Vector3 GetMouesPosition(Event _guiEvent, float _planeHeight)
    {
        Ray mouseRay = HandleUtility.GUIPointToWorldRay(_guiEvent.mousePosition);
        float dstToPlane = (_planeHeight - mouseRay.origin.y) / mouseRay.direction.y;
        return mouseRay.GetPoint(dstToPlane);
    }

    private void AddAnchorPoint(Vector3 _position)
    {
        Vector3[] newCurvePoints = new Vector3[curvePoints.Length + 3];
        for (int i = 0; i < curvePoints.Length; i++)
        {
            newCurvePoints[i] = curvePoints[i];
        }
        Vector3 direction = _position - curvePoints[curvePoints.Length - 1];
        direction.Normalize();
        newCurvePoints[newCurvePoints.Length - 3] = newCurvePoints[newCurvePoints.Length - 4] + (direction * 10f);
        newCurvePoints[newCurvePoints.Length - 2] = _position - (direction * newControlMag);
        newCurvePoints[newCurvePoints.Length - 1] = _position;
        levelCreator.level.curvePoints = newCurvePoints;
       
        curvePoints = newCurvePoints;
        AlignControlRotations(newCurvePoints.Length - 5, newCurvePoints.Length - 3);
    }
}
