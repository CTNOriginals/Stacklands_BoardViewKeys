using HarmonyLib;
using System;
using System.Collections;
using UnityEngine;

namespace BoardViewKeys;

public class ExampleMod : Mod {
	public void Awake() {
		Harmony.PatchAll();
	}

	public override void Ready() {
		Logger.Log($"{this.Manifest.Name} (v{this.Manifest.Version}): Ready!");
	}
}