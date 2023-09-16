using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using static System.MathF;

public class FogDisabler : MonoBehaviour
{
    public Tilemap fogTileMap;
    public Tilemap bordersTileMap;
    public Tile fogTile;
    public Tile borderTile;
    void Update()
    {
        HideFog();
    }
    [ContextMenu("HideFog")]
    public void HideFog()
    {
        Vector2 angleRT = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
        if (angleRT.x < 0)
            angleRT.x = (int)angleRT.x;
        else
            angleRT.x = (int)angleRT.x + 1;
        if (angleRT.y < 0)
            angleRT.y = (int)angleRT.y;
        else
            angleRT.y = (int)angleRT.y + 1;
        Vector2 angleLD = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        if (angleLD.x < 0)
            angleLD.x = (int)angleLD.x - 1;
        else
            angleLD.x = (int)angleLD.x - 0;
        if (angleLD.y < 0)
            angleLD.y = (int)angleLD.y - 1;
        else
            angleLD.y = (int)angleLD.y - 0;
        bool[,] borders = new bool[(int)Abs(angleRT.x - angleLD.x) + 1, (int)Abs(angleRT.y - angleLD.y) + 1];
        for (int y = (int)(angleLD.y); y <= angleRT.y; y++)
            for (int x = (int)(angleLD.x); x <= (int)(angleRT.x); x++)
            {
                int indexX = (int)(x - angleLD.x);
                int indexY = (int)(y - angleLD.y);
                if (bordersTileMap.GetTile(new Vector3Int(x, y, 0)) == borderTile)
                    borders[indexX, indexY] = true;
                else
                    borders[indexX, indexY] = false;
            }
        List<Vector2Int> cord = new List<Vector2Int>();
        bool[,] vision = new bool[(int)Abs(angleRT.x - angleLD.x) + 1, (int)Abs(angleRT.y - angleLD.y) + 1];
        vision[(int)Abs(angleRT.x - angleLD.x) / 2, (int)Abs(angleRT.y - angleLD.y) / 2] = true;
        cord.Add(new Vector2Int((int)Abs(angleRT.x - angleLD.x) / 2, (int)Abs(angleRT.y - angleLD.y) / 2));
        for(int i = 0; i < cord.Count; i++)
        {
            if (borders[cord[i].x, cord[i].y])
            {
                continue;
            }
            if (cord[i].x - 1 >= 0 && vision[cord[i].x - 1, cord[i].y] == false)
            {
                vision[cord[i].x - 1, cord[i].y] = true;
                cord.Add(new Vector2Int(cord[i].x - 1, cord[i].y));
            }
            if (cord[i].x + 1 < (int)Abs(angleRT.x - angleLD.x) && vision[cord[i].x + 1, cord[i].y] == false)
            {
                vision[cord[i].x + 1, cord[i].y] = true;
                cord.Add(new Vector2Int(cord[i].x + 1, cord[i].y));
            }
            if (cord[i].y - 1 >= 0 && vision[cord[i].x, cord[i].y - 1] == false)
            {
                vision[cord[i].x, cord[i].y - 1] = true;
                cord.Add(new Vector2Int(cord[i].x, cord[i].y - 1));
            }
            if (cord[i].y + 1 < (int)Abs(angleRT.y - angleLD.y) && vision[cord[i].x, cord[i].y + 1] == false)
            {
                vision[cord[i].x, cord[i].y + 1] = true;
                cord.Add(new Vector2Int(cord[i].x, cord[i].y + 1));
            }
        }
        for (int i = 0; i < cord.Count; i++)
        {
            if (borders[cord[i].x, cord[i].y])
            {
                continue;
            }
            if (cord[i].x - 1 >= 0 && vision[cord[i].x - 1, cord[i].y] == false)
            {
                vision[cord[i].x - 1, cord[i].y] = true;
            }
            if (cord[i].x + 1 < (int)Abs(angleRT.x - angleLD.x) && vision[cord[i].x + 1, cord[i].y] == false)
            {
                vision[cord[i].x + 1, cord[i].y] = true;
            }
            if (cord[i].y - 1 >= 0 && vision[cord[i].x, cord[i].y - 1] == false)
            {
                vision[cord[i].x, cord[i].y - 1] = true;
            }
            if (cord[i].y + 1 < (int)Abs(angleRT.y - angleLD.y) && vision[cord[i].x, cord[i].y + 1] == false)
            {
                vision[cord[i].x, cord[i].y + 1] = true;
            }
        }
        for (int y = (int)(angleLD.y); y <= angleRT.y; y++)
            for (int x = (int)(angleLD.x); x <= (int)(angleRT.x); x++)
            {
                int indexX = (int)(x - angleLD.x);
                int indexY = (int)(y - angleLD.y);
                if (vision[indexX, indexY])
                    fogTileMap.SetTile(new Vector3Int(x, y, 0), null);
                else
                    fogTileMap.SetTile(new Vector3Int(x, y, 0), fogTile);
            }
    }
}
