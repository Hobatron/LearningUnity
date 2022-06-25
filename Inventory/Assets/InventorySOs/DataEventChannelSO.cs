using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Data Event")]
public class DataEventChannelSO : ScriptableObject
{
    public UnityAction SaveEventRaised;
    public UnityAction LoadEventRaised;

    public void SaveOnRaised()
    {
        SaveEventRaised.Invoke();
    }
    
    public void LoadOnRaised()
    {
        LoadEventRaised.Invoke();
    }
}
