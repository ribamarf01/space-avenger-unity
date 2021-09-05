using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Status : MonoBehaviour
{
    private GameObject system;
    public Text HealthPointsText;
    public Text RifleAmmoText;
    public Text KilledMonstersText;
    public Text TotalKilledMonstersText;
    public Text coinsText;
    public Text BandAidText;
    public Text TurnText;
    public Text TurnPartText;
    public Text stakeText;
    int HealthPoints;
    int RifleAmmo;
    int killedMonsters;
    int totalKilledMonsters;
    int coins;
    int bandAids; // Essa variavel ficou com um nome bem infeliz
    int stake;
    
    // Como o hp aumenta, deve-se aumentar esse valor para impedir overflow de hp
    int maxHealth;

    void Start() {
        // Variaveis do personagem
        this.maxHealth = 100;
        this.HealthPoints = this.maxHealth;
        this.RifleAmmo = 100;
        this.killedMonsters = 0;
        this.totalKilledMonsters = 0;
        this.coins = 0;
        this.bandAids = 3;
        this.stake = 3;
        // Textos
        this.HealthPointsText.text = this.HealthPoints.ToString();
        this.RifleAmmoText.text = this.RifleAmmo.ToString();
        this.KilledMonstersText.text = "";
        this.TotalKilledMonstersText.text = "Abates Totais: " + this.totalKilledMonsters.ToString();
        this.coinsText.text = this.coins.ToString();
        this.BandAidText.text = this.bandAids.ToString();
        this.TurnText.text = "Turno: 1";
        this.TurnPartText.text = "Estratégia";
        this.stakeText.text = this.stake.ToString();

        // Sistema do jogo
        this.system = GameObject.FindWithTag("Respawn");
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Alpha4))
            useBandAid();
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("cl");
            this.system.GetComponent<UpgradePanel>().ClosePanel();
        }
    }

    public void loseHealth(int healthLose) {
        this.HealthPoints -= healthLose;

        if(this.HealthPoints > 0) {
            this.HealthPointsText.text = HealthPoints.ToString();
        } else {
            Death();
        }

    }
    public void heal(int amount)
    {
        this.HealthPoints += amount;
        if(this.HealthPoints > this.maxHealth)
            this.HealthPoints = this.maxHealth;
        this.HealthPointsText.text = this.HealthPoints.ToString();
    }

    public void healMax()
    {
        this.HealthPoints = this.maxHealth;
        this.HealthPointsText.text = this.HealthPoints.ToString();
    }

    public void upgradeMaxHealth()
    {
        if(this.coins >= 50)
        {
            this.maxHealth += 10;
            this.healMax();
            removeCoin(50);
        }
    }

    public void buyAmmoRifle()
    {
        if(this.coins >= 5)
        {
            this.RifleAmmo += 10;
            this.RifleAmmoText.text = this.RifleAmmo.ToString();
            removeCoin(5);
        }
    }

    public bool ShootRifle()
    {
        if(this.RifleAmmo > 0 && this.system.GetComponent<GameSystem>().battleHappening()) 
        {
            this.RifleAmmo--;
            this.RifleAmmoText.text = this.RifleAmmo.ToString();
            return true;
        } else return false;

    }
    public void addAmmo(int amount)
    {
        this.RifleAmmo += amount;
        this.RifleAmmoText.text = this.RifleAmmo.ToString();
    }
    public void killMonster()
    {
        totalKilled();
        setMonsterNumber(--this.killedMonsters);
        this.system.GetComponent<GameSystem>().decreaseEnemiesToKill();
    }
    public void setMonsterNumber(int monster)
    {
        this.killedMonsters = monster;
        this.KilledMonstersText.text = "Restam: " + this.killedMonsters.ToString();
    }
    public void totalKilled()
    {
        this.totalKilledMonsters++;
        this.TotalKilledMonstersText.text = "Abates Totais: " + this.totalKilledMonsters.ToString();
    }

    public void addCoin(int amount)
    {
        this.coins += amount;
        this.coinsText.text = this.coins.ToString();
    }

    public void removeCoin(int amount)
    {
        this.coins -= amount;
        this.coinsText.text = this.coins.ToString();
    }

    public void useBandAid()
    {
        if(this.bandAids > 0)
        {
            this.bandAids--;
            this.BandAidText.text = this.bandAids.ToString();

            int healAmount = Mathf.RoundToInt(this.maxHealth * Mathf.RoundToInt(Random.Range(30f, 50f))/100);

            heal(healAmount);
        }
    }

    public void restoreBandAids()
    {
        this.bandAids = 3;
        this.BandAidText.text = this.bandAids.ToString();
    }

    public void SetTurn(int turn)
    {
        this.TurnText.text = "Turno: " + turn;
    }

    public void SetTurnPart(bool battle)
    {
        if(battle)
            this.TurnPartText.text = "Defesa";
        else
            this.TurnPartText.text = "Estratégia";
    }

    public bool putStake()
    {
        if(this.stake > 0)
        {
            this.stake--;
            this.stakeText.text = this.stake.ToString();
            return true;
        } else return false;
    }

    public void removeStake()
    {
        this.stake++;
        this.stakeText.text = this.stake.ToString();
    }

    public void buyStake()
    {
        if(this.coins >= 30)
        {
            this.stake++;
            this.stakeText.text = this.stake.ToString();
            this.removeCoin(30);
        }
    }

    public void Death() {
        SceneManager.LoadScene("DeathScene");
    }
}