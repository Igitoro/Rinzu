using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ai_manager : MonoBehaviour
{
    public static Ai_manager Instance { get; private set; }
    public GameObject bosspath;

    public GameObject Rewards;

    public GameObject Congrats;

    public Text Quest;

    public List<Enemy> Enemies = new();

    public List<EnemyBoss> EnemiesBoss = new();

    

    public int MaxEnemies;


    private void Awake()
    {
        Instance = this;
    }

    private void RemoveEnemy(Enemy enemy)
    {
        Enemies.Remove(enemy);
        MaxEnemies--;
        enemy.Die();
        if (MaxEnemies <= 0)
        {
            bosspath.SetActive(false);
            Rewards.SetActive(true);
            Quest.text = "Квест: Убейте Гоблина босса";
        }
    }

    private void RemoveBossEnemy(EnemyBoss enemyBoss)
    {
        EnemiesBoss.Remove(enemyBoss);
        enemyBoss.Die();
        if (EnemiesBoss.Count <= 0)
        {
            Congrats.SetActive(true);
        }
    }

    public void Register(Enemy enemy)
    {
        Enemies.Add(enemy);
        enemy.OnDie += RemoveEnemy;
    }

    public void Register2(EnemyBoss enemyBoss)
    {
        EnemiesBoss.Add(enemyBoss);
        enemyBoss.OnDie += RemoveBossEnemy;
    }
}
