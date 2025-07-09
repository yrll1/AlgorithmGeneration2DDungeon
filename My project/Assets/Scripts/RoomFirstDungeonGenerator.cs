using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomFirstDungeonGenerator : SimpleRandomWalkGenerator
{
    [SerializeField]
    private int minRoomWidth = 4, minRoomHeight = 4;//定义了生成房间的最小宽度和高度。
    [SerializeField]
    private int dungeonWidth = 20, dungeonHeight = 20;//定义了地下城的整体宽度和高度。
    [SerializeField]
    [Range(0, 10)]
    private int offset = 1;//于控制房间内部边界的偏移量，通过 [Range(0, 10)] 限制其取值范围在 0 到 10 之间。
    [SerializeField]
    private bool randomWalkRooms = false;//个布尔值，用于决定是否使用随机游走算法来生成房间，但在当前代码中未使用该变量。
    protected override void RunProceduralGeneration()
    {
        CreatRooms();
    }

    private void CreatRooms()
    {//使用二分空间分割算法将指定区域分割成多个房间，并将结果存储在 roomList 中。
        var roomList = ProceduralGenerationAlgorithms.BinarySpacePartitioning(new BoundsInt((Vector3Int)startPosition,new Vector3Int(dungeonWidth,dungeonHeight,0)),minRoomWidth,minRoomHeight);

        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();//用于存储地板的位置。
        floor = CreatesSimpleRooms(roomList);//根据 roomList 生成简单的房间，并将房间内的地板位置存储在 floor 集合中。
        tilemapVisualizer.PaintFloorTiles(floor);//将地板位置绘制到地图上。
        WallGenerator.CreateWalls(floor, tilemapVisualizer);//根据地板位置生成墙壁。

    }

    private HashSet<Vector2Int> CreatesSimpleRooms(List<BoundsInt> roomList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();

        foreach (var room in roomList)//遍历 roomList 中的每个房间。
        {
            for (int col = offset; col<room.size.x - offset; col++)/// 外层循环，控制列的遍历
            {
                for (int row = offset; row < room.size.y-offset; row++)//// 内层循环，控制行的遍历
                {   // 计算当前位置的坐标
                    // (Vector2Int)room.min 是房间的最小坐标，即房间左下角的坐标
                    // new Vector2Int(col, row) 是相对于房间最小坐标的偏移量
                    // 两者相加得到当前位置的实际坐标
                    Vector2Int position = (Vector2Int)room.min+new Vector2Int(col,row);
                    floor.Add(position);//// 将当前位置添加到 floor 集合中
                }
            }
        }
        return floor;


    }
}
