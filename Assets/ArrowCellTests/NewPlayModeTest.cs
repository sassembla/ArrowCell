using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using ArrowCellCore;
using UnityEngine.UI;

public class NewPlayModeTest : MonoBehaviour {

	void Start () {
		StartCoroutine(RunOnDefaultScene());
		StartCoroutine(RunOnDefaultScene2());
	}
	
	[UnityTest] public IEnumerator RunOnDefaultScene() {
		var testObject = GameObject.Find("BGImage");
		testObject.GetRemoteComponent<Text>("ItemLabel01", textComp => textComp.text = "changed.");
		yield return null;
	}

	[UnityTest] public IEnumerator RunOnDefaultScene2() {
		var testObject = GameObject.Find("BGImage");
		testObject.GetRemoteComponent<Text>("Detail02", textComp => textComp.text = "changed.");
		yield return null;
	}
}
