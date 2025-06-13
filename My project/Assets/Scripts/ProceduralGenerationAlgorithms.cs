using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProceduralGenerationAlgorithms 
{
   
    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition,int walkLength)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();
         path.Add(startPosition);//·���������ʼֵ
        var previousposition = startPosition;//��¼��ǰ��ֵ
        for (int i = 0; i < walkLength; i++)
        {
            var newposition = previousposition + Direction2D.GetRandomCardinalDirection();//�ۼ�
            path.Add(newposition);//���
            previousposition= newposition;

        }
        return path;
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
        return cardinalDirectionsList[Random.Range(0,cardinalDirectionsList.Count)];//�����б���ص�4�������
    }
}

