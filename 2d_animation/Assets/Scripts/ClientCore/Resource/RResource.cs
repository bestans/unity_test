using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ClientCore {
    public class LoadAsyncResource {
        public ResourceRequest resourceRequest;

        public IEnumerator LoadAsync(string path, string name) {
            string fullname = RPrefab.Fullname(path, name);

            resourceRequest = Resources.LoadAsync(fullname);

            while (!resourceRequest.isDone) {
                yield return 0;
            }

            yield return resourceRequest;
        }
    }

    //
    public abstract class RResource<T> : TSingleton<T> {
        //
        private static StringBuilder fullname = new StringBuilder("");

        public Object Load(string path, System.Type systemTypeInstance) {
            //TODOyyh
            return Resources.Load(path, systemTypeInstance);
            //return RBundleManager.Singleton.LoadResource(path, systemTypeInstance);
        }

        public static string Fullname(string path, string name) {
            if (string.IsNullOrEmpty(path)) {
                return name;
            }

            fullname.Remove(0, fullname.Length);

            fullname.Append(path);
            if (path.LastIndexOf('/') != path.Length - 1) {
                fullname.Append("/");
            }

            fullname.Append(name);

            return fullname.ToString();
        }

        public static string Fullname(string path, string name, string expand) {
            fullname.Remove(0, fullname.Length);

            if (!string.IsNullOrEmpty(path)) {
                fullname.Append(path);
                if (path.LastIndexOf('/') != path.Length - 1) {
                    fullname.Append("/");
                }
            }

            fullname.Append(name);

            if (!string.IsNullOrEmpty(expand)) {
                if (expand.IndexOf('.') != 0) {
                    fullname.Append('.');
                }
                fullname.Append(expand);
            }

            return fullname.ToString();
        }

        public static bool IsValid(string text) {
            return !(string.IsNullOrEmpty(text) || text.Equals("0") || text.Equals("-1"));
        }
    }
}