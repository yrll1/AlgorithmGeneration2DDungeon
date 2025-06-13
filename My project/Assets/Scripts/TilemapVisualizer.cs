using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTilemap;
    [SerializeField]
    private TileBase floorTile;
    
    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)//允许外部调用的公共方法绘制地板瓦片(IEnumerable<>泛型接口,它定义了一个可以被迭代的集合,实现这个集合的类可以循环遍历其元素)
    {
        PaintTiles(floorPositions, floorTilemap, floorTile);//此处floorPositions以存储设置的所有漫游步数坐标
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)////方法接收位置集合、瓦片地图引用和瓦片类型，遍历每个位置并调用PaintSingleTile。
    {
     foreach (var position in positions)//循环遍历集合Positions每个XY坐标
        {
          PaintSingleTile(tilemap,tile,position);
        }
    }

    private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)// PaintSingleTile 方法将 2D 整数坐标转换为瓦片地图中的单元格坐标，然后在该位置设置指定的瓦片。
    {                                                            //这里不使用IEnumerable接口是因为传进来的只是一个坐标
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);//将世界坐标转换到2维瓦片地图坐标,并使用显示类型转换
        tilemap.SetTile(tilePosition,tile);//在瓦片地图中绘制,第一个参数是2维坐标,第2个参数就是要绘制的瓦片
    }
    public void Clear()
    {
        floorTilemap.ClearAllTiles();//清空当前绘制的瓦片地图
    }
}
