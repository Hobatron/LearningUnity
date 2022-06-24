using UnityEngine;

[CreateAssetMenu(menuName = "Items/Allowed Item")]
public class AllowedItemTypeSO : ScriptableObject
{
    [SerializeField] public ItemType itemType;
    [SerializeField] public BoolEventChannelSO CanDrop;

}
