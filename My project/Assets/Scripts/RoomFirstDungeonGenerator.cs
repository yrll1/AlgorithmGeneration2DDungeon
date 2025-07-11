using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
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
        if(randomWalkRooms)
        {
            floor = CreateRoomsRandomly(roomList);
        }
        else
        {
        floor = CreatesSimpleRooms(roomList); //根据 roomList 生成简单的房间，并将房间内的地板位置存储在 floor 集合中。
        }
        
       
        List<Vector2Int> roomCenters = new List<Vector2Int>();
        foreach (var room in roomList)//遍历房间列表
        {
            roomCenters.Add((Vector2Int)Vector3Int.RoundToInt(room.center));//将room.center通过RoundToInt方法四舍五入为正数
                                                                            //然后通过显示类型转换获得X,Y
        }
        HashSet<Vector2Int> corridors = ConnectRooms(roomCenters);
        floor.UnionWith(corridors);
        tilemapVisualizer.PaintFloorTiles(floor);//将地板位置绘制到地图上。
        WallGenerator.CreateWalls(floor, tilemapVisualizer);//根据地板位置生成墙壁。

    }

    private HashSet<Vector2Int> CreateRoomsRandomly(List<BoundsInt> roomList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        for (int i = 0; i < roomList.Count; i++)
        {
            var roomBounds = roomList[i];
            var roomCenter = new Vector2Int(Mathf.RoundToInt(roomBounds.center.x),Mathf.RoundToInt(roomBounds.center.y));
            var roomFloor = RunRandowWalk(randomWalkParameters, roomCenter);
            foreach (var position in roomFloor)
            {
                if(position.x>=(roomBounds.xMin + offset)&& position.x <= (roomBounds.xMax-offset)&&position.y>=(roomBounds.yMin-offset) && position.y<=(roomBounds.yMax-offset))
                {
                    floor.Add(position);
                }
            }
        }
        return floor;
    }

    private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenters)//：将 roomCenters 列表中的所有房间中心点用走廊连接起来，最终返回一个包含所有走廊位置的 HashSet<Vector2Int>。
    {
       HashSet<Vector2Int> corridors = new HashSet<Vector2Int>();// 用于存储所有走廊的位置。
        var currentRoomCenter = roomCenters[UnityEngine.Random.Range(0, roomCenters.Count)];//随机选择一个房间中心点作为起始点 currentRoomCenter，
        roomCenters.Remove(currentRoomCenter); //并从 roomCenters 列表中移除该点。
        while (roomCenters.Count > 0)
        {
             Vector2Int closet = FindClosesPointTo(currentRoomCenter, roomCenters);//调用 FindClosesPointTo 方法找到距离 currentRoomCenter 最近的点 closet。
            roomCenters.Remove(closet);//从 roomCenters 列表中移除 closet。
            HashSet<Vector2Int> newCorridor = CreateCorridor(currentRoomCenter,closet);//调用 CreateCorridor 方法创建连接 currentRoomCenter 和 closet 的走廊 newCorridor。
            currentRoomCenter = closet;//更新 currentRoomCenter 为 closet。
            corridors.UnionWith(newCorridor);// newCorridor 中的位置合并到 corridors 中。
        }
       return corridors;
    }
    //创建连接两个房间中心点的走廊，返回一个包含走廊位置的 HashSet<Vector2Int>。
    private HashSet<Vector2Int> CreateCorridor(Vector2Int currentRoomCenter, Vector2Int destination)
    {
       HashSet<Vector2Int> corridor = new HashSet<Vector2Int>();//初始化一个 HashSet<Vector2Int> 类型的 corridor 用于存储走廊的位置。
        var position = currentRoomCenter;//将起始点 currentRoomCenter 添加到 corridor 中。
        corridor.Add(position);
        //第一个 while 循环：
        //如果目标点的 y 坐标大于当前点的 y 坐标，当前点向上移动一格（Vector2Int.up）。
        //如果目标点的 y 坐标小于当前点的 y 坐标，当前点向下移动一格（Vector2Int.down）。
        //将移动后的点添加到 corridor 中。
    while (position.y!= destination.y)
        {
            if (destination.y>position.y)
            {
                position += Vector2Int.up;
            }
            else if (destination.y<position.y)
            {
                position += Vector2Int.down;
            }
            corridor.Add(position);
        }
        //第二个 while 循环：
       // 如果目标点的 x 坐标大于当前点的 x 坐标，当前点向右移动一格（Vector2Int.right）。
     //如果目标点的 x 坐标小于当前点的 x 坐标，当前点向左移动一格（Vector2Int.left）。
    //将移动后的点添加到 corridor 中。
        while (position.x!=destination.x)

        {
            if (destination.x>position.x)
            {
                position += Vector2Int.right;
            }
            else if (destination.x < position.x)
            {
                position += Vector2Int.left;
            }
            corridor.Add(position);
        }
        return corridor;
    }

    private Vector2Int FindClosesPointTo(Vector2Int currentRoomCenter, List<Vector2Int> roomCenters)
    {
       // 在 roomCenters 列表中找到距离 currentRoomCenter 最近的点，返回该点的 Vector2Int 坐标。
        Vector2Int closest = Vector2Int.zero;//初始化 closest 为 Vector2Int.zero，
        float distance = float.MaxValue;//distance 为 float.MaxValue。
        foreach (var position in roomCenters)// roomCenters 列表中的每个点：
        {//计算当前点与 currentRoomCenter 之间的距离 currentDistance。
          // 如果 currentDistance 小于 distance，更新 distance 为 currentDistance，并将 closest 更新为当前点。
            float currentDistance = Vector2.Distance(position,currentRoomCenter);
            if (currentDistance < distance)
            {//如果 currentDistance 小于 distance，更新 distance 为 currentDistance，并将 closest 更新为当前点。
                distance = currentDistance;
                closest = position;
             
            }
        }
        return closest;
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
