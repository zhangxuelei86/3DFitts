﻿using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConfigurationCanvasBehaviour : MonoBehaviour
{
    public Dropdown participantCode;
    public Dropdown conditionCode;
    public Dropdown sessionCode;
    public Dropdown groupCode;
    public InputField observations;

    public Dropdown experimentMode;
    public Dropdown experimentTask;
    
    public Dropdown cursorPositioningMethod;
    public Dropdown cursorSelectionMethod;

    public Dropdown planeOrientation;

    public Dropdown numberOfTargets;

    public InputField cursorWidth;
    public InputField amplitudes;
    public InputField widths;
     
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDropdownValueChanged(Dropdown dropdown)
    {
        // TODO: one day, refactor the canvas so the dropdown items order will automatically
        // follow the order in the equivalent enums
        if (dropdown == experimentMode)
        {
            ExperimentMode newValue = (ExperimentMode)experimentMode.value;
            CurrentExperimentConfiguration.experimentMode = newValue;

            switch (CurrentExperimentConfiguration.experimentMode)
            {
                case ExperimentMode.Experiment2D:
                    // For now, only mouse can be used for 2D experiment
                    cursorPositioningMethod.value = (int)CursorPositioningMethod.Mouse;
                    cursorPositioningMethod.interactable = false;

                    // For now, you can only test the plane of the screen
                    planeOrientation.value = (int)PlaneOrientation.PlaneXY;
                    planeOrientation.interactable = false;

                    break;
                case ExperimentMode.Experiment3DOnMeta2:
                    cursorPositioningMethod.interactable = true;
                    planeOrientation.interactable = true;
                    break;
            }
        }
        else if (dropdown == experimentTask)
        {
            CurrentExperimentConfiguration.experimentTask = (ExperimentTask)dropdown.value;
        }
        else if (dropdown == cursorPositioningMethod)
        {
            CurrentExperimentConfiguration.cursorPositioningMethod = (CursorPositioningMethod)dropdown.value;
        }
        else if (dropdown == cursorSelectionMethod)
        {
            CurrentExperimentConfiguration.cursorSelectionMethod = (CursorSelectionMethod)dropdown.value;
        }
        else if (dropdown == planeOrientation)
        {
            CurrentExperimentConfiguration.planeOrientation = (PlaneOrientation)dropdown.value;
        }
        else if (dropdown == numberOfTargets)
        {
            CurrentExperimentConfiguration.numberOfTargets = 2*dropdown.value + 3;
        }
        else if (dropdown == participantCode)
        {
            CurrentExperimentConfiguration.participantCode = dropdown.options[dropdown.value].text;
        }
        else if (dropdown == conditionCode)
        {
            CurrentExperimentConfiguration.conditionCode = dropdown.options[dropdown.value].text;
        }
        else if (dropdown == sessionCode)
        {
            CurrentExperimentConfiguration.sessionCode = dropdown.options[dropdown.value].text;
        }
        else if (dropdown == groupCode)
        {
            CurrentExperimentConfiguration.groupCode = dropdown.options[dropdown.value].text;
        }
    }

    public void OnObservationsValueChanged(string newValue)
    {
        CurrentExperimentConfiguration.observations = newValue;
    }

    public void OnCursorWidthValueChanged(string newValue)
    {
        if (CurrentExperimentConfiguration.TrySetCursorWidthFromString(newValue))
        {
            cursorWidth.text = CurrentExperimentConfiguration.cursorWidth.ToString();
        }
        else
        {
            cursorWidth.text = "-";
        }
    }

    public void OnAmplitudesValueChanged(string newValue)
    {
        if (!CurrentExperimentConfiguration.TrySetAmplitudesFromString(newValue))
        {
            amplitudes.text = "-";
        }
    }

    public void OnWidthsValueChanged(string newValue)
    {
        if (!CurrentExperimentConfiguration.TrySetWidthsFromString(newValue))
        {
            widths.text = "-";
        }
    }

    public void RunExperiment()
    {
        switch (CurrentExperimentConfiguration.experimentMode)
        {
            case ExperimentMode.Experiment2D:
                SceneManager.LoadScene("2DMouseExperimentOnScreen");
                break;
            case ExperimentMode.Experiment3DOnMeta2:
                SceneManager.LoadScene("3DExperimentMeta2");
                break;
        }


    }
}
