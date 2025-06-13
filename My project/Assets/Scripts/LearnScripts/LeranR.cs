using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LeranR : MonoBehaviour
{public Vector2Int startPosition = Vector2Int.zero;
    public int literation = 10;
    public int walkLength = 10;

public bool b1 = true;

 public void Onclick()
    {
        HashSet<Vector2Int> list = Run();
        foreach (var item in list)
        {
            Debug.Log(item);
        }
    }
 public HashSet<Vector2Int> Run()
    {
        var currentPosition = startPosition;
        HashSet<Vector2Int> listH = new HashSet<Vector2Int>();
       for (int i = 0;i<=literation; i++)
        {
            var path = LearnP.per(startPosition, walkLength);
            listH.UnionWith(path);
         if(b1)
            {
              currentPosition = listH.ElementAt(Random.Range(0,listH.Count));
            }
        }

        return listH;
     
    }
}
