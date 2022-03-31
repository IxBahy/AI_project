using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameScript : MonoBehaviour
{
    [SerializeField] private Transform EmptySpace = null;
    private Camera _camera;
    [SerializeField] private TileScript[] tiles;
    private int emptySpaceIndex = 8;
    private bool _isFinished;
    [SerializeField] private GameObject endPanel;


    void Start()
    {
        _camera = Camera.main;
        shuffle();
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit)
            {

                if (Vector2.Distance(a: EmptySpace.position, b: hit.transform.position) < 3.7)
                {
                    Vector2 lastEmptySpacePosition = EmptySpace.position;
                    TileScript thisTile = hit.transform.GetComponent<TileScript>();
                    EmptySpace.position = thisTile.targetPosition;
                    thisTile.targetPosition = lastEmptySpacePosition;
                    int tileIndex = FindIndex(thisTile);

                    tiles[emptySpaceIndex] = tiles[tileIndex];
                    tiles[tileIndex] = null;
                    emptySpaceIndex = tileIndex;
                }
            }
        }
        if (!_isFinished)
        {


            int correctTiles = 0;
            foreach (var item in tiles)
            {
                if (item != null)
                {
                    if (item.inRightPlace)
                    {
                        correctTiles++;
                    }
                }
            }
            if (correctTiles == tiles.Length - 1)
            {
                _isFinished = true;
                endPanel.SetActive(true);
            }
        }
    }
    public void PlayAgine()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void shuffle()
    {
        if (emptySpaceIndex != 8)
        {
            var tileOn8LastPos = tiles[8].targetPosition;
            tiles[15].targetPosition = EmptySpace.position;
            EmptySpace.position = tileOn8LastPos;
            tiles[emptySpaceIndex] = tiles[8];
            tiles[8] = null;
            emptySpaceIndex = 8;
        }
        int invertion;
        do
        {
            for (int i = 0; i < 8; i++)
            {
                var lastPos = tiles[i].targetPosition;
                int randomIndex = Random.Range(0, 7);
                tiles[i].targetPosition = tiles[randomIndex].targetPosition;
                tiles[randomIndex].targetPosition = lastPos;
                var tile = tiles[i];
                tiles[i] = tiles[randomIndex];
                tiles[randomIndex] = tile;

            }
            invertion = GetInversions();
            Debug.Log(message: "puzzle shuffled");
        } while (invertion % 2 != 0);
    }
    public int FindIndex(TileScript ts)
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i] != null)
            {
                if (tiles[i] == ts)
                {
                    return i;
                }
            }
        }
        return -1;
    }
    int GetInversions()
    {
        int inversionsSum = 0;
        for (int i = 0; i < tiles.Length; i++)
        {
            int thisTileInvertion = 0;
            for (int j = i; j < tiles.Length; j++)
            {
                if (tiles[j] != null)
                {
                    if (tiles[i].number > tiles[j].number)
                    {
                        thisTileInvertion++;
                    }
                }
            }
            inversionsSum += thisTileInvertion;
        }
        return inversionsSum;
    }
}

