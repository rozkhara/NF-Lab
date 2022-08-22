using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMaterials : MonoBehaviour
{
    public static CreateMaterials Instance { get; private set; }


    private readonly float param = 0.7f;

    private void Awake()
    {
        Instance = this;
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    public Material CreateMat(int mass)
    {
        Shader shader = gameObject.GetComponent<MeshRenderer>().material.shader;
        Material mat = new Material(shader);

        Random.InitState(mass);
        Color baseColor = new Color(Random.Range(0, 256) / 256f, Random.Range(0, 256) / 256f, Random.Range(0, 256) / 256f);

        Color.RGBToHSV(baseColor, out float H, out float S, out float V);
        if (H > 0.5f)
        {
            H -= 0.5f;
        }
        else
        {
            H += 0.5f;
        }
        //if (S > 0.5f)
        //{
        //    S -= 0.5f;
        //}
        //else
        //{
        //    S += 0.5f;
        //}
        Color edgeColor = Color.HSVToRGB(H, S * param, V);
        //Color edgeColor = new Color(1 - baseColor.r, 1 - baseColor.g, 1 - baseColor.b);

        mat.SetColor("_BaseColor", baseColor);
        mat.SetColor("_EdgeColor", edgeColor);
        return mat;
    }
}
