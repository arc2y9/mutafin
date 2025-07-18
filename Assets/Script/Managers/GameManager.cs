using NUnit.Framework;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;
using Unity.Properties;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<GameObject> tilePrefabs;
    [SerializeField] GameObject tileParent;
    List<GameObject> tiles;
    [Header("Player Settings"), SerializeField]
    GameObject playerPrefab;
    [SerializeField] GameObject playerParent;
    [SerializeField] GameObject commonParent;
    [SerializeField] GameObject dynamite;

    int boardHeight = 40;
    int boardWidth = 50;

    float pace = 100f;

    GameObject player;

    void Start()
    {
        tiles = new List<GameObject>();

        CreateBoard();
        CreatePlayer(playerPrefab, playerParent);

        player.GetComponent<Player>().onDynamite += PlaceDynamite;
        MineableObject.OnMined += Log;
    }
    void Log(string s)
    {
        UnityEngine.Debug.Log(s + " has been digged");
    }
    void Update()
    {
        
        if (Input.GetMouseButton(1))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider != null)
            {
                UnityEngine.Debug.Log("Týklanan yerde bir obje var: " + hit.collider.name);
            }
            else
            {
                UnityEngine.Debug.Log("Týklanan yerde hiçbir obje yok.");
                PlaceBlock();
            }
        }
    }
    private void CreateBoard()
    {
        for(int i = 0; i < boardHeight; i++)
        {
            for(int j = 0; j < boardWidth; j++)
            {
                GameObject newTile = Instantiate(tilePrefabs[Random.Range(0,tilePrefabs.Count)], tileParent.transform);
                newTile.GetComponent<RectTransform>().anchoredPosition = new Vector2(-1800 + j * pace, 400 - i * pace);
                newTile.GetComponent<SpriteRenderer>().sortingOrder = (int)LayerManager.Mineables;
                tiles.Add(newTile);
            }
        }
    }

    private GameObject CreatePlayer(GameObject playerPrefab, GameObject playerParent)
    {
        player = Instantiate(playerPrefab, playerParent.transform);
        player.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 800);
        player.GetComponent<SpriteRenderer>().sortingOrder = (int)LayerManager.Players;
        return player;
    }

    private void PlaceDynamite()
    {
        GameObject obj = Instantiate(dynamite, commonParent.transform);

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            commonParent.GetComponent<RectTransform>(),
            Input.mousePosition,
            this.gameObject.GetComponent<Canvas>().renderMode == RenderMode.ScreenSpaceOverlay ? null : Camera.main,
            out localPoint
        );

        obj.GetComponent<RectTransform>().localPosition = localPoint;
        obj.GetComponent<Dynamite>().OnBlast += BlastArea;
        obj.GetComponent<SpriteRenderer>().sortingOrder = (int)LayerManager.Blastables;

        UnityEngine.Debug.Log("Dynamite placed at: " + localPoint);
    }
    private void BlastArea(Vector3 center, int range)
    {
        UnityEngine.Debug.Log("blasting the area");
        for (int i = tiles.Count - 1; i >= 0; i--)
        {
            var tile = tiles[i];
            if (Vector2.Distance(center, tile.GetComponent<RectTransform>().position) < range)
            {
                UnityEngine.Debug.Log(tile.name + " has been destroyed.");
                tiles.RemoveAt(i);
                Destroy(tile);
            }
        }
    }

    private void PlaceBlock()
    {
        GameObject newTile = Instantiate(tilePrefabs[0], commonParent.transform);

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            commonParent.GetComponent<RectTransform>(),
            Input.mousePosition,
            this.gameObject.GetComponent<Canvas>().renderMode == RenderMode.ScreenSpaceOverlay ? null : Camera.main,
            out localPoint
        );

        localPoint = new Vector3(pace * (int)(localPoint.x / pace),
                            pace * (int)(localPoint.y / pace), 0f);

        newTile.GetComponent<RectTransform>().localPosition = localPoint;
        tiles.Add(newTile);
    }
}
