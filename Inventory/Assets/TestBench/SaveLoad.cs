using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    [SerializeField] DataEventChannelSO dataEvents;
    public void SaveData()
    {
        dataEvents.SaveOnRaised();
    }
    public void LoadData()
    {
        dataEvents.LoadOnRaised();
    }
}
