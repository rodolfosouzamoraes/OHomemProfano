using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="State")]
public class State : ScriptableObject
{
    [TextArea(14, 10)] [SerializeField] string textoJogo;
    [SerializeField] State[] proximoState;

    public string GetStateStory()
    {
        return textoJogo;
    }

    public State[] GetNextState()
    {
        return proximoState;
    }
}
