using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ClientCore
{
	//
	public class RTexture : RResource<RTexture>
    {
		private Dictionary<string, Texture> texDict = new Dictionary<string, Texture>();
		private Dictionary<string, Texture2D> tex2Dict = new Dictionary<string, Texture2D>();
		
		private RTexture() {
		}
		
		public Texture Load(string name) {
			return Load("", name);
		}
		
		public Texture Load(string path, string name) {
			if (!texDict.ContainsKey(name)) {
				Texture tex = base.Load(Fullname(path, name), typeof(Texture)) as Texture;
				
				if (tex == null) {
					return null;
				}
				
				texDict[name] = tex;
			}
			
			return texDict[name];
		}
		
		public Texture2D Load2D(string name) {
			return Load2D("tex", name);
		}
		
		public Texture2D Load2D(string path, string name) {
			if (!tex2Dict.ContainsKey(name)) {
				Texture2D tex = base.Load(Fullname(path, name), typeof(Texture2D)) as Texture2D;
				
				if (tex == null) {
					return null;
				}
				
				tex2Dict[name] = tex;
			}
			
			return tex2Dict[name];
		}

        public Sprite LoadSprite(string path, string name) {
            return base.Load(Fullname(path, name), typeof(Sprite)) as Sprite;
        }

        public void Clear() {
            texDict.Clear();
            tex2Dict.Clear();
        }
	}
}