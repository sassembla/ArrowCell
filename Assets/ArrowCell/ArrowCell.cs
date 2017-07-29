using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
	娘さん生誕記念リリース。


	コンポーネント間メッセージング機構
	Editorでのvisualize機構

	コンポーネント間メッセージング
		・起点から自分以外の下位にあるGameObjectのコンポーネントを探し、返す
		・起点ベースのキャッシュを可能にする

	Editor機構
		・Editorで、特定の名前をコード上から指定しているArrowSell使用箇所を探す(コード -> AST -> という感じか。なんかDLLから出せそう。)
		・Inspectorで表示
		・設定ファイルみたいなやつにコンパイル後に漁るやつ書いてそこに情報ファイル吐いて云々でもいいかも。

	制約
		・コンポーネントに対してしか使えない(Child依存)
		・距離制約がない(上以外の無限に遠くも打てる)
		・対象が複数見つかった場合困る(困るが、最初の一つを狙うので気付けない。)

	なんとかしたいところ
		・Obj名への依存(コンパイラブルでない、チェックが難しい、など。) -> Objの名前以外のコンパイラブルな要素に依存するとか？
			ああでもvisualizerで解決できそうな気がする。
		・一度目が遅い問題 -> 予測解決とかできるんかな？まあしないけど。

 */
namespace ArrowCellCore {
	public static class ArrowCell {
		private static Dictionary<string, Component> cacheDict = new Dictionary<string, Component>();

		/*
			gameObject名 x 型でコンポーネントをキャッシュしておいて返す。
		 */
		public static void GetRemoteComponent<T> (this GameObject obj, string remoteGameObjectName, Action<T> loaded) where T : Component {
			var key = remoteGameObjectName + "_" + typeof(T);
			if (cacheDict.ContainsKey(key) && cacheDict[key] != null) {
				loaded(cacheDict[key] as T);
				return;
			}

			// not cached.

			var targetChildComponent = FindChildRecursive<T>(obj.transform, remoteGameObjectName);
			if (targetChildComponent != null) {
				cacheDict[key] = targetChildComponent;

				loaded(cacheDict[key] as T);
				return;
			}

			// not found.

			Debug.LogError("failed to found component:" + typeof(T) + " from gameObject:" + remoteGameObjectName);
		}

		private static T FindChildRecursive<T> (Transform parentTrans, string remoteGameObjectName) where T : Component {
			foreach (Transform t in parentTrans) {
				if (t.gameObject.name == remoteGameObjectName) {
					return t.gameObject.GetComponent<T>() as T;
				}

				var childResult = FindChildRecursive<T>(t, remoteGameObjectName);
				if (childResult != null) {
					return childResult;
				}
			}

			return null;
		}
	}
}