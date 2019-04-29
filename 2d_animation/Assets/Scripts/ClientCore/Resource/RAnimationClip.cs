using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ClientCore {

    public class RAnimationClip : RResource<RAnimationClip> {
        public const string kAnimationPath = "Animations/";

        private RAnimationClip() {
        }

        public AnimationClip Load(string name) {
            return Load(kAnimationPath, name);
        }

        public AnimationClip Load(string path, string name) {
            AnimationClip clip = base.Load(Fullname(path, name), typeof(AnimationClip)) as AnimationClip;

            if (clip == null) {
                return null;
            }
            return clip;
        }
    }
}