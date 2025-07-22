using UnityEngine;

public class Sand : MineableObject
{
    [SerializeField] GameObject collactable;
    public override string Name { get; set; } = "Sand";

    public override float hardness => .35f;

    public override void Mine()
    {
        
    }
}
