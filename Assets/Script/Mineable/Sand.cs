using UnityEngine;

public class Sand : MineableObject
{
    public override string name { get; set; } = "Sand";

    public override float hardness => .35f;

    public override void Mine()
    {
    }
}
