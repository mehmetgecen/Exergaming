using UnityEngine;
using DG.Tweening;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    #region variables for mission control
    public int missionCounter = 0;
    private bool _isButtonActive = false;
    public GameObject[] missions;
    public Transform[] cameraPoses;
    public Mission_5_UI_Control missionUIControl;
    #endregion

    #region variables for level management
    private Transform handTracking;
    #endregion

    #region variables for UI managemen
    public string[] missionDescriptions;
    #endregion

    private void Start()
    {
        // set camera pos for first mission
        Camera.main.transform.SetPositionAndRotation(cameraPoses[0].transform.position, cameraPoses[0].transform.rotation);
        handTracking = Camera.main.transform.GetChild(0);
        handTracking.transform.localPosition = cameraPoses[missionCounter].transform.GetChild(0).transform.localPosition;
        handTracking.transform.localRotation = cameraPoses[missionCounter].transform.GetChild(0).transform.localRotation;

        // set description UI
        EventManager.updateDescription?.Invoke(missionDescriptions[missionCounter]);
        EventManager.updateMissionCircle?.Invoke(missionCounter, missions.Length);
    }

    public void SkipMission()
    {
        missionCounter++;

        if (missionCounter >= missions.Length) // level end control
        {
            // set finish UI
            EventManager.updateMissionCircle?.Invoke(missionCounter, missions.Length);
            EventManager.updateDescription?.Invoke("All missions completed");

            EventManager.levelEndUI?.Invoke();

        }
        else // skip next mission
        {
            // set NEW description UI
            EventManager.updateDescription?.Invoke(missionDescriptions[missionCounter]);
            EventManager.updateMissionCircle?.Invoke(missionCounter, missions.Length);

            // open next mission
            missions[missionCounter].SetActive(true);

            // move camera to next mission camera pos
            Camera.main.transform.DOMove(cameraPoses[missionCounter].transform.position, 1f);
            Camera.main.transform.DORotate(cameraPoses[missionCounter].transform.eulerAngles, 1f);

            // move hand to next mission hand position
            handTracking.transform.localPosition = cameraPoses[missionCounter].transform.GetChild(0).transform.localPosition;
            handTracking.transform.localRotation = cameraPoses[missionCounter].transform.GetChild(0).transform.localRotation;

            // close hands for 1 second
            EventManager.closeHandsInTravel?.Invoke();
        }
    }

    private void Update()
    {
        if ((missionCounter == 4) && (!_isButtonActive))
        {
            StartCoroutine(Mission5UIControl());
        }
    }

    public IEnumerator Mission5UIControl()
    {
        yield return new WaitForSeconds(1f);

        missionUIControl.leftRecipeButton.gameObject.SetActive(true);
        missionUIControl.rightRecipeButton.gameObject.SetActive(true);

        _isButtonActive = true;
    }
}