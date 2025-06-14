using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//这是一个属性标签，允许在 Unity 编辑器中通过右键菜单创建该类的实例（.asset 文件）。
[CreateAssetMenu(fileName = "SimpleRandomWalkParamters_",menuName = "PCG/SimpleRandomWalkData")]
public class SimpleRandomWalkSo : ScriptableObject//ScriptableObject 是 Unity 中的一种数据容器，可用于存储配置数据并在多个对象间共享。与 MonoBehaviour 不同，它不需要附加到游戏对象上，常用于存储可序列化的数据。
{
   public int interations = 10 ,walkLength  =10 ;
public bool startRnadomlyEachIteration = true ;

}
