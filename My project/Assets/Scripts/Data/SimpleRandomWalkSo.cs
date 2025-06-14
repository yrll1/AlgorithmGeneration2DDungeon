using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SimpleRandomWalkParamters_",menuName = "PCG/SimpleRandomWalkData")]
public class SimpleRandomWalkSo : ScriptableObject
{
   public int interations = 10 ,walkLength  =10 ;
public bool startRnadomlyEachIteration = true ;

}
