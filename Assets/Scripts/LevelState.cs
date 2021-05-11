using UnityEngine;

public class LevelState : MonoBehaviour
{
    public enum State
    {
        Checkpoint0,
        Checkpoint1,
        Checkpoint2,
        Checkpoint3
    }

    public State state = State.Checkpoint0;
}
