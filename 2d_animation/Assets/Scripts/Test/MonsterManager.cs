using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClientCore;

public class MonsterManager : TSingleton<MonsterManager>
{
    public struct PosVector
    {
        public int x;
        public int y;

        public PosVector(int argx, int argy)
        {
            x = argx;
            y = argy;
        }

        public override bool Equals(object obj)
        {
            var other = (PosVector)obj;
            return other.x == x && other.y == y;
        }
        public override int GetHashCode()
        {
            return x * 10000 + y;
        }
    };
    public Dictionary<PosVector, SceneGameObject> monsterMap = new Dictionary<PosVector, SceneGameObject>();

    private MonsterManager()
    {

    }
    public void Add(PosVector index, SceneGameObject obj)
    {
        if (monsterMap.ContainsKey(index))
        {
            Debug.LogError("duplicat index:x=" + index.x + ",y=" + index.y);
            return;
        }
        monsterMap.Add(index, obj);
    }
    public void Add(int x, int y, SceneGameObject obj)
    {
        Add(new PosVector(x, y), obj);
    }
    
}
