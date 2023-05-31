using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path;
using UnityEngine;

public class Bezier : MonoBehaviour
{
    [SerializeField] private Transform[] m_controlPoints;
    private Vector2 m_gizmoPosition;

    private void OnDrawGizmos()
    {
        // for i in range(0, 10, 2)
        for (float t = 0; t <= 1; t += 0.05f) 
        {
            m_gizmoPosition = Mathf.Pow(1 - t, 3) * m_controlPoints[0].position +
                3 * Mathf.Pow(1 - t, 2) * t * m_controlPoints[1].position +
                3 * (1 - t) * Mathf.Pow(t, 2) * m_controlPoints[2].position +
                Mathf.Pow(t, 3) * m_controlPoints[3].position;

            Gizmos.DrawSphere(m_gizmoPosition, 0.3f);
        }

        Gizmos.DrawLine(m_controlPoints[0].position, m_controlPoints[1].position); 
        Gizmos.DrawLine(m_controlPoints[2].position, m_controlPoints[3].position);
    }
}
