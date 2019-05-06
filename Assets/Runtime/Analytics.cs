using GameAnalyticsSDK;
using Naninovel;
using Naninovel.Actions;
using UnityEngine;

public class Analytics : MonoBehaviour
{
    private NovelScriptPlayer player;

    private void OnEnable ()
    {
        Engine.OnInitialized += HandleEngineInited;
    }

    private void OnDisable ()
    {
        Engine.OnInitialized -= HandleEngineInited;

        if (player != null)
        {
            player.OnActionExecutionFinish -= RecordActionExecuted;
            player.OnAutoPlay -= RecordAutoPlay;
            player.OnSkip -= RecordSkip;
            player.OnStop -= RecordStop;
        }
    }

    private void HandleEngineInited ()
    {
        player = Engine.GetService<NovelScriptPlayer>();

        GameAnalytics.Initialize();
        player.OnActionExecutionFinish += RecordActionExecuted;
        player.OnAutoPlay += RecordAutoPlay;
        player.OnSkip += RecordSkip;
        player.OnStop += RecordStop;
    }

    private void RecordActionExecuted (NovelAction action)
    {
        if (action is null) return;

        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, action.ScriptName ?? "Unknown script", action.LineNumber.ToString(), action.InlineIndex.ToString());
    }

    private void RecordAutoPlay (bool enable)
    {
        GameAnalytics.NewDesignEvent("Auto Play", enable ? 1 : 0);
    }

    private void RecordSkip (bool enable)
    {
        GameAnalytics.NewDesignEvent("Skip", enable ? 1 : 0);
    }

    private void RecordStop ()
    {
        GameAnalytics.NewDesignEvent("Stop (Finished Playing)");
    }
}
