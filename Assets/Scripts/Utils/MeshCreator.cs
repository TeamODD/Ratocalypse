using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using mattatz.Triangulation2DSystem;

[RequireComponent(typeof(MeshFilter))]
public class MeshCreator : MonoBehaviour
{
    [SerializeField]
    private Sprite _sprite;
    [SerializeField]
    private float _thickness;

    private MeshFilter _meshFilter;

    private void Awake()
    {
        _meshFilter = GetComponent<MeshFilter>();

        CreateMesh();
    }

    private void CreateMesh()
    {
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<Vector2> uvs = new List<Vector2>();
        List<Vector2> points = new List<Vector2>();


        float width = _sprite.bounds.size.x;
        float height = _sprite.bounds.size.y;

        _sprite.GetPhysicsShape(0, points);

        for (int i = 0; i < points.Count; i++)
        {
            Vector2 p = points[i];
            points[i] = new Vector2(p.x / width, p.y / height);
            Debug.Log(points[i]);
        }

        for (int i = 0; i < points.Count; i++)
        {
            Vector2 point1 = points[i];
            Vector2 point2 = points[(i + 1) % points.Count];

            AddWall(point1, point2, vertices, triangles, uvs, i == 0, i == points.Count - 1);
        }

        Polygon2D polygon = new Polygon2D(points.ToArray());
        Triangulation2D triangulation = new Triangulation2D(polygon);
        Triangle2D[] delaunayed = triangulation.Triangles;
        List<int> indices = new List<int>();

        foreach (Triangle2D triangle in delaunayed)
        {
            AddTriangle(triangle.a.Coordinate, triangle.b.Coordinate, triangle.c.Coordinate, vertices, triangles, uvs, true);
        }
        foreach (Triangle2D triangle in delaunayed)
        {
            AddTriangle(triangle.a.Coordinate, triangle.b.Coordinate, triangle.c.Coordinate, vertices, triangles, uvs, false);
        }

        _meshFilter.mesh.vertices = vertices.ToArray();
        _meshFilter.mesh.triangles = triangles.ToArray();
        _meshFilter.mesh.SetUVs(0, uvs);
        _meshFilter.mesh.RecalculateNormals();

    }

    private void AddTriangle(Vector3 a, Vector3 b, Vector3 c, List<Vector3> vertices, List<int> triangles, List<Vector2> uvs, bool isFront)
    {
        int frontBack = isFront ? 1 : -1;
        a = a + new Vector3(0, 0, frontBack * _thickness / 2);
        b = b + new Vector3(0, 0, frontBack * _thickness / 2);
        c = c + new Vector3(0, 0, frontBack * _thickness / 2);

        Vector2 offset = !isFront ? Vector2.zero : new Vector2(0.5f, 0f);

        int indexA = vertices.Count;
        vertices.Add(a);
        AddSurfaceUv(uvs, a, offset);

        int indexB = vertices.Count;
        vertices.Add(b);
        AddSurfaceUv(uvs, b, offset);

        int indexC = vertices.Count;
        vertices.Add(c);
        AddSurfaceUv(uvs, c, offset);


        if (Utils2D.LeftSide(a, b, c) == isFront)
        {
            triangles.Add(indexA);
            triangles.Add(indexB);
            triangles.Add(indexC);
        }
        else
        {
            triangles.Add(indexC);
            triangles.Add(indexB);
            triangles.Add(indexA);
        }
    }

    private void AddSurfaceUv(List<Vector2> uvs, Vector2 uv, Vector2 offset = default)
    {
        offset = offset==default ? Vector2.zero : offset;
        float x = uv.x;
        float y = uv.y;

        uvs.Add( new Vector2((x + 0.5f)/2 ,y + 0.5f) + offset);
    }

    private void AddWall(Vector2 point1, Vector2 point2, List<Vector3> vertices, List<int> triangles, List<Vector2> uvs, bool first, bool last)
    {
        if (last)
        {
            int count = vertices.Count;
            triangles.Add((count - 2 + 0) % count);
            triangles.Add((count - 2 + 1) % count);
            triangles.Add((count - 2 + 2) % count);
            triangles.Add((count - 2 + 2) % count);
            triangles.Add((count - 2 + 1) % count);
            triangles.Add((count - 2 + 3) % count);
            return;
        }

        if (first)
        {
            Vector3 vertex0 = (Vector3)point1 + new Vector3(0, 0, _thickness / 2);
            Vector3 vertex1 = (Vector3)point1 + new Vector3(0, 0, -_thickness / 2);
            vertices.Add(vertex0);
            uvs.Add(Vector2.zero);
            vertices.Add(vertex1);
            uvs.Add(Vector2.zero);
        }


        Vector3 vertex2 = (Vector3)point2 + new Vector3(0, 0, _thickness / 2);
        Vector3 vertex3 = (Vector3)point2 + new Vector3(0, 0, -_thickness / 2);


        vertices.Add(vertex2);
        uvs.Add(Vector2.zero);
        vertices.Add(vertex3);
        uvs.Add(Vector2.zero);

        int currentIndex = vertices.Count - 4;

        triangles.Add(currentIndex + 0);
        triangles.Add(currentIndex + 1);
        triangles.Add(currentIndex + 2);
        triangles.Add(currentIndex + 2);
        triangles.Add(currentIndex + 1);
        triangles.Add(currentIndex + 3);
    }


}
