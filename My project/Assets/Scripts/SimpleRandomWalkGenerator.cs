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
       var currentPosition = startPosition;//记录起始位置
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        for (int i = 0; i < randomWalkParameters.interations; i++)
        {
            var path = ProceduralGenerationAlgorithms.SimpleRandomWalk(currentPosition, randomWalkParameters.walkLength);//调用漫步程序
            floorPositions.UnionWith(path);//去重floorPositions每次迭代中重复的值
            if (randomWalkParameters.startRnadomlyEachIteration)
            {
                currentPosition = floorPositions.ElementAt(Random.Range(0,floorPositions.Count));//每次迭代起始位置随机化
            }
        }
        return floorPositions;

    }

  
}

