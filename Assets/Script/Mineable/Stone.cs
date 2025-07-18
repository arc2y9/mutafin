using UnityEngine;

public class Stone : MineableObject
{
    public override string name { get; set; } = "Stone";

    public override float hardness => 2f;

    public override void Mine()
    {
    }
}
