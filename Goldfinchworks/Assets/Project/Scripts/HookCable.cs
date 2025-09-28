using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Rendering;

public class HookCable : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float lineWidth = 0.1f;

    [SerializeField] private Transform startRope;
    [SerializeField] private Transform oldRope;

    private void Start()
    {
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;

        // Устанавливаем количество точек
        lineRenderer.positionCount = 2;

        // Задаем позиции точек линии
        lineRenderer.SetPosition(0, startRope.position);
        lineRenderer.SetPosition(1, oldRope.position);
    }

    private void Update()
    {
        lineRenderer.SetPosition(0, startRope.position);
        lineRenderer.SetPosition(1, oldRope.position);
    }
}
