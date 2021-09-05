using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{

    private GameObject status;
    public Transform[] spawners;
    public GameObject[] enemies;
    int EnemyNumberBase = 20;
    int EnemyNumber;
    int EnemyAlive;
    int turn;
    bool battle;

    public void StartTurn()
    {
        if(!battle)
        {
            this.EnemyNumber = this.EnemyNumberBase + (turn * 5);
            this.battle = true;
            if(this.EnemyNumber > 100)
                this.EnemyNumber = 100;

            status.GetComponent<Status>().setMonsterNumber(this.EnemyNumber);
            status.GetComponent<Status>().SetTurnPart(battle);
            StartCoroutine(TurnStartedHint());
        }
    }

    void EndTurn()
    {
        this.turn++;
        status.GetComponent<Status>().SetTurn(turn + 1);
        status.GetComponent<Status>().healMax();
        status.GetComponent<Status>().SetTurnPart(battle);
        StartCoroutine(TurnEndedHint());
    }

    void Start()
    {
        this.battle = false;
        this.turn = 0;
        this.EnemyAlive = 0;
        this.status = GameObject.FindWithTag("Player");
        status.GetComponent<Status>().SetTurn(turn + 1);
        status.GetComponent<Status>().SetTurnPart(battle);
    }

    // Update is called once per frame
    void Update()
    {
        if(battle) {
            if(this.EnemyAlive < 10 && this.EnemyAlive < this.EnemyNumber)
            {
                this.EnemyAlive++;
                spawnEnemy();
                StartCoroutine(RespawnTimer());
            }
            if(this.EnemyNumber == 0)
            {
                battle = false;
                status.GetComponent<Status>().restoreBandAids();
                EndTurn();
            }
        }
    }

    void spawnEnemy()
    {
        int rand = Mathf.RoundToInt(Random.Range(0f, spawners.Length-1));
        Vector3 spawnPoint = spawners[rand].transform.position;
        
        rand = Mathf.RoundToInt(Random.Range(0f, enemies.Length-1));
        GameObject enemy = enemies[rand];
        
        spawnPoint.z = 0f;
        Instantiate(enemy, spawnPoint, Quaternion.identity);
    }

    public void decreaseEnemiesToKill()
    {
        this.EnemyAlive--;
        this.EnemyNumber--;
    }

    public bool battleHappening()
    {
        return this.battle;
    }

    IEnumerator TurnStartedHint()
    {
        this.gameObject.GetComponent<HintText>().SetHintText("Os monstros começaram o ataque!!!");
        yield return new WaitForSeconds(4);
        this.gameObject.GetComponent<HintText>().Dispose();
    }

    IEnumerator TurnEndedHint()
    {
        this.gameObject.GetComponent<HintText>().SetHintText("Os monstros cansaram de atacar!");
        yield return new WaitForSeconds(4);
        this.gameObject.GetComponent<HintText>().Dispose();
    }

    IEnumerator RespawnTimer()
    {
        yield return new WaitForSeconds(2);
    }
}
