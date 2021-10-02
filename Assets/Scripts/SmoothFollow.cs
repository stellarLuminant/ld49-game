﻿using System;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    public Vector3 Offset;
    public Single Factor = 1f / 3f;
    public Transform Target;

    private void Start()
    {
        Offset = transform.position - Target.position;
    }

    private void Update()
    {
        Vector3 displacement = Target.position - (transform.position - Offset);

        Single x = Mathf.SmoothStep(0, displacement.x, Factor);
        Single y = Mathf.SmoothStep(0, displacement.y, Factor);
        Single z = Mathf.SmoothStep(0, displacement.z, Factor);

        transform.position = transform.position + new Vector3(x, y, z);
    }
}
