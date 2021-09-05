using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    private GameObject player;
    void Start()
    {
        this.player = GameObject.FindWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if(col.gameObject.tag.Equals("Player"))
        {
            int rand = Mathf.RoundToInt(Random.Range(3f, 10f));
            this.player.GetComponent<Status>().addCoin(rand);

            Destroy(gameObject);
        }
    }
}
