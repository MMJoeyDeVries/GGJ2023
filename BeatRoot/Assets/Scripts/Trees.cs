using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Trees : MonoBehaviour
{
    public List<GameObject> Treez = new List<GameObject>();
    public Transform Player;
    public float YSpawnMin = 7.0f;
    public float YSpawnMax = 7.0f;
    
    const float width = 100.0f;
    private float _LastPlayerSpawnX = 0.0f;

    private List<GameObject> spawnedObjects = new List<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        SpawnNewTrees(20);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Player.position.x - _LastPlayerSpawnX);
        if ((Player.position.x - _LastPlayerSpawnX) > width)
        {
            SpawnNewTrees(20);
        }
        
        // check when player has passed tree and animate in hydration
        // TODO
    }

    void SpawnNewTrees(int count)
    {
        // destroy previous spawn first
        for (int i = 0; i < spawnedObjects.Count; ++i)
        {
            Destroy(spawnedObjects[i]);
        }
        
      
        float halfWidth = 0.5f * width;

        for (int i = 0; i < count; ++i)
        {
            int random = Random.Range(0, Treez.Count - 1);

            float randX = Random.Range(-halfWidth, halfWidth);
            Vector3 spawnPos = new Vector3(Player.position.x + randX, Random.Range(YSpawnMin, YSpawnMax), -15.0f);
            GameObject tree = Instantiate(Treez[random], spawnPos, Quaternion.identity);
            
            spawnedObjects.Add(tree);
        }

        _LastPlayerSpawnX = Player.position.x;
    }
}
