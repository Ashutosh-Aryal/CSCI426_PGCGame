using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefresherRandomizer : MonoBehaviour
{
    [SerializeField]
    private GameObject refresherPrefab;

    private const float X_LIMIT = 6.0f;
    private const float MAX_DISTANCE = 2.5f;
    private const float MIN_SPAWN_DISTANCE = 0.5f;
    private const float MAX_RANDOM_RANGE = 1000.0f;

    // Static so they're shared amongst all Refreshers
    private static GameObject s_PrefabContainer = null;
    public static List<GameObject> s_SpawnedObjects = new List<GameObject>();

    private void Start()
    {
        if (s_PrefabContainer == null)
        {
            s_PrefabContainer = refresherPrefab;
        }
    }

    public static void SpawnRefresher(Vector3 lastRefresherLocation)
    {
        float distanceScalar = Random.Range(0.0f, MAX_RANDOM_RANGE) / MAX_RANDOM_RANGE;
        distanceScalar = Mathf.Pow(distanceScalar - 1.0f, 4.0f);

        float spawnDistance = (1.0f - distanceScalar) * MAX_DISTANCE + MIN_SPAWN_DISTANCE;

        float x_direction;

        if(lastRefresherLocation.x >= X_LIMIT / 2.0f) {
            x_direction = Random.Range(-10.0f, -1.0f);
        } else if(lastRefresherLocation.x <= -X_LIMIT / 2.0f) {
            x_direction = Random.Range(1.0f, 10.0f);
        } else {
            x_direction = Random.Range(-10.0f, 10.0f);
        }

        Vector2 spawnDirection = new Vector2(x_direction, Random.Range(1.0f, 10.0f));
        spawnDirection.Normalize();

        Vector2 currentRefresherLocation = lastRefresherLocation;

        Vector2 newRefresherSpawn = currentRefresherLocation + spawnDirection * spawnDistance;
        
        if(Mathf.Abs(newRefresherSpawn.x) >= Mathf.Abs(X_LIMIT))
        {
            newRefresherSpawn.x = (newRefresherSpawn.x >= X_LIMIT) ? X_LIMIT : -X_LIMIT;
        }

        // The refreshers should be diamond shaped
        Quaternion rotation = Quaternion.Euler(0, 0, 45);
        GameObject newRefresher = Instantiate(s_PrefabContainer, newRefresherSpawn, rotation);
        newRefresher.GetComponent<FadeBehavior>().StartFadeIn(); // Make refreshers fade in
        s_SpawnedObjects.Add(newRefresher);
    }
}
