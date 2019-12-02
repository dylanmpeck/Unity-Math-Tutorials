using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CreateBoard : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public GameObject housePrefab;
    public GameObject treePrefab;
    GameObject[] tiles;
    public Text score;
    long dirtBB = 0;
    long desertBB = 0;
    long treeBB = 0;
    long playerBB = 0;

    const int HEIGHT = 8;
    const int WIDTH = 8;

    // Start is called before the first frame update
    void Start()
    {
        tiles = new GameObject[64];
        for (int row = 0; row < HEIGHT; row++)
        {
            for (int col = 0; col < WIDTH; col++)
            {
                int randomTile = UnityEngine.Random.Range(0, tilePrefabs.Length);
                Vector3 pos = new Vector3(col, 0, row);
                GameObject tile = Instantiate(tilePrefabs[randomTile], pos, Quaternion.identity);
                tile.name = tile.tag + "_" + row + "_" + col;
                tiles[row * 8 + col] = tile;
                if (tile.tag == "Dirt")
                {
                    dirtBB = SetCellState(dirtBB, row, col);
                    //PrintBB("Dirt", dirtBB);
                }
                if (tile.tag == "Desert")
                    desertBB = SetCellState(desertBB, row, col);
            }
        }
        //Debug.Log("Dirt cells = " + CellCount(dirtBB));
        InvokeRepeating("PlantTree", 1, 1);
    }

    void PlantTree()
    {
        int randomRow = UnityEngine.Random.Range(0, HEIGHT);
        int randomCol = UnityEngine.Random.Range(0, WIDTH);
        if (GetCellState(dirtBB & ~playerBB, randomRow, randomCol))
        {
            GameObject tree = Instantiate(treePrefab);
            tree.transform.parent = tiles[randomRow * 8 + randomCol].transform;
            tree.transform.localPosition = Vector3.zero;
            treeBB = SetCellState(treeBB, randomRow, randomCol);
        }

    }

    void PrintBB(string name, long BB)
    {
        Debug.Log(name + ": " + Convert.ToString(BB, 2).PadLeft(64, '0'));
    }

    long SetCellState(long bitboard, int row, int col)
    {
        long newBit = 1L << (row * WIDTH + col);
        return (bitboard |= newBit);
    }

    bool GetCellState(long bitboard, int row, int col)
    {
        long mask = 1L << (row * WIDTH + col);
        return ((mask & bitboard) != 0);
    }

    int CellCount(long bitboard)
    {
        int count = 0;
        long bb = bitboard;
        while (bb != 0)
        {
            bb &= bb - 1;
            count++;
        }
        return (count);
    }

    void CalculateScore()
    {
        score.text = "Score: " + (CellCount(playerBB & dirtBB) * 10 + CellCount(playerBB & desertBB) * 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                int row = (int)hit.collider.gameObject.transform.position.z;
                int col = (int)hit.collider.gameObject.transform.position.x;
                if (GetCellState((dirtBB & ~treeBB) | desertBB, row, col))
                {
                    GameObject house = Instantiate(housePrefab);
                    house.transform.parent = hit.collider.gameObject.transform;
                    house.transform.localPosition = Vector3.zero;
                    playerBB = SetCellState(playerBB, row, col);
                    CalculateScore();
                }
            }
        }
    }
}
