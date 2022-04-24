using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateManager : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public Rigidbody2D rb;

    // State Machine Setup
    BossBaseState currentState;
    public BossAwakeningState awakeningState = new BossAwakeningState();
    public BossStage1State stage1State = new BossStage1State();
    public BossStage2State stage2State = new BossStage2State();
    public BossDeathState deathState = new BossDeathState();

    [Header("Config")]
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    public List<Pool> pools;
    public int health = 100;

    public float stageStartTime = 0;

    // Drone control
    public float lastDroneSpawnTime = 0f;
    public float lastDroneDeath = 0f;


    // Start is called before the first frame update
    void Start()
    {
        currentState = awakeningState;
        currentState.EnterState(this);

        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        currentState.OnCollisionEnter(this, collision);
    }

    public void SwitchState(BossBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    public GameObject SpawnFromPool(string tag, Vector3 position)
    {
        GameObject spawnedObject = poolDictionary[tag].Dequeue();
        spawnedObject.SetActive(true);
        spawnedObject.transform.position = position;
        poolDictionary[tag].Enqueue(spawnedObject);

        return spawnedObject;
    }
}
