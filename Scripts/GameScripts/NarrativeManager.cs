using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NarrativeManager : MonoBehaviour
{
    [Header("Text UI")]
    [SerializeField] GameObject narrativePanel;
    [SerializeField] TextMeshProUGUI monologueText;

    private void Awake()
    {
        narrativePanel.SetActive(false);
    }


    #region Narrative_Events
    public void StartIntroMonologueRoutine() //we set it in Game Manager or PlayerCollision
    {
        Debug.Log("StartIntroMonologueRoutine");
      //  StartCoroutine(TurnMonologuePanelRoutine(true, 1f));
        StartCoroutine(SetMonologueTextRoutine("Come on, Goddo! Hurry up! It's getting dark...", 2f));
        StartCoroutine(SetMonologueTextRoutine("We will be home soon...", 5f));
        StartCoroutine(TurnMonologuePanelRoutine(false, 7f));
    }

    #endregion

    public IEnumerator SetMonologueTextRoutine(string _text, float _delayBeforeShowText)
    {
        yield return new WaitForSeconds(_delayBeforeShowText);
        narrativePanel.SetActive(true);
        monologueText.SetText(_text);
    }


    public IEnumerator TurnMonologuePanelRoutine(bool _isPanelOn, float _delay)
    {
        yield return new WaitForSeconds(_delay);
        narrativePanel.SetActive(_isPanelOn);
    }



}
