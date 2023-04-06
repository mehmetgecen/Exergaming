using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System;

public class CanvasManager : MonoBehaviour
{
    #region variables for canvas objects
    public GameObject bottomPanel, nextLevelPanel, sandClock;
    public Transform missionDescriptionsUI;
    public TextMeshProUGUI timerTMP, missionDescriptionTMP, missionFilledTMP, levelEndTMP;
    public Image missionFilledBar, missionIcon, doneIcon;
    #endregion

    private void OnEnable()
    {
        EventManager.levelEndUI += LevelEndUI;
        EventManager.uiTimer += UiTimer;
        EventManager.updateDescription += UpdateDescription;
        EventManager.updateMissionCircle += UpdateMissionCircle;
    }

    private void OnDisable()
    {
        EventManager.levelEndUI -= LevelEndUI;
        EventManager.uiTimer -= UiTimer;
        EventManager.updateDescription -= UpdateDescription;
        EventManager.updateMissionCircle -= UpdateMissionCircle;
    }

    private void Update()
    {
        EventManager.uiTimer?.Invoke();
    }

    private void LevelEndUI()
    {
        // set level end text
        //levelEndTMP.text = String.Format("Level {0} is Completed.", EventManager.getGameData.Invoke().Level + 1);
        levelEndTMP.text = "All Levels Are Completed.";
        // stop timer
        EventManager.uiTimer -= UiTimer;
        sandClock.GetComponent<Animator>().enabled = false;
        sandClock.transform.DORotate(Vector3.zero, 0.5f).OnComplete(() => 
        {
            nextLevelPanel.GetComponent<Image>().DOFade(0.98f, 1f);
            bottomPanel.GetComponent<Image>().DOFade(0.98f, 1f);
        });

        nextLevelPanel.SetActive(true);

        // end animation
        missionIcon.DOFillAmount(0f, 0.5f).OnComplete(() => 
        {
            doneIcon.DOFillAmount(1f, 0.5f).OnComplete(() => doneIcon.rectTransform.DOAnchorPosX(-440, 1f));
        });
    }

    private void UiTimer()
    {
        EventManager.levelTimer += Time.deltaTime;

        int minutes = Mathf.FloorToInt(EventManager.levelTimer / 60F);
        int seconds = Mathf.FloorToInt(EventManager.levelTimer - minutes * 60);

        string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);

        timerTMP.text = niceTime;
    }

    private void UpdateMissionCircle(float missionCounter, float missionLength)
    {
        missionFilledTMP.text = missionCounter + "/" + missionLength;
        missionFilledBar.DOFillAmount(missionCounter / missionLength, 0.5f);
    }

    private void UpdateDescription(string newDescriptionStr)
    {
        StartCoroutine(UpdateDescriptionAnim(newDescriptionStr));
    }

    private IEnumerator UpdateDescriptionAnim(string newDescriptionStr)
    {
        missionDescriptionTMP.gameObject.GetComponent<Animator>().SetBool("runAnim", true);

        yield return new WaitForSeconds(0.5f);

        missionDescriptionTMP.gameObject.GetComponent<Animator>().SetBool("runAnim", false);
        
        missionDescriptionTMP.text = newDescriptionStr;
    }
}