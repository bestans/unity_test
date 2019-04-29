using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ClientCore {
    //
    public class RAudio : RResource<RAudio> {
        public const string kAudioPath = "Audio/";

        private Dictionary<string, AudioClip> effectDict = new Dictionary<string, AudioClip>();

        private RAudio() {
        }

        public AudioClip Load(string name, int type) {
            return Load(null, name, type);
        }

        public AudioClip Load(string path, string name, int type) {
            AudioClip audio = base.Load(Fullname(kAudioPath + path, name), typeof(AudioClip)) as AudioClip;

            if (audio == null) {
                return null;
            }
            return audio;
        }

        public void Clear() {
            effectDict.Clear();
        }

    }
}