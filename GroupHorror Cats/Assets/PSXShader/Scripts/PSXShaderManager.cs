using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class PSXShaderManager : MonoBehaviour
{
    [SerializeField]
    public float _VertexGridResolution = 220.0f;
    [SerializeField]
    public bool _EnableTriangleSortingEmulation = false;
    [SerializeField]
    public bool _DepthDebugView = false;

    void Start()
    {
        UpdateValues();
    }

    void Update()
    {
        UpdateValues();
    }

    public void UpdateValues()
    {
        Shader.SetGlobalFloat("_PSX_GridSize", _VertexGridResolution);
        Shader.SetGlobalFloat("_PSX_DepthDebug", _DepthDebugView ? 1.0f : 0.0f);
        if (_EnableTriangleSortingEmulation)
        {
            Shader.EnableKeyword("PSX_ENABLE_TRIANGLE_SORTING");
        }
        else
        {
            Shader.DisableKeyword("PSX_ENABLE_TRIANGLE_SORTING");
        }
    }
}
