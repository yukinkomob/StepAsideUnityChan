using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    public GameObject carPrefab;
    public GameObject coinPrefab;
    public GameObject conePrefab;
    public GameObject unityChan;
    private int startPos = 80;
    private int goalPos = 360;
    private int appearanceBoundary = 50;
    private int sectionDistance = 15;
    private int targetPos;
    private float posRange = 3.4f;
    private List<GameObject> carPrefabs = new List<GameObject>();
    private List<GameObject> coinPrefabs = new List<GameObject>();
    private List<GameObject> conePrefabs = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        targetPos = startPos;
    }

    // Update is called once per frame
    void Update()
    {
        float baseZ = unityChan.transform.position.z;
        if (baseZ > targetPos - appearanceBoundary && targetPos < goalPos)
        {
            createObjects(targetPos);
            targetPos += sectionDistance;
        }

        destroyObsoleteObjects(conePrefabs);
        destroyObsoleteObjects(coinPrefabs);
        destroyObsoleteObjects(carPrefabs);
    }

    void createObjects(int pos)
    {
        int i = pos;
        int num = Random.Range(1, 11);
        if (num <= 2)
        {
            for (float j = -1; j <= 1; j += 0.4f)
            {
                GameObject cone = Instantiate(conePrefab);
                cone.transform.position = new Vector3(4 * j, cone.transform.position.y, i);
                conePrefabs.Add(cone);
            }
        }
        else
        {
            for (int j = -1; j <= 1; j++)
            {
                int item = Random.Range(1, 11);
                int offsetZ = Random.Range(-5, 6);
                if (1 <= item && item <= 6)
                {
                    GameObject coin = Instantiate(coinPrefab);
                    coin.transform.position = new Vector3(posRange * j, coin.transform.position.y, i + offsetZ);
                    coinPrefabs.Add(coin);
                }
                else if (7 <= item && item <= 9)
                {
                    GameObject car = Instantiate(carPrefab);
                    car.transform.position = new Vector3(posRange * j, car.transform.position.y, i + offsetZ);
                    carPrefabs.Add(car);
                }
            }
        }
    }

    void destroyObsoleteObjects(List<GameObject> objects)
    {
        int eraseBoundaryZ = 10;
        float baseZ = unityChan.transform.position.z;

        for (int i = 0; i < objects.Count; i++)
        {
            if (objects[i] == null)
            {
                continue;
            }
            float z = objects[i].transform.position.z;
            if (z < baseZ - eraseBoundaryZ)
            {
                Destroy(objects[i]);
                objects[i] = null;
            }
        }
    }
}
