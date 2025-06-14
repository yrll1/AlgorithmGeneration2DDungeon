using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class WallGenerator
{
    public static void CreateWalls(HashSet<Vector2Int>floorPositions,TilemapVisualizer tilemapVisualizer)//由简单随机漫步生成调用,
    {
        var basicWallPositions = FindWallsInDirections(floorPositions,Direction2D.cardinalDirectionsList);
        foreach (var position in basicWallPositions)//遍历返回的墙体坐标
        {
            tilemapVisualizer.PaintSingleBasicWall(position);//调用瓦片地图可视化工具中的来绘制墙贴图

        }                                                                     
    }

    private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> directionList)//寻找墙体处于的方向
    {
     HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();//存储墙体位置
        foreach (var position in floorPositions)//遍历所有地转位置
        {
            foreach (var direction in directionList)//遍历方向列四个方向(上下左右)位置是
            {      //neighbour:邻居位置
               var neighbourPosition = position + direction;//得到另据位置
                if (floorPositions.Contains(neighbourPosition)==false)//判断邻居坐标是否存有地砖坐标(如果没有就是墙体坐标)
                {
                    wallPositions.Add(neighbourPosition);//将改坐标添加到墙体位置
                }
            }
        }
        return wallPositions;
    }
}
