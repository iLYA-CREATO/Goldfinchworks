using System;
using UnityEngine;

public class CraneBeam : MonoBehaviour
{
    [Header("Основные части крана")]
    [SerializeField] private Transform craneHook;
    [SerializeField] private Transform craneHolder;
    [SerializeField] private Transform hook;

    [Header("Текущее состояние")]
    [SerializeField] private bool isCraneHookMoving = false;
    [SerializeField] private bool isCraneHolderMoving = false;
    [SerializeField] private bool isHookMoving = false;

    [Header("Границы движения")]
    [SerializeField] private float minCraneHookDown = -10f;
    [SerializeField] private float maxCraneHookDown = 10f;

    [SerializeField] private float minHook = 1f;
    [SerializeField] private float maxHook = 8f;

    [SerializeField] private float minCranHolder = 1f;
    [SerializeField] private float maxCranHolder = 8f;

    [Header("Настройки движения")]
    [SerializeField] private float craneHookSpeed = 3f;
    [SerializeField] private float craneHolderSpeed = 3f;
    [SerializeField] private float hookSpeed = 2f;

    private Vector3 craneHookVelocity = Vector3.zero;
    private Vector3 craneHolderVelocity = Vector3.zero;
    private Vector3 hookVelocity = Vector3.zero;

    private float directionHookVertical;
    private float directionCraneHookHorizontal;
    private float directionCraneHolderHorizontal;

    [SerializeField] private RemoteController remoteController;

    [SerializeField] private AudioSource audioSourceHook;
    [SerializeField] private AudioSource audioSourceCraneHook;
    [SerializeField] private AudioSource audioSourceCraneHolder;

    [SerializeField] private AudioClip audioClipHook;
    [SerializeField] private AudioClip audioClipCraneHook;
    [SerializeField] private AudioClip audioClipCraneHolder;
    private void OnEnable()
    {
        CraneBeamInput.OnHookMove += MoveVerticalHook;
        CraneBeamInput.OnCraneHook += MoveHorizontalCranHook;
        CraneBeamInput.OnCraneHolder += MoveHorizontalCranHolder;
    }

    private void Update()
    {
        if (directionCraneHookHorizontal != 0)
        {
            if (!audioSourceCraneHook.isPlaying)
                audioSourceCraneHook.PlayOneShot(audioClipCraneHook);

            isCraneHookMoving = true;
            craneHookVelocity = new Vector3(directionCraneHookHorizontal * craneHookSpeed, 0, 0);
        }
        else
        {
            StopCraneMovement();
        }

        if (directionCraneHolderHorizontal != 0)
        {
            if (!audioSourceCraneHolder.isPlaying)
                audioSourceCraneHolder.PlayOneShot(audioClipCraneHolder);

            isCraneHolderMoving = true;
            craneHolderVelocity = new Vector3(0, 0, directionCraneHolderHorizontal * craneHolderSpeed);
        }
        else
        {
            StopCraneHolderMovement();
        }

        if (directionHookVertical != 0)
        {
            if (!audioSourceHook.isPlaying)
                audioSourceHook.PlayOneShot(audioClipHook);

            isHookMoving = true;
            hookVelocity = new Vector3(0, directionHookVertical * hookSpeed, 0);
        }
        else
        {
            StopHookMovement();
        }


        if (isHookMoving)
        {
            MoveHook();
        }

        if(isCraneHolderMoving)
        {
            MoveCraneHolder();
        }

        if (isCraneHookMoving)
        {
            MoveCraneHook();
        }
        
    }
    private void OnDisable()
    {
        CraneBeamInput.OnHookMove -= MoveVerticalHook;
        CraneBeamInput.OnCraneHook -= MoveHorizontalCranHook;
        CraneBeamInput.OnCraneHolder -= MoveHorizontalCranHolder;
    }

    private void MoveVerticalHook(float direction)
    {
        directionHookVertical = direction;

        if (directionHookVertical == 1)
        {
            remoteController.button1.color = remoteController.buttonOn;
        }
        else if (directionHookVertical == 0)
        {
            remoteController.button1.color = remoteController.buttonOff;
        }

        if (directionHookVertical == -1)
        {
            remoteController.button2.color = remoteController.buttonOn;
        }
        else if (directionHookVertical == 0)
        {
            remoteController.button2.color = remoteController.buttonOff;
        }
    }
    private void MoveHorizontalCranHook(float direction)
    {
        directionCraneHookHorizontal = direction;

        if (directionCraneHookHorizontal == 1)
        {
            remoteController.button3.color = remoteController.buttonOn;
        }
        else if (directionCraneHookHorizontal == 0)
        {
            remoteController.button3.color = remoteController.buttonOff;
        }
        if (directionCraneHookHorizontal == -1)
        {
            remoteController.button4.color = remoteController.buttonOn;
        }
        else if (directionCraneHookHorizontal == 0)
        {
            remoteController.button4.color = remoteController.buttonOff;
        }
    }
    private void MoveHorizontalCranHolder(float direction)
    {
        directionCraneHolderHorizontal = direction;

        if (directionCraneHolderHorizontal == 1)
        {
            remoteController.button5.color = remoteController.buttonOn;
        }
        else if (directionCraneHolderHorizontal == 0)
        {
            remoteController.button5.color = remoteController.buttonOff;
        }

        if (directionCraneHolderHorizontal == -1)
        {
            remoteController.button6.color = remoteController.buttonOn;
        }
        else if (directionCraneHolderHorizontal == 0)
        {
            remoteController.button6.color = remoteController.buttonOff;
        }
    }

    private void MoveCraneHolder()
    {
        Debug.Log("Двигаем кран балку");
        Vector3 newPosition = craneHolder.localPosition + craneHolderVelocity * Time.deltaTime;
        newPosition.z = Mathf.Clamp(newPosition.z, minCranHolder, maxCranHolder);
        craneHolder.localPosition = newPosition;
    }
    private void MoveCraneHook()
    {
        Debug.Log("Двигаем кран с крюком");
        Vector3 newPosition = craneHook.localPosition + craneHookVelocity * Time.deltaTime;
        newPosition.x = Mathf.Clamp(newPosition.x, minCraneHookDown, maxCraneHookDown);
        craneHook.localPosition = newPosition;
    }

    private void MoveHook()
    {
        Debug.Log("Двигаем крюк");
        Vector3 newPosition = hook.localPosition + hookVelocity * Time.deltaTime;
        newPosition.y = Mathf.Clamp(newPosition.y, minHook, maxHook);
        hook.localPosition = newPosition;
    }

    private void StopCraneMovement()
    {
        audioSourceCraneHook.Pause();
        isCraneHookMoving = false;
        craneHookVelocity = Vector3.zero;
    }
    private void StopCraneHolderMovement()
    {
        audioSourceCraneHolder.Pause();
        isCraneHolderMoving = false;
        craneHolderVelocity = Vector3.zero;
    }
    private void StopHookMovement()
    {
        audioSourceHook.Pause();

        isHookMoving = false;
        hookVelocity = Vector3.zero;
    }
}


[Serializable]
public class RemoteController
{
    [Header("Кнопка нажата")]
    public Color buttonOn;
    [Header("Кнопка не нажата")]
    public Color buttonOff;

    [Space(20)]
    public Material button1;
    public Material button2;
    public Material button3;
    public Material button4;
    public Material button5;
    public Material button6;
}