using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Collections;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class SimpleMesh : MonoBehaviour
{
    [SerializeField]
    public enum MeshMode { 
        ExactMode,
        SimpleMode
    };
    [SerializeField]
    public MeshMode mode;
    #region ExactCreate
    [SerializeField]
    public List<Vector3> vertices = new List<Vector3>();

    [SerializeField]
    public List<int> TriangleIndexs = new List<int>();

    [SerializeField]
    public List<Vector2> uv = new List<Vector2>();
    #endregion
    #region SimpleCreate

    [SerializeField]
    public int column;
    [SerializeField]    
    public int row;

    [SerializeField]
    public float length;
    [SerializeField]
    public float width;
    #endregion

    [SerializeField] 
    public Mesh mesh;

    [SerializeField]
    public Material material;

}
