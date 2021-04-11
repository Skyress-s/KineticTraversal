using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayerBaseState
{
    public abstract void EnterState(Context player);

    public abstract void Update(Context player);

    public abstract void ExitState(Context player);

}
