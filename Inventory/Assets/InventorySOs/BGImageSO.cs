using UnityEngine;

[CreateAssetMenu(menuName = "BG Image", fileName = "bg-image")]
public class BGImageSO : ScriptableObject
{
    [SerializeField] Sprite SourceImage;
}
