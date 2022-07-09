using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _width;
    [SerializeField] private int _height;
    [SerializeField] private Node _nodePrefab;
    [SerializeField] private GameObject _boardPrefab;
    [SerializeField] private GameObject _blockPrefab;
    [SerializeField] private List<BlockType> _blockTypeList;

    private List<Node> _nodeList;

    private BlockType GetBlockTypeByValue(int value) {
        return _blockTypeList.First(t => t.value == value);
    }

    void Start() {
        _nodeList = new List<Node>();
        GenerateGrid();
    }

    void GenerateGrid()
    {
        Vector2 center = new Vector2((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f);
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _width; y++)
            {
                //Instantiate Floor Nodes
                var node = Instantiate(_nodePrefab, new Vector2(x,y), Quaternion.identity);
                _nodeList.Add(node);
            }
        }

        //Instantiate Floor
        var board = Instantiate(_boardPrefab, center, Quaternion.identity);

        Camera.main.transform.position = new Vector3(center.x, center.y, -10);
        // Also, generate 2 blocks on the floor as the start point

        List<Node> emptyNodes = _nodeList.FindAll(x => !x.hasBlock).OrderBy(x => Random.value).Take(2).ToList();
        foreach(Node node in emptyNodes)
        {
            var block = Instantiate(_blockPrefab, node.transform.position, Quaternion.identity);
            //why assigning color value from list makes its alpha value 0??????????
            var a = block.GetComponent<SpriteRenderer>().color.a;
            var col = GetBlockTypeByValue(2).color;
            col.a = a;
            block.GetComponent<SpriteRenderer>().color = col;
            block.transform.GetChild(0).GetComponent<TextMeshPro>().text = GetBlockTypeByValue(2).value.ToString();
        }
    }

}

[Serializable]
public struct BlockType
{
    public int value;
    public Color color;
}


