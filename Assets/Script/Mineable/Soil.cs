using System;
using Unity.VisualScripting;
using UnityEngine;

public class Soil : MineableObject
{
    [SerializeField] GameObject collactable;
    public override GameObject CollectableObject { get => collactable; set => collactable = value; }
    public override string name { get; set; } = "Soil";
    public override float hardness { get; } = 0.55f;
    public override void Mine()
    {
        
    }
}