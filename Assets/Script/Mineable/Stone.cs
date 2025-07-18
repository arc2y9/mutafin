using UnityEngine;

public class Stone : MineableObject
{
    [SerializeField] GameObject collactable;
    public override GameObject CollectableObject { get => collactable; set => collactable = value; }
    public override string name { get; set; } = "Stone";

    public override float hardness => 2f;
    public override void Mine()
    {
    }
}
