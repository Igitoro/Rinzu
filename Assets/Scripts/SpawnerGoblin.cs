using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class SpawnerGoblin : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject enemyPrefab2;
    public int maxEnemy;

    public float timeSpawn;
    private float timer;
    public Sprite SpawnYes;
    public Sprite SpawnNo;

    private SpriteRenderer spriteRenderer; 
    
    private int counter; 
    int EnemyType;  
    private void Start()
    {
        timer = timeSpawn;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }   

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = timeSpawn;
            EnemyType = Random.Range(1, 4);
            if(counter >= maxEnemy)
            {
                this.enabled = false;
                spriteRenderer.sprite = SpawnNo;
            }
            else if (counter < maxEnemy && transform.childCount < maxEnemy)
            {
                counter++;
                if(EnemyType == 1 || EnemyType == 2)
                {
                    Instantiate(enemyPrefab, transform);
                }
                else if (EnemyType == 3)
                {
                    Instantiate(enemyPrefab2, transform);
                }
                spriteRenderer.sprite = SpawnYes;
            }
        }
    }
}