using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//����һ�����Ա�ǩ�������� Unity �༭����ͨ���Ҽ��˵����������ʵ����.asset �ļ�����
[CreateAssetMenu(fileName = "SimpleRandomWalkParamters_",menuName = "PCG/SimpleRandomWalkData")]
public class SimpleRandomWalkSo : ScriptableObject//ScriptableObject �� Unity �е�һ�����������������ڴ洢�������ݲ��ڶ������乲���� MonoBehaviour ��ͬ��������Ҫ���ӵ���Ϸ�����ϣ������ڴ洢�����л������ݡ�
{
   public int interations = 10 ,walkLength  =10 ;
public bool startRnadomlyEachIteration = true ;

}
