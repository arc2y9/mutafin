using System;
using Unity.VisualScripting;
using UnityEngine;

public class Soil : MineableObject
{
    public override string name { get; set; } = "Soil";
    public override float hardness { get; } = 0.25f;
    public override void Mine()
    {
        
    }
}