using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] TextAsset mapText;
    [SerializeField] GameObject[] prefabs;

    public enum MAP_TYPE
    {
        GROUND, //0
        WALL, //1
        PLAYER, //2
        ACTION, //3
        GOAL //4
    }
    MAP_TYPE[,] mapTable;

    float mapSize;
    Vector2 centerPos;

    public Vector2Int FIRSTPLAYERPOSITION;

   

    void Start()
    {
        _loadMapData();

        _createMap();
    }

    void _loadMapData()
    {
        string[] mapLines = mapText.text.Split(new[] { "\n", "/r" }, System.StringSplitOptions.RemoveEmptyEntries);

        int row = mapLines.Length;
        int col = mapLines[0].Split(new char[] { ',' }).Length;
        mapTable = new MAP_TYPE[col, row];

        for(int y = 0; y < row; y++)
        {
            string[] mapValues = mapLines[y].Split(new char[] { ',' });
            for(int x = 0; x < col; x++)
            {
                mapTable[x, y] = (MAP_TYPE)int.Parse(mapValues[x]);
            }
        }

        //FOR DEBUG
        /*foreach(string mapLine in mapLines)
        {
            string[] mapValues = mapLine.Split(new char[] { ',' });
            foreach(string mapValue in mapValues)
            {
                Debug.Log(mapValue);
            }
        }*/
    }

    public MAP_TYPE GetNextMapType(Vector2Int _pos)
    {
        return mapTable[_pos.x, _pos.y];
    }

    void _createMap()
    {
        //Add size
        mapSize = prefabs[0].GetComponent<SpriteRenderer>().bounds.size.x;

        //c‰¡‚Ì”‚ğ”¼•ª‚É‚µ‚ÄmapSize‚ğŠ|‚¯‚é‚±‚Æ‚Å’†S‚ğ‹‚ß‚é

        //—ñ‚ª‹ô”‚Ìê‡
        if (mapTable.GetLength(0) % 2 == 0)
        {
            centerPos.x = mapTable.GetLength(0) / 2 * mapSize - (mapSize / 2);
        }
        else
        {
            centerPos.x = mapTable.GetLength(0) / 2 * mapSize;
        }
        //s‚ª‹ô”‚Ìê‡
        if (mapTable.GetLength(1) % 2 == 0)
        {
            centerPos.y = mapTable.GetLength(1) / 2 * mapSize - (mapSize / 2);
        }
        else
        {
            centerPos.y = mapTable.GetLength(1) / 2 * mapSize;
        }

        for (int y = 0;y < mapTable.GetLength(1); y++)
        {
            for(int x = 0; x < mapTable.GetLength(0); x++)
            {
                //Get now position 
                Vector2Int pos = new Vector2Int(x,y);
                GameObject _ground = Instantiate(prefabs[(int)MAP_TYPE.GROUND], transform);
                _ground.transform.position = new Vector2(x, y);


                GameObject _map = Instantiate(prefabs[(int)mapTable[x, y]],transform);
                _map.transform.position = new Vector2(x, y);

                _ground.transform.position = ScreenPos(pos);
                _map.transform.position = ScreenPos(pos);

                if (mapTable[x, y] == MAP_TYPE.PLAYER)
                {
                    FIRSTPLAYERPOSITION = pos;
                    _map.GetComponent<ControlPlayer>().currentPos = pos;
                }
            }
        }
    }

    public Vector2 ScreenPos(Vector2Int _pos)
    {
        return new Vector2(
            _pos.x * mapSize - centerPos.x,
            -(_pos.y * mapSize - centerPos.y));

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
