using UnityEngine;
using System.Collections.Generic;

public class NPCSpawner : MonoBehaviour
{
    [System.Serializable]
    public class PrefabData
    {
        public string infoKey;
        public GameObject prefab;
        public bool isSitting; // Bu prefab oturuyor mu?
    }

    public List<PrefabData> prefabMappings;

    public Transform[] sittingPoints;
    public Transform[] standingPoints;

    private Dictionary<string, PrefabData> infoToPrefab;

    void Awake()
    {
        infoToPrefab = new Dictionary<string, PrefabData>();
        foreach (var data in prefabMappings)
        {
            infoToPrefab[data.infoKey] = data;
        }
    }

    public void SpawnNPCs(List<string> incomingInfos)
    {
        List<Transform> sittingAvailable = new List<Transform>(sittingPoints);
        List<Transform> standingAvailable = new List<Transform>(standingPoints);

        Shuffle(sittingAvailable);
        Shuffle(standingAvailable);

        foreach (string info in incomingInfos)
        {
            if (!infoToPrefab.ContainsKey(info))
            {
                Debug.LogWarning($"Bilgi bulunamadý: {info}");
                continue;
            }

            PrefabData data = infoToPrefab[info];
            Transform spawnPoint = null;

            if (data.isSitting && sittingAvailable.Count > 0)
            {
                spawnPoint = sittingAvailable[0];
                sittingAvailable.RemoveAt(0);
            }
            else if (!data.isSitting && standingAvailable.Count > 0)
            {
                spawnPoint = standingAvailable[0];
                standingAvailable.RemoveAt(0);
            }

            if (spawnPoint != null)
            {
                Instantiate(data.prefab, spawnPoint.position, spawnPoint.rotation);
            }
            else
            {
                Debug.LogWarning($"Yeterli alan yok: {info}");
            }
        }
    }

    void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            T temp = list[i];
            int rand = Random.Range(i, list.Count);
            list[i] = list[rand];
            list[rand] = temp;
        }
    }
}
