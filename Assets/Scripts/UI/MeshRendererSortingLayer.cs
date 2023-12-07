using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshRendererSortingLayer : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.sortingLayerName = "UI";
        meshRenderer.sortingOrder = 0;
    }
}
