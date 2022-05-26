using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSXPostProcessEffect : MonoBehaviour
{
    public enum DitheringMatrixSize
    {
        Dither2x2,
        Dither4x4
    };

    public Vector3 colorDepth = new Vector3(64, 64, 64);
    public Vector3 ditherDepth = new Vector3(32, 32, 32);
    public DitheringMatrixSize ditheringMatrixSize;
    public int framerateLimit = 20;

    [SerializeField]
    private Shader _Shader;
    private Material _Material;

    void Start()
    {
        if (_Shader != null && _Shader.isSupported)
        {
            _Material = new Material(_Shader);
        }
        else
        {
            enabled = false;
            return;
        }
    }

    void Update()
    {
        Application.targetFrameRate = framerateLimit;
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        _Material.SetVector("_ColorResolution", colorDepth);
        _Material.SetVector("_DitherResolution", ditherDepth);
        _Material.SetFloat("_HighResDitherMatrix", ditheringMatrixSize == DitheringMatrixSize.Dither2x2 ? 0.0f : 1.0f);
        Graphics.Blit(source, destination, _Material);
    }
}
