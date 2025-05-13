using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Versioning;
using Unity.VisualScripting;
using UnityEditor.AssetImporters;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public static class MeshBuilder 
{
    public static void AddTriangles(SimpleMesh simpleMesh, int vertex1, int vertex2, int vertex3)
    {
        if(vertex1 >= simpleMesh.vertices.Count || 
           vertex2 >= simpleMesh.vertices.Count ||
           vertex3 >= simpleMesh.vertices.Count)
        {
            Debug.LogError("Vertex index out of bound!");
            return;
        }
        simpleMesh.TriangleIndexs.Add(vertex1);
        simpleMesh.TriangleIndexs.Add(vertex2);
        simpleMesh.TriangleIndexs.Add(vertex3); 
    }
    public static SimpleMesh SimpleMeshPretreatmentSQ(SimpleMesh simpleMesh)
    {
        if (simpleMesh.length < 0 || simpleMesh.width < 0)
        {
            Debug.LogError("The value to define the shape of the mesh should be nonnegative in the simple create mode!");
            return null;
        }   
        if(simpleMesh.row <= 1 || simpleMesh.column <=1 )
        {
            Debug.LogError("The mesh's row or column must be greater than 1!");
            return null;
        }

        simpleMesh.vertices.Clear();
        simpleMesh.TriangleIndexs.Clear();
        simpleMesh.uv.Clear();

        GameObject go = simpleMesh.gameObject;

        float dx, dy;
        Vector3 position = go.transform.position;
        Vector3 nextPosition;

        dx = simpleMesh.length * 2 / (simpleMesh.column - 1);
        dy = simpleMesh.width * 2 / (simpleMesh.row - 1);
        for(int i = 0; i < simpleMesh.row ; i++)//uv(0,0)
        {
            //move to i column
            float v = i * (1.0f / (float)(simpleMesh.row - 1));

            nextPosition = position + new Vector3(-simpleMesh.length, -simpleMesh.width, 0);
            nextPosition = nextPosition + new Vector3(0, i * dy, 0);
            for (int j = 0; j < simpleMesh.column; j++)
            {
                simpleMesh.vertices.Add(nextPosition);
                nextPosition = nextPosition + new Vector3(dx, 0, 0);

                float u = j * (1.0f / (float)(simpleMesh.column - 1));
                simpleMesh.uv.Add(new Vector2(u, v));
            }
        }

        for (int i = 0; i < simpleMesh.row - 1; i++)
        {
            for (int j = 0; j < simpleMesh.column - 1; j++)
            {
                simpleMesh.TriangleIndexs.Add(i * simpleMesh.column + j);
                simpleMesh.TriangleIndexs.Add((i + 1) * simpleMesh.column + j);
                simpleMesh.TriangleIndexs.Add(i * simpleMesh.column + j + 1);

                simpleMesh.TriangleIndexs.Add((i + 1) * simpleMesh.column + j + 1);
                simpleMesh.TriangleIndexs.Add(i * simpleMesh.column + j + 1);
                simpleMesh.TriangleIndexs.Add((i + 1) * simpleMesh.column + j);
            }
        }

        return simpleMesh;
    }
    public static SimpleMesh SimpleMeshPretreatmentPL(SimpleMesh simpleMesh)
    {
        if (simpleMesh.length < 0 || simpleMesh.width < 0)
        {
            Debug.LogError("The value to define the shape of the mesh should be nonnegative in the simple create mode!");
            return null;
        }
        if (simpleMesh.row <= 1 || simpleMesh.column <= 1)
        {
            Debug.LogError("The mesh's row or column must be greater than 1!");
            return null;
        }

        simpleMesh.vertices.Clear();
        simpleMesh.TriangleIndexs.Clear();
        simpleMesh.uv.Clear();

        GameObject go = simpleMesh.gameObject;

        float dx, dz;
        Vector3 position = go.transform.position;
        Vector3 nextPosition;

        dx = simpleMesh.length * 2 / (simpleMesh.column - 1);
        dz = simpleMesh.width * 2 / (simpleMesh.row - 1);
        for (int i = 0; i < simpleMesh.row; i++)
        {
            //move to i column
            float v = i * (1.0f / (float)(simpleMesh.row - 1));

            nextPosition = position + new Vector3(-simpleMesh.length, 0, -simpleMesh.width);
            nextPosition = nextPosition + new Vector3(0, 0, i * dz);
            for (int j = 0; j < simpleMesh.column; j++)
            {
                simpleMesh.vertices.Add(nextPosition);
                nextPosition = nextPosition + new Vector3(dx, 0, 0);

                float u = j * (1.0f / (float)(simpleMesh.column - 1));
                simpleMesh.uv.Add(new Vector2(u, v));
            }
        }

        for (int i = 0; i < simpleMesh.row - 1; i++)
        {
            for (int j = 0; j < simpleMesh.column - 1; j++)
            {
                simpleMesh.TriangleIndexs.Add(i * simpleMesh.column + j);
                simpleMesh.TriangleIndexs.Add((i + 1) * simpleMesh.column + j);
                simpleMesh.TriangleIndexs.Add(i * simpleMesh.column + j + 1);

                simpleMesh.TriangleIndexs.Add((i + 1) * simpleMesh.column + j + 1);
                simpleMesh.TriangleIndexs.Add(i * simpleMesh.column + j + 1);
                simpleMesh.TriangleIndexs.Add((i + 1) * simpleMesh.column + j);
            }
        }

        return simpleMesh;
    }

    public static SimpleMesh CreateMesh(SimpleMesh simpleMesh)
    {
        simpleMesh.mesh = new Mesh();
        simpleMesh.mesh.vertices = simpleMesh.vertices.ToArray(); 
        simpleMesh.mesh.triangles = simpleMesh.TriangleIndexs.ToArray();
        simpleMesh.mesh.uv = simpleMesh.uv.ToArray();

        simpleMesh.mesh.RecalculateNormals();
        simpleMesh.mesh.RecalculateTangents();
        if (simpleMesh.material == null)
        {
            simpleMesh.gameObject.GetComponent<MeshRenderer>().material = Graphic.defaultGraphicMaterial;
        }
        else
        {
            simpleMesh.gameObject.GetComponent<MeshRenderer>().material = simpleMesh.material;
        }
        simpleMesh.gameObject.GetComponent<MeshFilter>().mesh = simpleMesh.mesh;
        return simpleMesh;
    }
}
