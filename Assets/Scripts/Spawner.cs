using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject platformPrefab;
    private Queue<GameObject> platformPooling = new Queue<GameObject>();

    // In variables ko bahar hona chahiye taake ye yaad rahein
    private int poolSize = 120; // Size barha diya
    private Vector3 lastPos;
    private float size;
    private int lastRand = -1;
    private int platformsInSameDirection = 0;

    private static Spawner instance;
    public static Spawner Instance { get { return instance; } }

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        lastPos = platformPrefab.transform.position;
        size = platformPrefab.GetComponent<MeshRenderer>().bounds.size.x;

        // Pool ko fill karein
        for (int i = 0; i < poolSize; i++)
        {
            SpawnObjectPooling();
        }

        // Shuru mein 20 platforms bana dein
        for (int i = 0; i < 20; i++)
        {
            SpawnPlatform();
        }
    }

    private void SpawnObjectPooling()
    {
        GameObject obj = Instantiate(platformPrefab);
        obj.SetActive(false);
        platformPooling.Enqueue(obj);
    }

    public void SpawnPlatform()
    {
        int rand = Random.Range(0, 2);

        // Same direction logic
        if (rand == lastRand)
        {
            platformsInSameDirection++;
            if (platformsInSameDirection > 5)
            {
                rand = (rand == 0) ? 1 : 0;
                platformsInSameDirection = 0;
            }
        }
        else
        {
            platformsInSameDirection = 0;
        }

        lastRand = rand;

        // Position set karein
        if (rand == 0) lastPos.x += size;
        else lastPos.z += size;

        // Pooling logic
        GameObject platformToReuse = platformPooling.Dequeue();
        platformToReuse.SetActive(true);
        platformToReuse.transform.position = lastPos;

        // Agar platform ke child mein diamond ha, toh usay yahan enable karein
        // platformToReuse.transform.GetChild(0).gameObject.SetActive(true);

        platformPooling.Enqueue(platformToReuse);
    }
}