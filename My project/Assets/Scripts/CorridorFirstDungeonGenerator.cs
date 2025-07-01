using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CorridorFirstDungeonGenerator : SimpleRandomWalkGenerator//此类先创建走廊,然后根据位置来生成地牢的地面和墙壁
{
    [SerializeField]
    private int corridorLength = 14, corridorCount = 5;//走廊长度和走廊数量
    [SerializeField]
    [Range(0.1F,1)]
    private float roomPercent = 0.8f;//控制房间生成的比列
  
    protected override void RunProceduralGeneration()//重写基类并调用第一走廊生成方法
    {
        CorridorFirstGeneration();
    } 

    private void CorridorFirstGeneration()
    {
      HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();//存储地砖位置信息
      HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();

        CreateCorridors(floorPositions, potentialRoomPositions);
        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions);

        floorPositions.UnionWith(roomPositions);

      
        tilemapVisualizer.PaintFloorTiles(floorPositions);//在Map上绘制走廊地转
        WallGenerator.CreateWalls(floorPositions,tilemapVisualizer);//根据floorPositions存储的坐标来生成地墙壁
    }

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPositions)//
    {
      HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
       int RoomCreateCount = Mathf.RoundToInt(potentialRoomPositions.Count*roomPercent);

        List<Vector2Int> roomToCreate = potentialRoomPositions.OrderBy(x => Guid.NewGuid()).Take(RoomCreateCount).ToList();
        //从 potentialRoomPositions 里随机挑选出 RoomCreateCount 个位置，把它们存储到 roomToCreate 列表中
        foreach (var roomPosition in roomToCreate)
        {
            var roomFloor = RunRandowWalk(randomWalkParameters,roomPosition);
            roomPositions.UnionWith(roomFloor);
        }
        return roomPositions;
    }

    private void CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions)//调用形参
    {
       var currentPosition = startPosition;//将初始坐标存储到当前坐标
       potentialRoomPositions.Add(currentPosition);
        for (int i = 0; i < corridorCount; i++)//递推遍历
        {
            var corridor = ProceduralGenerationAlgorithms.RandomWalkCorridor(currentPosition,corridorLength);//调用随机漫步
            currentPosition = corridor[corridor.Count-1];//记录走廊最后一位坐标位于当前坐标,方便随机遍历
            potentialRoomPositions.Add(currentPosition);
            floorPositions.UnionWith(corridor);//存储地转坐标,方便遍历
        }
    }
}
