using NUnit.Framework;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;
using TMPro;
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
    [SerializeField] GameObject Bomb;
    int boardHeight = 40;
    int boardWidth = 50;

    int remainingUse = 10;

    float pace = 100f;


    GameObject player;
    [Header("UI")]
    [SerializeField] TextMeshProUGUI distance;
    [SerializeField] TextMeshProUGUI remaningUseText;
    void Start()
    {
        
        tiles = new List<GameObject>();

        CreateBoard();
        CreatePlayer(playerPrefab, playerParent);

        player.GetComponent<Player>().onDynamite += PlaceDynamite;
        MineableObject.OnMined += CreateCollectable;

        remaningUseText.text = "Remaning usage : " + remainingUse;

    }
    
    void Update()
    {
        
        if (Input.GetMouseButtonDown(1))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider == null)
            {
                PlaceBlock();
            }
        }

        if (Input.GetKeyDown(KeyCode.Q) && remainingUse > 0 && Bomb != null)
        {
            remainingUse--;
            remaningUseText.text = "Remaning usage : " + remainingUse;
            distance.text = "Distance : " + (int)Vector2.Distance(player.GetComponent<RectTransform>().position,
                Bomb.GetComponent<RectTransform>().position);
        }
        else if(Bomb == null)
        {
            remaningUseText.text = "Bomb has been eliminated.";
        }
        else if(remainingUse <= 0)
        {
            remaningUseText.text = "Your scanner has no battery.";
        } 
    }
    GameObject CreateCollectable(GameObject minedObj) // performansý düþürüyo
    {
        UnityEngine.Debug.Log("collectable created");
        minedObj.GetComponent<RectTransform>().localScale = new Vector3(minedObj.GetComponent<RectTransform>().localScale.x * 0.1f,
                                                                        minedObj.GetComponent<RectTransform>().localScale.y * 0.1f,
                                                                        minedObj.GetComponent<RectTransform>().localScale.z * 0.1f);
        return minedObj;
    }
    private void CreateBoard()
    {
        int a = Random.Range(0, boardHeight);
        int b = Random.Range(0, boardWidth);

        for(int i = 0; i < boardHeight; i++)
        {
            for(int j = 0; j < boardWidth; j++)
            {
                GameObject newTile = Instantiate(tilePrefabs[Random.Range(0,tilePrefabs.Count)], tileParent.transform);
                newTile.GetComponent<RectTransform>().anchoredPosition = new Vector2(-1800 + j * pace, 400 - i * pace);
                newTile.GetComponent<SpriteRenderer>().sortingOrder = (int)LayerManager.Mineables;
                tiles.Add(newTile);
                if(a == i && b == j)
                {
                    GameObject bomb = Instantiate(Bomb, commonParent.transform);
                    bomb.GetComponent<RectTransform>().position = tileParent.GetComponent<RectTransform>().position;
                    bomb.GetComponent<SpriteRenderer>().sortingOrder = (int)LayerManager.Bomb;
                }
                
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
    private void BlastArea(GameObject blaster)
    {
        int range = blaster.GetComponent<BlastableObject>().range;
        Vector2 center = blaster.GetComponent<RectTransform>().position;
        UnityEngine.Debug.Log("blasting the area");
        for (int i = tiles.Count - 1; i >= 0; i--)
        {
            var tile = tiles[i];
            if (tile != null && tile.GetComponent<RectTransform>() != null &&
                Vector2.Distance(center, tile.GetComponent<RectTransform>().position) < range)
            {
                GameObject newObj = CreateCollectable(tile.gameObject);
                newObj.GetComponent<MineableObject>().Blow(center, range);
                newObj.GetComponent<Collider2D>().isTrigger = true;
                UnityEngine.Debug.Log(tile.name + " has been destroyed.");
                tiles.RemoveAt(i);
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
