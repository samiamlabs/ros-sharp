/*
License: BSD
https://raw.githubusercontent.com/samiamlabs/dyno/master/LICENCE
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScanReader : MonoBehaviour
{

    public Transform m_ScanLink;

    public float m_RangeMinimum = 0.05f;
    public float m_RangeMaximum = 25.0f;
    public float m_ApertureAngle = 270.0f;

    public int m_ScanningFrequency = 10; // Sould be fraction of Unity Fixed Timestep Frequency

    public float m_AngularResolution = 0.25f;

    public bool m_UseTimeIncrement = false;

    public LayerMask m_LayerMask = -1;
    public bool m_Visualize = true;
    public Color m_VisualizationColor = Color.red;


    private int m_NumLines;

    private void Update()
    {
        UpdateNumLines();
        if (m_Visualize)
        {
            for (int index = 0; index < m_NumLines; index++)
            {
                GetDistance(index, true);
            }
        }
    }


    public void UpdateScan(ref float[] ranges, ref float[] intensities)
    {
        UpdateNumLines();
        for (int index = 0; index < m_NumLines; index++)
        {
            ranges[index] = GetDistance(index, false);

        }
    }

    private void UpdateNumLines()
    {
        m_NumLines = (int)Mathf.Round(m_ApertureAngle / m_AngularResolution) + 1;
    }

    private float GetDistance(int index, bool visualize)
    {
        Vector3 ray = m_ScanLink.rotation * Quaternion.AngleAxis(m_ApertureAngle / 2 + (-1 * index * m_AngularResolution), Vector3.up) * Vector3.forward;

        float distance = 0.0f;
        RaycastHit hit;

        if (Physics.Raycast(m_ScanLink.position, ray, out hit, m_RangeMaximum, m_LayerMask))
        {
            distance = hit.distance;
        }

        if (visualize)
        {
            Debug.DrawRay(m_ScanLink.position, ray * distance, m_VisualizationColor);
        }

        return distance;
    }
}
