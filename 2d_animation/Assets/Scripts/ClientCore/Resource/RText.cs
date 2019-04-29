using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ClientCore
{
	//
    public class RText : RResource<RText>
    {
		private Dictionary<string, TextAsset> textDict = new Dictionary<string, TextAsset>();
		
		private RText() {
			
		}
		
		public TextAsset Load(string name) {
			return Load("text", name);
		}
		
		public TextAsset Load(string path, string name) {
			if (!textDict.ContainsKey(name)) {
				TextAsset asset = base.Load(Fullname("text", name), typeof(TextAsset)) as TextAsset;
				if (asset == null) {
					return null;
				}
				
				textDict[name] = asset;
			}
			
			return textDict[name];
		}
	}
}