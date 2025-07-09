using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public static class ProceduralGenerationAlgorithms 
{
   
    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition,int walkLength)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();
         path.Add(startPosition);//路径上添加起始值
        var previousposition = startPosition;//记录以前的值
        for (int i = 0; i < walkLength; i++)
        {
            var newposition = previousposition + Direction2D.GetRandomCardinalDirection();//累加
            path.Add(newposition);//添加
            previousposition= newposition;

        }
        return path;
    }
    public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPosition,int corridorLength)
    {
        List<Vector2Int> corridor = new List<Vector2Int>();//走廊长度
        var direction = Direction2D.GetRandomCardinalDirection();//记录方向
      var currentPosition = startPosition;//记录当前走廊位置
        corridor.Add(currentPosition); 
      for(int i = 0;i < corridorLength;i++)//遍历存储走廊位置的列表
        {
            currentPosition += direction;//当前位置叠加方向
            corridor.Add(currentPosition);
          
        }
      return corridor;
    }
    public static List<BoundsInt> BinarySpacePartitioning(BoundsInt spaceIoSplit,int minwidth,int minHeight)
    {
        Queue<BoundsInt> roomsQueue = new Queue<BoundsInt>();//创建一个队列 roomsQueue 用于存储待分割的空间，并将初始空间 spaceIoSplit 加入队列。
        List<BoundsInt> roomsList = new List<BoundsInt>();//创建一个列表 roomsList 用于存储最终分割好的房间。

        roomsQueue.Enqueue(spaceIoSplit);
       while(roomsQueue.Count > 0) // 循环处理队列中的空间
        {
            var room = roomsQueue.Dequeue();//从队列中取出一个空间 room 进行处理。
            if (room.size.y>=minHeight&&room.size.x>=minwidth)// 检查空间是否满足最小宽度和最小高度要求
            {
                if (Random.value<0.5f)// 水平分割条件判断
                {
                   if(room.size.y >= minHeight *2)
                    {
                        SplitHorizontally(minwidth,minHeight,roomsQueue,room);
                    }  // 垂直分割条件判断
                    else if (room.size.x >= minwidth *2)
                    {
                        SplitVeritically(minwidth, minHeight, roomsQueue, room);
                    }// 空间无法再分割，加入结果列表
                    else if (room.size.x >= minwidth  && room.size.y >=minHeight)
                    {
                        roomsList.Add(room);
                    }
                }
                else
                {
                    // 垂直分割条件判断
                    if (room.size.x >= minwidth * 2)
                    {
                        SplitVeritically(minwidth, minHeight, roomsQueue, room);
                    }// 水平分割条件判断
                    else if (room.size.y >= minHeight * 2)
                    {
                        SplitHorizontally(minwidth, minHeight, roomsQueue, room);
                    }// 空间无法再分割，加入结果列表
                    else if (room.size.x >= minwidth && room.size.y >= minHeight)
                    {
                        roomsList.Add(room);
                    }

                }
            }
        }
       return roomsList;
    }

    private static void SplitVeritically(int minwidth, int minHeight, Queue<BoundsInt> roomQueue, BoundsInt room)//垂直分割一个房间，将其分成左右两个子房间，并将子房间加入到队列中。

    {
        var xSplit = Random.Range(1, room.size.x); //随机选择一个分割点 xSplit，范围在 1 到 room.size.x - 1 之间。
        BoundsInt room1 = new BoundsInt(room.min,new Vector3Int(xSplit,room.size.y,room.size.z));//根据分割点创建两个新的空间 room1 和 room2。
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x+xSplit,room.min.y,room.min.z),new Vector3Int(room.size.x-xSplit,room.size.y,room.size.z));
        roomQueue.Enqueue(room1);//用于存储待分割房间的队列。
        roomQueue.Enqueue(room2);
    }

    private static void SplitHorizontally(int minwidth, int minHeight, Queue<BoundsInt> roomQueue, BoundsInt room)//水平分割一个房间，将其分成上下两个子房间，并将子房间加入到队列中。
    {
        var ySplit = Random.Range(1,room.size.y); //随机选择一个分割点 ySplit，范围在 1 到 room.size.y - 1 之间。
        BoundsInt room1 = new BoundsInt(room.min,new Vector3Int(room.size.x,ySplit,room.size.z));////根据分割点创建两个新的空间 room1 和 room2。
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x,room.min.y+ySplit,room.min.z),new Vector3Int(room.size.x,room.size.y-ySplit,room.size.z));
        roomQueue.Enqueue(room1);//用于存储待分割房间的队列。
        roomQueue.Enqueue(room2);
    }
}
public static class Direction2D
{
    public static List<Vector2Int> cardinalDirectionsList = new List<Vector2Int>
 {
         new Vector2Int(0,1),//UP
         new Vector2Int(1,0),//Right
         new Vector2Int(0,-1),//Down
         new Vector2Int(-1,0),//Left
 };

public static Vector2Int GetRandomCardinalDirection()
    {
        return cardinalDirectionsList[Random.Range(0,cardinalDirectionsList.Count)];//反会列表记载的4种随机树
    }
}

