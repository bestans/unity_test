using UnityEngine;
using System.Collections;

namespace ClientCore {
    public class RMaterial : RResource<RMaterial> {
        //
        public const string kPrefabPath = "Materials";

        private RMaterial() {

        }

        // 加载Prefab到内存 
        public Material Load(string name) {
            return Load(kPrefabPath, name);
        }

        public Material Load(string path, string name) {
            string fullname = Fullname(path, name);

            Material loadObject = base.Load(fullname, typeof(Material)) as Material;

            return loadObject;
        }
    }
}
