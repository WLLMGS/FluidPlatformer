using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GridCell
{
    Empty = 0,
    Wall = 1,
    Spike = 2,
    Finish = 3,
    PlayerSpawn = 4
}

public class MapReader : MonoBehaviour
{
    [SerializeField] private Texture2D _mapFile;
    [SerializeField] private GameObject _wallTile;
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject _finishFlag;
    [SerializeField] private GameObject _spikes;
    [SerializeField] private GameObject _bgTile;

    private float _bgZ = 10;
    private float _wallZ = 1;
    private float _playerZ = 0;
    private float _spikeZ = 1;
    private float _flagZ = 1;


    private GridCell[,] _grid;

    private void Start()
    {
        CalculateGrid();
        SpawnMap();
    }

    private void CalculateGrid()
    {

        _grid = new GridCell[_mapFile.width,_mapFile.height];

        for (int x = 0; x < _mapFile.width; ++x)
        {
            for (int y = 0; y < _mapFile.height; ++y)
            {
                Color32 c = _mapFile.GetPixel(x, y);

                if (c == Color.black)
                {
                    //add wall
                    //Instantiate(_wallTile, new Vector3(x, y, 0), Quaternion.identity);

                    _grid[x, y] = GridCell.Wall;
                }
                else if (c == Color.red)
                {
                    //add player start
                    //Instantiate(_playerPrefab, new Vector3(x, y, 0), Quaternion.identity);
                    //Instantiate(_bgTile, new Vector3(x, y, 5), Quaternion.identity);

                    _grid[x, y] = GridCell.PlayerSpawn;
                }
                else if (c == Color.green)
                {
                    //Instantiate(_finishFlag, new Vector3(x, y, 0), Quaternion.identity);
                    //Instantiate(_bgTile, new Vector3(x, y, 5), Quaternion.identity);

                    _grid[x, y] = GridCell.Finish;
                }
                else if (c == Color.blue)
                {
                    //Instantiate(_spikes, new Vector3(x, y, 0), Quaternion.identity);
                    //Instantiate(_bgTile, new Vector3(x, y, 5), Quaternion.identity);

                    _grid[x, y] = GridCell.Spike;
                }
                else if (c == Color.white)
                {
                    //Instantiate(_bgTile, new Vector3(x, y, 5), Quaternion.identity);
                    _grid[x, y] = GridCell.Empty;
                }
            }
        }
    }

    private void SpawnMap()
    {
        for (int x = 0; x < _mapFile.width; ++x)
        {
            for (int y = 0; y < _mapFile.height; ++y)
            {
                GridCell cell = _grid[x, y];

                switch (cell)
                {
                    case GridCell.Empty:
                        Instantiate(_bgTile, new Vector3(x, y, _bgZ), Quaternion.identity);
                        break;
                    case GridCell.Wall:
                        Instantiate(_wallTile, new Vector3(x, y, _wallZ), Quaternion.identity);
                        break;
                    case GridCell.Spike:
                        float rot = CalculateSpikeRotation(x, y);
                        Instantiate(_spikes, new Vector3(x, y, _spikeZ), Quaternion.Euler(0,0,rot));
                        Instantiate(_bgTile, new Vector3(x, y, _bgZ), Quaternion.identity);
                        break;
                    case GridCell.Finish:
                        Instantiate(_finishFlag, new Vector3(x, y, _flagZ), Quaternion.identity);
                        Instantiate(_bgTile, new Vector3(x, y, _bgZ), Quaternion.identity);
                        break;
                    case GridCell.PlayerSpawn:
                        Instantiate(_playerPrefab, new Vector3(x, y, _playerZ), Quaternion.identity);
                        Instantiate(_bgTile, new Vector3(x, y, _bgZ), Quaternion.identity);
                        break;
                }
            }
        }
    }

    private float CalculateSpikeRotation(int x, int y)
    {
        float rot = 0;

        if (_grid[x + 1, y] == GridCell.Wall)
        {
            rot = 90.0f;
        }
        else if (_grid[x-1,y] == GridCell.Wall)
        {
            rot = 270.0f;
        }
        else if (_grid[x , y + 1] == GridCell.Wall)
        {
            rot = 180.0f;
        }
        else if(_grid[x, y - 1] == GridCell.Wall)
        {
            rot = 0.0f;
        }

        return rot;
    }
}
