using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
[CustomEditor(typeof(AbstractDungonGeneration),true)]//�Զ���༭��,ָ���༭������AbstractDungeonGeneration��,����Ҳ��Ӧ����̳е�����
public class RandomDungeonGeneratorEditor : Editor
{
    AbstractDungonGeneration generator;

    private void Awake()
    {
        generator = (AbstractDungonGeneration)target;//��ͨ�õ�target����ת��Ϊ�����AbstractDungonGeneration����
                                                     //�����Ϳ���ֱ�ӷ���Ŀ������ķ���������
    }
    public override void OnInspectorGUI()//�����Զ���༭���ĺ��ķ������������ Inspector ����
                                         //base.OnInspectorGUI()���ø��෽������Ĭ�Ͻ���
                                         //���һ����Ϊ "Create Dungeon" �İ�ť
                                      //����ť�����ʱ������generator.GenerateDungeon() �������ɵ���
    {
      base.OnInspectorGUI();
    if(GUILayout.Button("Create Dungeon"))
        {
            generator.GenerateDungeon();
        }
    }



}
