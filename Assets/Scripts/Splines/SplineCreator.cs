using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SplineCreator
{
    private readonly ScriptedLevel level;
    private readonly float spacing;
    private readonly OrientedPoint[] evenlySpacedPoints;

    public SplineCreator(ScriptedLevel _level, float _spacing)
    {
        this.level = _level;
        this.spacing = _spacing;
        this.evenlySpacedPoints = GetEvenlySpacedPoints(GetCurvePoints(this.level));
        
    }
    
    public Mesh GetSplineMesh(Scripted2DSpline spline2D)
    {
        return CreateMeshOnCurve(evenlySpacedPoints, spline2D);
    }
    public OrientedPoint[] GetEvenlySpacedPoints()
    {
        return evenlySpacedPoints;
    }
    

    /// <summary>
    /// Returns an array of the bezier point positions in the format [a, c, c, a, c, c, a]
    /// </summary>
    private Vector3[] GetCurvePoints(ScriptedLevel _level)
    {
        int bezierPointCount = (_level.bezierCurve.Length * 3) - 2;
        Vector3[] points = new Vector3[bezierPointCount];
        
        int currentSegmentIndex = 0;
        for (int i = 0; i < _level.bezierCurve.Length; i++)
        {
            BezierPoint bp = _level.bezierCurve[i];
            Quaternion rot = Quaternion.AngleAxis(bp.controlRotation, Vector3.up);
            Vector3 controlOne = new Vector3(bp.controlScale, 0, 0);
            controlOne = rot * controlOne;
            controlOne += bp.anchorPosition;
            Vector3 controlTwo = new Vector3(-bp.controlScale, 0, 0);
            controlTwo = rot * controlTwo;
            controlTwo += bp.anchorPosition;
            if (i > 0)
            {
                points[currentSegmentIndex] = controlTwo;
                currentSegmentIndex += 1;
            }
            points[currentSegmentIndex] = bp.anchorPosition;
            currentSegmentIndex += 1;
            if (i < _level.bezierCurve.Length - 1)
            {
                points[currentSegmentIndex] = controlOne;
                currentSegmentIndex += 1;
            }
        }
        return points;
    }
    /// <summary>
    /// Gets evenly spaced points along a bezier curve
    /// </summary>
    private OrientedPoint[] GetEvenlySpacedPoints(Vector3[] _curvePoints)
    {
        List<OrientedPoint> evenPoints = new List<OrientedPoint>();
        
        float t = 0;
        float tSpace = 0.001f;   //How much t is incremented by each check
        float spacingSquared = spacing * spacing;   //So it can compare square distance
        Vector3 lastPointPos = _curvePoints[0]; //Sets the start position of the curve
        for (int i = 0; i < _curvePoints.Length - 3; i += 3)
        {
            //Loops through each segment of the curve
            Vector3 a1 = _curvePoints[i];
            Vector3 c1 = _curvePoints[i + 1];
            Vector3 c2 = _curvePoints[i + 2];
            Vector3 a2 = _curvePoints[i + 3];
            while (t <= 1)
            {
                OrientedPoint currentOriPoint = GetBezierOrientedPoint(a1, c1, c2, a2, t);
                Vector3 pointDelta = currentOriPoint.point - lastPointPos;
                if (Vector3.SqrMagnitude(pointDelta) >= spacingSquared || (i == 0 && t == 0))
                {
                    //It has reached the correct spacing from the last point
                    lastPointPos = currentOriPoint.point;
                    evenPoints.Add(currentOriPoint);
                }
                t += tSpace;
            }
            t -= 1;
        }
        return evenPoints.ToArray();
    }

    /// <summary>
    /// Extrudes a 2D mesh along a curve using an array of evenly spaced points
    /// </summary>
    private Mesh CreateMeshOnCurve(OrientedPoint[] _evenPoints, Scripted2DSpline _spline2D)
    {
        Mesh splineMesh = new Mesh();
        Vector3[] positions = new Vector3[_evenPoints.Length * _spline2D.verts.Length];
        Vector3[] normals = new Vector3[_evenPoints.Length * _spline2D.verts.Length];
        Vector2[] uvs = new Vector2[_evenPoints.Length * _spline2D.verts.Length];
        int[] triangles = new int[(_evenPoints.Length - 1) * _spline2D.vertPairs.Length * 6];
        
        for (int i = 0; i < _evenPoints.Length; i++)
        {
            int vLength = _spline2D.verts.Length;
            for (int p = 0; p < vLength; p++)
            {
                //Rotates each point in the 2D spline so that it's facing the curves direction
                Vector3 newPos = _spline2D.verts[p].position + _spline2D.splineOffset;
                newPos = _evenPoints[i].rotation * newPos;
                newPos += _evenPoints[i].point;
                positions[(i * vLength) + p] = newPos;
                //Rotates each normal
                Vector3 newNormal = _spline2D.verts[p].normal;
                newNormal = _evenPoints[i].rotation * newNormal.normalized;
                newNormal.Normalize();
                normals[(i * vLength) + p] = newNormal;
                //Adds uvs
                uvs[(i * vLength) + p] = _spline2D.verts[p].uv + (i * _spline2D.uvDelta);
            }

            //Calculates the triangle indices for the extruded mesh
            if (i == _evenPoints.Length - 1) { continue; }  //Since the triangles join the current point and next, and there is no 'next' on the last point.
            int vpLength = _spline2D.vertPairs.Length;
            for (int vp = 0; vp < vpLength; vp++)
            {
                Vector2Int pair = _spline2D.vertPairs[vp];
                triangles[(i * vpLength * 6) + (vp * 6)] = pair.x + (i * vLength);
                triangles[(i * vpLength * 6) + (vp * 6) + 1] = pair.x + (i * vLength) + vLength;
                triangles[(i * vpLength * 6) + (vp * 6) + 2] = pair.y + (i * vLength);
                triangles[(i * vpLength * 6) + (vp * 6) + 3] = pair.y + (i * vLength);
                triangles[(i * vpLength * 6) + (vp * 6) + 4] = pair.x + (i * vLength) + vLength;
                triangles[(i * vpLength * 6) + (vp * 6) + 5] = pair.y + (i * vLength) + vLength;

            }
        }
        splineMesh.vertices = positions;
        splineMesh.normals = normals;
        splineMesh.triangles = triangles;
        splineMesh.SetUVs(0, uvs);

        return splineMesh;
    }

    private OrientedPoint GetBezierOrientedPoint(Vector3 _a, Vector3 _b, Vector3 _c, Vector3 _d, float _t)
    {
        //Where a, b, c, d = a1, c1, c2, a2
        Vector3 ab = Vector3.Lerp(_a, _b, _t);
        Vector3 bc = Vector3.Lerp(_b, _c, _t);
        Vector3 cd = Vector3.Lerp(_c, _d, _t);
        Vector3 abbc = Vector3.Lerp(ab, bc, _t);
        Vector3 bccd = Vector3.Lerp(bc, cd, _t);

        Vector3 p = Vector3.Lerp(abbc, bccd, _t);
        Quaternion r = Quaternion.LookRotation((bccd - abbc).normalized);
        return new OrientedPoint(p, r);
    }



}

public struct OrientedPoint
{
    public Vector3 point;
    public Quaternion rotation;
    public OrientedPoint(Vector3 _p, Quaternion _r)
    {
        this.point = _p;
        this.rotation = _r;
    }
}
