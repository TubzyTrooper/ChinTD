using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float m_speed;
    [SerializeField] private Bezier[] m_routes;
    private int m_routeIndex;
    private float m_tParam;
    private bool m_shouldInvoke;

    // Start is called before the fi3rst frame update
    void Start()
    {
        m_tParam = 0f;
        m_routeIndex = 0;
        m_shouldInvoke = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_shouldInvoke)
        {
            StartCoroutine(WalkPath(m_routeIndex));
        }
    }

    private IEnumerator WalkPath(int routeIndex)
    {
        m_shouldInvoke = false;

        while(m_tParam < 1f)
        {
            m_tParam += Time.deltaTime * m_speed;

            transform.position = m_routes[routeIndex].Evaluate(m_tParam);

            yield return new WaitForEndOfFrame();
        }

        m_tParam = 0f;
        m_routeIndex++;

        if(m_routeIndex > m_routes.Length - 1)
        {
            m_routeIndex = 0;
        }

        m_shouldInvoke = true;
    }
}
