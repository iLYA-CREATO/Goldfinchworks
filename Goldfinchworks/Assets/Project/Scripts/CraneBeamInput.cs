using System;
using UnityEngine;

public class CraneBeamInput : MonoBehaviour
{
    [SerializeField] private KeyCode hookUp;
    [SerializeField] private KeyCode hookDown;
    [SerializeField] private KeyCode craneHookDown;
    [SerializeField] private KeyCode craneHookUp;
    [SerializeField] private KeyCode craneHolderUp;
    [SerializeField] private KeyCode craneHolderDown;

    public static event Action<float> OnCraneHook;
    public static event Action<float> OnCraneHolder;
    public static event Action<float> OnHookMove;

    private void Update()
    {
        #region Крюк
        if (Input.GetKeyDown(hookDown))
        {
            OnHookMove?.Invoke(1);
        }
        else if (Input.GetKeyUp(hookDown))
        {
            OnHookMove?.Invoke(0);
        }

        if (Input.GetKeyDown(hookUp))
        {
            OnHookMove?.Invoke(-1);
        }
        else if (Input.GetKeyUp(hookUp))
        {
            OnHookMove?.Invoke(0);
        }
        #endregion

        #region Кран с крюком
        if (Input.GetKeyDown(craneHookUp))
        {
            OnCraneHook?.Invoke(1);
        }
        else if (Input.GetKeyUp(craneHookUp))
        {
            OnCraneHook?.Invoke(0);
        }

        if (Input.GetKeyDown(craneHookDown))
        {
            OnCraneHook?.Invoke(-1);
        }
        else if (Input.GetKeyUp(craneHookDown))
        {
            OnCraneHook?.Invoke(0);
        }
        #endregion

        #region Кран балка

        if (Input.GetKeyDown(craneHolderDown))
        {
            OnCraneHolder?.Invoke(1);
        }
        else if (Input.GetKeyUp(craneHolderDown))
        {
            OnCraneHolder?.Invoke(0);
        }

        if (Input.GetKeyDown(craneHolderUp))
        {
            OnCraneHolder?.Invoke(-1);
        }
        else if (Input.GetKeyUp(craneHolderUp))
        {
            OnCraneHolder?.Invoke(0);
        }
        #endregion
    }
}
