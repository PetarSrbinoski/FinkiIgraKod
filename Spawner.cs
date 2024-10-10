using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject proffesor;
    [SerializeField] GameObject BigProf;
    [SerializeField] GameObject Door;

    public float proffesorSpawnTime = 3.5f;
    public float throwerSpawnTime = 10;
    public bool continuousSpawning;

    public float spawnRange = 10f;
    public float minSpawnDistanceFromPlayer = 5f;

    public int ProffesorsToSpawn = 0;
    public int BigManToSpawn = 0;
    public int ThrowerToSpawn = 0;

    public int numberOfWaves = 0;
    private int wave = 0;

    [SerializeField] private List<GameObject> enemies = new List<GameObject>();

    public int NumberOfEnemies = 0;
    public LayerMask enemyLayer;

    private Transform player;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Start()
    {
        if (continuousSpawning)
        {
            StartCoroutine(SpawnEnemy(proffesorSpawnTime, proffesor));
            StartCoroutine(SpawnEnemy(throwerSpawnTime, BigProf));
        }
    }

    private IEnumerator SpawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);

        GameObject newEnemy = Instantiate(enemy, GetValidSpawnPosition(), Quaternion.identity);
        enemies.Add(newEnemy);

        StartCoroutine(SpawnEnemy(interval, enemy));
    }

    private void Update()
    {
        DetectEnemiesOnScreen();

        if (enemies.Count == 0 && wave < numberOfWaves)
        {
            StartCoroutine(SpawnWave());
            wave++;
        }

        if (enemies.Count == 0 && wave == numberOfWaves)
        {
            Door.SetActive(true);
            Door.GetComponent<Animator>().SetTrigger("openDoor");
        }


    }

    private IEnumerator SpawnWave()
    {
        for (int i = 0; i < ProffesorsToSpawn; i++)
        {
            SpawnSingleEnemy(proffesor);
            yield return new WaitForSeconds(1f);
        }

        for (int i = 0; i < BigManToSpawn; i++)
        {
            SpawnSingleEnemy(BigProf);
            yield return new WaitForSeconds(1f);
        }

        for (int i = 0; i < ThrowerToSpawn; i++)
        {
            SpawnSingleEnemy(proffesor);
            yield return new WaitForSeconds(1f);
        }
    }

    private void SpawnSingleEnemy(GameObject enemyPrefab)
    {
        GameObject newEnemy = Instantiate(enemyPrefab, GetValidSpawnPosition(), Quaternion.identity);
        enemies.Add(newEnemy);
    }

    private void DetectEnemiesOnScreen()
    {
        enemies.Clear();

        Vector2 screenBottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector2 screenTopRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        Vector2 center = (screenBottomLeft + screenTopRight) / 2;
        Vector2 size = screenTopRight - screenBottomLeft;

        Collider2D[] colliders = Physics2D.OverlapBoxAll(center, size, 0f, enemyLayer);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                enemies.Add(collider.gameObject);
            }
        }

        NumberOfEnemies = enemies.Count;
    }

    private Vector3 GetValidSpawnPosition()
    {
        Vector3 spawnPosition;
        do
        {
            spawnPosition = new Vector3(Random.Range(-spawnRange, spawnRange), transform.position.y, 0);
        }
        while (Vector3.Distance(spawnPosition, player.position) < minSpawnDistanceFromPlayer);

        return spawnPosition;
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        Vector2 screenBottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector2 screenTopRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        Vector2 center = (screenBottomLeft + screenTopRight) / 2;
        Vector2 size = screenTopRight - screenBottomLeft;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, size);
    }
}
