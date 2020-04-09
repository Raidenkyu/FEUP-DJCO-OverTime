using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehaviour : MonoBehaviour
{
    public enum MonsterState {ROAM, PREY, ATTACK, FREEZE };
    private MonsterState state;
    // Start is called before the first frame update
    void Start()
    {
        state = MonsterState.ROAM;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    MonsterState GetState() {
        return state;
    }
}
