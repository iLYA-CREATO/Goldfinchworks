using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GasAnalyzer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textDistance;
    [SerializeField] private GameObject display;
    [SerializeField] private Image progresBar;
    [SerializeField] private float speedProgressBar;
    [SerializeField] private float distanceDangerObject;
    [SerializeField] private float maxDistanceDangerObject;
    [Header("Время зажития для вкл/выкл")]
    [SerializeField] private float timeActivet;
    [SerializeField] private List<GameObject> dangerObjects;

    [SerializeField] private GasAnalizState gasAnalizState;

    [SerializeField] private KeyCode activeGasAnaliz;
    
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;

    [Header("Доп данные")]
    [SerializeField] private float timeActiv = 0;
    [SerializeField] private float disOld;
    [SerializeField] private float disNew;
    [SerializeField] private Vector3 dirNew;
    [SerializeField] private Vector3 dirOld;
    private void Start()
    {
        progresBar.fillAmount = 0;
        progresBar.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKey(activeGasAnaliz))
        {
            ActivatedDisplay();
        }
        else if (Input.GetKeyUp(activeGasAnaliz))
        {
            timeActiv = 0;
            progresBar.fillAmount = 0;
            progresBar.gameObject.SetActive(false);
        }

        CheckDistance();


        if (gasAnalizState == GasAnalizState.On)
        {
            GasAnalizActive();
        }
        else
        {
            display.SetActive(false);
        }
    }

    private void ActivatedDisplay()
    {
        if (progresBar.fillAmount < 1f)
        {
            progresBar.gameObject.SetActive(true);
        }

        timeActiv += Time.deltaTime;
        progresBar.fillAmount += Time.deltaTime * speedProgressBar;

        if (timeActiv >= timeActivet)
        {
            progresBar.gameObject.SetActive(false);

            if (gasAnalizState == GasAnalizState.On)
                gasAnalizState = GasAnalizState.Off;
            else if (gasAnalizState == GasAnalizState.Off)
                gasAnalizState = GasAnalizState.On;

            timeActiv = 0;
            return;
        }
    }
    private void GasAnalizActive()
    {
        display.SetActive(true);
        distanceDangerObject = dirOld.magnitude;
        if (maxDistanceDangerObject >= distanceDangerObject)
        {
            textDistance.text = "Distance:\r\n " + distanceDangerObject.ToString("F2") + " m";

            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(audioClip);
            }

            if (distanceDangerObject > 15) audioSource.volume = 0;
            else if (distanceDangerObject > 10 && distanceDangerObject < 15) audioSource.volume = 0.008f;
            else if (distanceDangerObject > 5 && distanceDangerObject < 10) audioSource.volume = 0.04f;
            else if (distanceDangerObject > 2 && distanceDangerObject < 5) audioSource.volume = 0.06f;
            else if (distanceDangerObject >= 0 && distanceDangerObject < 2) audioSource.volume = 0.1f;
        }
        else
        {
            audioSource.Pause();
            textDistance.text = "Distance:\r\n No";
        }
    }
    private void CheckDistance()
    {
        disOld = Mathf.Infinity;
        dirOld = Vector3.zero;
        GameObject closestObject = null;

        foreach (GameObject danObject in dangerObjects)
        {
            if (danObject == null) continue;

            dirNew = danObject.transform.position - transform.position;
            disNew = dirNew.magnitude;


            if (disNew < disOld)
            {
                disOld = disNew;
                dirOld = dirNew;
                closestObject = danObject;
            }
        }
    }
}

public enum GasAnalizState
{
    On,
    Off,
}

