using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
[CustomEditor(typeof(AbstractDungonGeneration),true)]//自定义编辑器,指定编辑器编译AbstractDungeonGeneration类,并且也会应用与继承的字类
public class RandomDungeonGeneratorEditor : Editor
{
    AbstractDungonGeneration generator;

    private void Awake()
    {
        generator = (AbstractDungonGeneration)target;//将通用的target对象转换为具体的AbstractDungonGeneration类型
                                                     //这样就可以直接访问目标组件的方法和属性
    }
    public override void OnInspectorGUI()//这是自定义编辑器的核心方法，负责绘制 Inspector 界面
                                         //base.OnInspectorGUI()调用父类方法绘制默认界面
                                         //添加一个名为 "Create Dungeon" 的按钮
                                      //当按钮被点击时，调用generator.GenerateDungeon() 方法生成地牢
    {
      base.OnInspectorGUI();
    if(GUILayout.Button("Create Dungeon"))
        {
            generator.GenerateDungeon();
        }
    }



}
