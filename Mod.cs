using HarmonyLib;

namespace BoardViewKeys;

public class Plugin : Mod {
	public void Awake() {
		Harmony.PatchAll();
	}

	public override void Ready() {
		Logger.Log($"{this.Manifest.Name} (v{this.Manifest.Version}): Ready!");
	}
}