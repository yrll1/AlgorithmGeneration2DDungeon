using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Compilation;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimpleRandomWalkGenerator : AbstractDungonGeneration
{
 
    [SerializeField]
    private int iterations = 10;//��������
    [SerializeField]
    public int walkLength = 10;//���β���
    [SerializeField]
    public bool startRandomEachIteration = true;



    protected override void RunProceduralGeneration()
    {
        HashSet<Vector2Int> floorPositions = RunRandowWalk();
        tilemapVisualizer.Clear();
       tilemapVisualizer.PaintFloorTiles(floorPositions);
    }

   protected HashSet<Vector2Int> RunRandowWalk()
    {
       var currentPosition = startPosition;//��¼��ʼλ��
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        for (int i = 0; i < iterations; i++)
        {
            var path = ProceduralGenerationAlgorithms.SimpleRandomWalk(currentPosition, walkLength);//������������
            floorPositions.UnionWith(path);//ȥ��floorPositionsÿ�ε������ظ���ֵ
            if (startRandomEachIteration)
            {
                currentPosition = floorPositions.ElementAt(Random.Range(0,floorPositions.Count));//ÿ�ε�����ʼλ�������
            }
        }
        return floorPositions;

    }

  
}

