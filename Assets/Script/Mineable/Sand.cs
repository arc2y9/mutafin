using UnityEngine;

public class Sand : MineableObject
{
    [SerializeField] GameObject collactable;
    public override GameObject CollectableObject { get => collactable; set => collactable = value; }
    public override string name { get; set; } = "Sand";

    public override float hardness => .35f;

    public override void Mine()
    {

    }
}
