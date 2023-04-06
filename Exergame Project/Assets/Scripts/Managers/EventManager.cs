using System;

public class EventManager
{
    #region GameManager Events
    public static Func<GameData> getGameData;
    #endregion

    #region CanvasManager Events
    public static float levelTimer;
    public static Action levelEndUI;
    public static Action uiTimer;
    public static Action<string> updateDescription;
    public static Action<float, float> updateMissionCircle;
    #endregion

    #region HandTracking events
    public static Action updateHandPoints;
    public static Func<string, bool> isGrabbing;
    public static Action closeHandsInTravel;
    public static Action<float, float> setMaxAndMinZPoint;
    #endregion
}