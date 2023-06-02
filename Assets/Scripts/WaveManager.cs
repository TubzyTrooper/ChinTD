using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private Bezier[] m_routes;
    private List<int> m_waveContents;
    private int m_waveIndex;
    [SerializeField] private GameObject m_enemy;

    // Start is called before the first frame update
    void Start()
    {
        LoadWaves();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartWave()
    {
        StartCoroutine(SpawnWave(m_waveIndex));
        m_waveIndex++;
    }

    private void LoadWaves()
    {
        m_waveContents = new List<int>();

        TextAsset waveFile = (TextAsset)Resources.Load("waves");

        string[] strings = waveFile.text.Split('\n');
        foreach (string s in strings)
        {
            int num = int.Parse(s);
            m_waveContents.Add(num);
        }
    }

    private IEnumerator SpawnWave(int waveindex)
    {
        int waveCount = m_waveContents[waveindex];
        while(waveCount > 0)
        {
            Enemy enemy = Instantiate(m_enemy, m_routes[0].Evaluate(0), Quaternion.identity).GetComponent<Enemy>();
            enemy.SetRoutes(m_routes);

            yield return new WaitForSeconds(0.2f);
            waveCount--;
        }
    }
}

[CustomEditor(typeof(WaveManager))]
public class TestOnInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if(GUILayout.Button("Next Wave"))
        {
            WaveManager manager = (WaveManager)target;
            manager.StartWave();
        }
    }
}
