using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class WallGenerator
{
    public static void CreateWalls(HashSet<Vector2Int>floorPositions,TilemapVisualizer tilemapVisualizer)//�ɼ�����������ɵ���,
    {
        var basicWallPositions = FindWallsInDirections(floorPositions,Direction2D.cardinalDirectionsList);
        foreach (var position in basicWallPositions)//�������ص�ǽ������
        {
            tilemapVisualizer.PaintSingleBasicWall(position);//������Ƭ��ͼ���ӻ������е�������ǽ��ͼ

        }                                                                     
    }

    private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> directionList)//Ѱ��ǽ�崦�ڵķ���
    {
     HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();//�洢ǽ��λ��
        foreach (var position in floorPositions)//�������е�תλ��
        {
            foreach (var direction in directionList)//�����������ĸ�����(��������)λ����
            {      //neighbour:�ھ�λ��
               var neighbourPosition = position + direction;//�õ����λ��
                if (floorPositions.Contains(neighbourPosition)==false)//�ж��ھ������Ƿ���е�ש����(���û�о���ǽ������)
                {
                    wallPositions.Add(neighbourPosition);//����������ӵ�ǽ��λ��
                }
            }
        }
        return wallPositions;
    }
}
