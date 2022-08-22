using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMaterials : MonoBehaviour
{
    private GameObject go;
    public static CreateMaterials Instance { get; private set; }

    private readonly float baseToEdgeParam = 0.5f;


    private void Awake()
    {
        Instance = this;
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    private void Start()
    {
        go = GameObject.Find("MaterialHolder");
    }

    public Material CreateMat(int mass)
    {
        Shader shader = go.GetComponent<MeshRenderer>().material.shader;
        Material mat = new Material(shader);
        Random.InitState(mass);
        Color baseColor = new Color(Random.Range(0, 256) / 256f, Random.Range(0, 256) / 256f, Random.Range(0, 256) / 256f);

        Color.RGBToHSV(baseColor, out float H, out float S, out float V);
        Color edgeColor = Color.HSVToRGB(H * baseToEdgeParam, S * baseToEdgeParam, V);
        //Color edgeColor = new Color(1 - baseColor.r, 1 - baseColor.g, 1 - baseColor.b);

        mat.SetColor("_BaseColor", baseColor);
        mat.SetColor("_EdgeColor", edgeColor);
        return mat;
    }

}
