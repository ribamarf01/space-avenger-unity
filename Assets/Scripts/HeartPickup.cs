using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPickup : MonoBehaviour
{
    private GameObject player;
    void Start()
    {
        this.player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if(col.gameObject.tag.Equals("Player"))
        { // Se o player colide com o objeto, acontece um efeito nos status
            int rand = Mathf.RoundToInt(Random.Range(2f, 5f));
            this.player.GetComponent<Status>().heal(rand);

            Destroy(gameObject);
        }
    }
}
