using System.Collections.Generic;
using UnityEngine;

public class GamePoolManager : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag = string.Empty;
        public GameObject gameObject = null;
        public int size = 0;
    }

    public static GamePoolManager instance { private set; get; }

    public List<Pool> pools = new List<Pool>();
    private Dictionary<string, Queue<GameObject>> _poolByName = new Dictionary<string, Queue<GameObject>>();

    private void Awake()
    {
        // First destroy any existing instance of it
        if (instance != null)
        {
            Destroy(instance);
        }

        // Then reassign a proper one
        instance = this;
    }

    private void Start()
    {
        foreach(Pool pool in pools)
        {
            Queue<GameObject> queue = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject gameObject = Instantiate(pool.gameObject, transform);

                gameObject.SetActive(false);
                queue.Enqueue(gameObject);
            }

            _poolByName.Add(pool.tag, queue);
        }
    }

    public GameObject UseFromPool(string name)
    {
        UnityEngine.Assertions.Assert.IsTrue(_poolByName.ContainsKey(name), "Can't find any pool of object " + name);

        GameObject gameObject = _poolByName[name].Dequeue();

        _poolByName[name].Enqueue(gameObject);

        return gameObject;
    }
}
