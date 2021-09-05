using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class ObstacleTilemapControl : MonoBehaviour
{
    
    private Grid grid;
    public Tilemap obstacleMap;
    public Tile obstacle;
    private GameObject status;
    private GameObject system;

    private void Start() {
        grid = gameObject.GetComponent<Grid>();
        status = GameObject.FindWithTag("Player");
        system = GameObject.FindWithTag("Respawn");
    }
    
    private void Update() {
        Vector3Int mousePos = GetMousePos();

        if(Input.GetKeyDown(KeyCode.Mouse1) && this.obstacleMap.GetTile(mousePos) == null && !this.system.GetComponent<GameSystem>().battleHappening() && this.status.GetComponent<Status>().putStake())
            obstacleMap.SetTile(mousePos, obstacle);

        if(Input.GetKeyDown(KeyCode.Mouse2) && this.obstacleMap.GetTile(mousePos) != null && !this.system.GetComponent<GameSystem>().battleHappening())
        {
            this.status.GetComponent<Status>().removeStake();
            obstacleMap.SetTile(mousePos, null);
        }

    }

    Vector3Int GetMousePos() {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return grid.WorldToCell(mouseWorldPos);
    }
}
