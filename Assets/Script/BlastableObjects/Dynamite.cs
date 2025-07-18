using UnityEngine;

public class Dynamite : BlastableObject
{
    public override float lastingTime { get; set; } = 2f;

    public override int range { get; set; } = 3;
}
