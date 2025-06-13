using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public static class LearnP 
{
 public static HashSet<Vector2Int> per(Vector2Int startPosition,int walkLength)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();
        path.Add(startPosition);
        var periousPosition =startPosition;
     for (int i = 0; i < walkLength; i++)
        {
            var newPos = periousPosition+ Direction2D1.retunRandom();
            path.Add(periousPosition);
             periousPosition = newPos;
        }
     return path;
    }
}
public static class Direction2D1
{
    public static List<Vector2Int> vector2Ints = new List<Vector2Int>
    {
        new Vector2Int(-1,0),
        new Vector2Int(1,0),
        new Vector2Int(0,1),
        new Vector2Int(0,-1),
    };
   public static Vector2Int retunRandom()
    {
       return vector2Ints[Random.Range(0,vector2Ints.Count)];
    }
}
