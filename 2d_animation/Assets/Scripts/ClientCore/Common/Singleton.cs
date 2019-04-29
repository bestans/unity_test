using System;
using System.Collections.Generic;
using System.Reflection;

namespace ClientCore
{
    public class TSingleton<T>
    {
        static object SyncRoot = new object();
        static T s_Instance;
        public static readonly Type[] EmptyTypes = new Type[0];
        public static T Singleton
        {
            get
            {
                if (null == s_Instance)
                {
                    lock (SyncRoot)
                    {
                        if (null == s_Instance)
                        {
                            ConstructorInfo ci = typeof(T).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, EmptyTypes, null);
                            if (null == ci)
                            {
                                throw new InvalidOperationException("class must contain a private constructor");
                            }
                            s_Instance = (T)ci.Invoke(null);
                        }
                    }
                }
                return s_Instance;
            }
        }
    }
}
