using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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

