using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogSpawn : MonoBehaviour
{
    public GameObject fogPrefab;
    public GameObject[,] fogs;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    [ContextMenu("SpawnFog")]
    public void SpawnFog()
    {
        for(int x = -50; x < 50; x++)
            for (int y = -50; y < 50; y++)
            {
                GameObject temp = Instantiate(fogPrefab, new Vector3(x + 0.5f, y + 0.5f, 0), Quaternion.identity);
                temp.transform.SetParent(transform);
            }
    }
    
}
