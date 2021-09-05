using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{

    private GameObject system;
    private void Start() {
        this.system = GameObject.FindWithTag("Respawn");
    }

    // Colisão com os cactos do cenário
    void OnTriggerEnter2D(Collider2D col) {
        if(col.gameObject.tag.Equals("Cactus"))
            this.gameObject.GetComponent<Status>().loseHealth(5);
        
    }

    // Colisão com o Modulo Espacial para Bob dormir e começar o turno de ataque
    void OnTriggerStay2D(Collider2D col) {
        if(col.gameObject.tag.Equals("SpaceModule") && !this.system.GetComponent<GameSystem>().battleHappening())
        {
            this.system.GetComponent<HintText>().SetHintText("Pressione espaço para iniciar o turno");
            if(Input.GetKeyDown(KeyCode.Space))
                this.system.GetComponent<GameSystem>().StartTurn();
        }

        if(col.gameObject.tag.Equals("Cactus"))
            this.system.GetComponent<HintText>().SetHintText("Isso está matando meus pés!");

        if(col.gameObject.tag.Equals("UpgradeModule") && !this.system.GetComponent<GameSystem>().battleHappening())
        {
            this.system.GetComponent<HintText>().SetHintText("Pressione espaço para fazer upgrades");
            if(Input.GetKeyDown(KeyCode.Space))
                this.system.GetComponent<UpgradePanel>().OpenPanel();
        }
        
    }

    private void OnTriggerExit2D(Collider2D col) {
        this.system.GetComponent<HintText>().Dispose();
    }
}
