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
    private SimpleRandomWalkSo randomWalkParameters;


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
        for (int i = 0; i < randomWalkParameters.interations; i++)
        {
            var path = ProceduralGenerationAlgorithms.SimpleRandomWalk(currentPosition, randomWalkParameters.walkLength);//������������
            floorPositions.UnionWith(path);//ȥ��floorPositionsÿ�ε������ظ���ֵ
            if (randomWalkParameters.startRnadomlyEachIteration)
            {
                currentPosition = floorPositions.ElementAt(Random.Range(0,floorPositions.Count));//ÿ�ε�����ʼλ�������
            }
        }
        return floorPositions;

    }

  
}

