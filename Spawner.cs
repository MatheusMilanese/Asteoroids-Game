using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Asteroid asteroidPrefab;

    [SerializeField] private float timeRate;
    [SerializeField] private float trajectoryVariance;
    [SerializeField] private float spawnDistance;
    [SerializeField] private int spawnAmount;

    void Start()
    {
        InvokeRepeating(nameof(Spawn), timeRate, timeRate);
    }

    private void Spawn(){
        for(int i = 0; i < spawnAmount; i++){
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * spawnDistance;
            Vector3 spawnPoint = transform.position + spawnDirection;

            float variance = Random.Range(-trajectoryVariance, trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            Asteroid newAsteroid = Instantiate(asteroidPrefab, spawnPoint, rotation);
            newAsteroid.size = Random.Range(newAsteroid.minSize, newAsteroid.maxSize);
            newAsteroid.SetTrajectory(rotation * -spawnDirection);
        }
    }
}
