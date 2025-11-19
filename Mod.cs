using HarmonyLib;

namespace BoardViewKeys;

public class Plugin : Mod {
	public static Dictionary<ViewType, ConfigEntry<string>> ViewKeys = new Dictionary<ViewType, ConfigEntry<string>>();

	public void Awake() {
		InitializeConfig();
		Harmony.PatchAll();
	}

	public override void Ready() {
		Logger.Log($"{this.Manifest.Name} (v{this.Manifest.Version}): Ready!");
	}

	private void InitializeConfig() {
		//? Will assign default keys 4, 5, 6, 7 and 8
		int defaultKey = 4; //* bad, i know, but idc enough right now

		foreach (ViewType type in Enum.GetValues(typeof(ViewType))) {
			ConfigEntry<string> config = Config.GetEntry<string>($"{type} View Key", defaultKey.ToString());

			ViewKeys.Add(type, config);
			defaultKey++;
		}
	}
}

[HarmonyPatch(typeof(WorldManager))]
public static class WorldManagerPatch {
	[HarmonyPostfix, HarmonyPatch(nameof(WorldManager.Update))]
	public static void UpdatePatch(ref WorldManager __instance) {
		string current = InputController.instance.inputString;
		
		if (current == "") {
			return;
		}

		foreach (KeyValuePair<ViewType, ConfigEntry<string>> item in Plugin.ViewKeys) {
			if (current == item.Value.Value) {
				__instance.SetViewType(item.Key);
			}
		}
	}
}