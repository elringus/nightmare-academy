using Naninovel;
using System.Linq;
using UnityEngine;

public class AutoLoadAfterInit : MonoBehaviour
{
    private void OnEnable ()
    {
        Engine.OnInitialized += HandleEngineInitialized;
    }

    private void OnDisable ()
    {
        Engine.OnInitialized -= HandleEngineInitialized;
    }

    private async void HandleEngineInitialized ()
    {
        var stateManager = Engine.GetService<StateManager>();

        // Start a new game if no saves exist.
        if (!stateManager.AnyGameSaveExists)
        {
            Engine.GetService<CustomVariableManager>()?.ResetLocalVariables();
            var player = Engine.GetService<NovelScriptPlayer>();
            var startScriptName = Configuration.LoadOrDefault<ScriptsConfiguration>().StartGameScript;
            await stateManager.LoadDefaultEngineStateAsync(() => player.PreloadAndPlayAsync(startScriptName));
            return;
        }

        // Load last saved game.
        var slots = (await stateManager.GameStateSlotManager.LoadAllSaveSlotsAsync()).Where(kv => kv.Value != null);
        var recentSlot = slots.OrderByDescending(kv => kv.Value.SaveDateTime).FirstOrDefault();
        var recentQuickSlot = stateManager.QuickLoadAvailable ? await stateManager.GameStateSlotManager.LoadAsync(stateManager.LastQuickSaveSlotId) : null;
        if (recentQuickSlot is null || (recentSlot.Value != null && recentSlot.Value.SaveDateTime > recentQuickSlot.SaveDateTime))
            await stateManager.LoadGameAsync(recentSlot.Key);
        else await stateManager.QuickLoadAsync();
        Engine.GetService<NovelScriptPlayer>().Play();
    }
}
