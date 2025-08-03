using UnityEngine;
using System.Collections.Generic;

public class NPCSpawner : MonoBehaviour
{
    [System.Serializable]
    public class PrefabData
    {
        public string infoKey;
        public GameObject prefab;
        public bool isSitting;
    }

    [System.Serializable]
    public class SittingPointData
    {
        public Transform point;
        public bool shouldFlipX;
    }

    public List<PrefabData> prefabMappings;

    public List<SittingPointData> sittingPoints;
    public Transform[] standingPoints;

    private Dictionary<string, PrefabData> infoToPrefab;

    public static List<EnemyType> NpcsToSpawn = new(); // Veriyi dışarıdan alıyorsun

    void Awake()
    {
        // Mapping hazırlığı
        infoToPrefab = new Dictionary<string, PrefabData>();
        foreach (var data in prefabMappings)
        {
            infoToPrefab[data.infoKey] = data;
        }

        List<string> incomingInfos = new();
        foreach (var enemyType in NpcsToSpawn)
        {
            string key = EnemyTypeToInfoKey(enemyType);
            if (key != null)
                incomingInfos.Add(key);
        }

        incomingInfos.Add("Npc1");
        incomingInfos.Add("Npc2");

        SpawnNPCs(incomingInfos);
        NpcsToSpawn.Clear();
    }

    private string EnemyTypeToInfoKey(EnemyType type)
    {
        return type switch
        {
            EnemyType.NormalAdam => "NormalAdam",
            EnemyType.Tazi => "Tazi",
            EnemyType.MuscleMan => "MuscleMan",
            EnemyType.HatBoi => "HatBoi",
            EnemyType.FanBoi => "FanBoi",
            EnemyType.Grandma => "Grandma",
            EnemyType.LariyeCroft => "LariyeCroft",
            _ => null,
        };
    }

    public void SpawnNPCs(List<string> incomingInfos)
    {
        List<SittingPointData> sittingAvailable = new(sittingPoints);
        List<Transform> standingAvailable = new(standingPoints);

        Shuffle(sittingAvailable);
        Shuffle(standingAvailable);

        foreach (string info in incomingInfos)
        {
            if (!infoToPrefab.ContainsKey(info))
            {
                Debug.LogWarning($"Prefab eşleşmesi bulunamadı: {info}");
                continue;
            }

            PrefabData data = infoToPrefab[info];

            Transform spawnPoint = null;
            bool flipX = false;

            if (data.isSitting && sittingAvailable.Count > 0)
            {
                SittingPointData sittingData = sittingAvailable[0];
                spawnPoint = sittingData.point;
                flipX = sittingData.shouldFlipX;
                sittingAvailable.RemoveAt(0);
            }
            else if (!data.isSitting && standingAvailable.Count > 0)
            {
                spawnPoint = standingAvailable[0];
                standingAvailable.RemoveAt(0);
            }

            if (spawnPoint != null)
            {
                GameObject npc = Instantiate(data.prefab, spawnPoint.position, spawnPoint.rotation);

                if (data.isSitting && flipX)
                {
                    Vector3 scale = npc.transform.localScale;
                    scale.x *= -1f;
                    npc.transform.localScale = scale;
                }
            }
            else
            {
                Debug.LogWarning($"Yeterli alan yok veya pozisyonlar dolu: {info}");
            }
        }
    }

    void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rand = Random.Range(i, list.Count);
            (list[i], list[rand]) = (list[rand], list[i]);
        }
    }
}
