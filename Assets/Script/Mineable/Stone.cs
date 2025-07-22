using UnityEngine;

public class Stone : MineableObject
{
    [SerializeField] GameObject collactable;
    public override string Name { get; set; } = "Stone";

    public override float hardness => 2f;
    public override void Mine()
    {
    }
}
