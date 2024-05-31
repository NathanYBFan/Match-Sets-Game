using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public sealed class Board : MonoBehaviour
{
    public static Board _Instance { get; private set; }

    public Row[] rows;

    public Tile[,] tiles { get; private set; }

    public int width => tiles.GetLength(0);
    public int height => tiles.GetLength(1);


    private readonly List<Tile> _selection = new List<Tile>();

    private const float tweenDuration = 0.25f;

    private void Awake() => _Instance = this;

    private void Start()
    {
        tiles = new Tile[rows.Max(row => row.tiles.Length), rows.Length];

        for (int x = 0; x < height; x++)
        {
            for (int y = 0; y < width; y++)
            {
                var tile = rows[y].tiles[x];

                tile.x = x;
                tile.y = y;
                
                tile.Item = GetRandomItem();

                tiles[x, y] = tile;
            }
        }

        Pop();
    }

    public async void Select(Tile tile)
    {
        if (!_selection.Contains(tile))
        {
            if (_selection.Count > 0 && Array.IndexOf(_selection[0].neighbours, tile) != -1)
                _selection.Add(tile);
            else
                _selection.Add(tile);
        }
        else
            _selection.Clear();

        if (_selection.Count < 2) return;


        //Debug.Log($"Selected tiles at ({_selection[0].x}, {_selection[0].y}), and ({_selection[1].x}, {_selection[1].y})");

        await Swap(_selection[0], _selection[1]);

        if (CanPop())
            Pop();
        else
            await Swap(_selection[0], _selection[1]);

        _selection.Clear();
    }

    public async Task Swap(Tile tileA, Tile tileB)
    {
        var iconA = tileA.icon;
        var iconB = tileB.icon;

        var iconATransform = iconA.transform;
        var iconBTransform = iconB.transform;

        var sequnce = DOTween.Sequence();

        // Animations to play
        sequnce.Join(iconATransform.DOMove(iconBTransform.position, tweenDuration))
               .Join(iconBTransform.DOMove(iconATransform.position, tweenDuration));

        await sequnce.Play().AsyncWaitForCompletion();

        // Reset parents
        iconATransform.SetParent(tileB.transform);
        iconBTransform.SetParent(tileA.transform);

        // Swap icons
        tileA.icon = iconB;
        tileB.icon = iconA;

        // Swap the items
        var tileAItem = tileA.Item;

        tileA.Item = tileB.Item;
        tileB.Item = tileAItem;
    }

    private bool CanPop()
    {
        for (var y = 0; y < height; y++)
            for (var x = 0; x < width; x++)
                if (tiles[x, y].GetConnectedTiles().Skip(1).Count() >= 2) return true;
        return false;
    }

    private async void Pop()
    {
        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                var tile = tiles[x, y];
                var connectedTiles = tile.GetConnectedTiles();
                var deflateSequence = DOTween.Sequence();

                // If no connections long enough, skip
                if (connectedTiles.Skip(1).Count() < 2) continue;

                AudioManager._Instance.PlayGameAudioClip(0);

                // Setup animations for all connected pieces that can pop
                foreach (var connectedTile in connectedTiles) deflateSequence.Join(connectedTile.icon.transform.DOScale(Vector3.zero, tweenDuration));

                // Display score
                Score._Instance.totalScore += tile.Item.value * connectedTiles.Count;

                // Play zoom out of icons
                await deflateSequence.Play().AsyncWaitForCompletion();

                AudioManager._Instance.PlayGameAudioClip(1);

                var inflateSequence = DOTween.Sequence();

                foreach (var connectedTile in connectedTiles)
                {
                    connectedTile.Item = GetRandomItem();

                    // Setup zoom in of icons
                    inflateSequence.Join(connectedTile.icon.transform.DOScale(Vector3.one, tweenDuration));
                }

                // Play zoom in of icons
                await inflateSequence.Play().AsyncWaitForCompletion();

                // Reset loop if a pop is found
                x = 0; y = 0;
            }
        }
    }

    private Item GetRandomItem()
    {
        return ItemDatabase.Items[UnityEngine.Random.Range(0, ItemDatabase.Items.Length)];
    }
}
