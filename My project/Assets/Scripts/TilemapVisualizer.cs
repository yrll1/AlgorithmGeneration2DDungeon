using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTilemap;
    [SerializeField]
    private TileBase floorTile;
    
    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)//�����ⲿ���õĹ����������Ƶذ���Ƭ(IEnumerable<>���ͽӿ�,��������һ�����Ա������ļ���,ʵ��������ϵ������ѭ��������Ԫ��)
    {
        PaintTiles(floorPositions, floorTilemap, floorTile);//�˴�floorPositions�Դ洢���õ��������β�������
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)////��������λ�ü��ϡ���Ƭ��ͼ���ú���Ƭ���ͣ�����ÿ��λ�ò�����PaintSingleTile��
    {
     foreach (var position in positions)//ѭ����������Positionsÿ��XY����
        {
          PaintSingleTile(tilemap,tile,position);
        }
    }

    private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)// PaintSingleTile ������ 2D ��������ת��Ϊ��Ƭ��ͼ�еĵ�Ԫ�����꣬Ȼ���ڸ�λ������ָ������Ƭ��
    {                                                            //���ﲻʹ��IEnumerable�ӿ�����Ϊ��������ֻ��һ������
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);//����������ת����2ά��Ƭ��ͼ����,��ʹ����ʾ����ת��
        tilemap.SetTile(tilePosition,tile);//����Ƭ��ͼ�л���,��һ��������2ά����,��2����������Ҫ���Ƶ���Ƭ
    }
    public void Clear()
    {
        floorTilemap.ClearAllTiles();//��յ�ǰ���Ƶ���Ƭ��ͼ
    }
}
