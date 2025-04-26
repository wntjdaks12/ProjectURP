using System.Collections.Generic;
using UnityEngine;

public class Spawner : View
{
    [SerializeField] private SpawnerSlot spawnerSlot;
    [SerializeField] private Transform parent;

    private Queue<string> queue;

    [SerializeField] private float toastDelay;
    private float curTime;

    private void Awake()
    {
        queue = new Queue<string>();

        GameApplication.Instance.EntityController.OnEntitySpawn += (entityObj) =>
        {
            if (entityObj.Entity is Monster)
            {
                var textInfo = GameApplication.Instance.GameModel.PresetData.ReturnData<TextInfo>(nameof(TextInfo), entityObj.Entity.Id);

                var str = "";
                if (textInfo != null)
                {
                    str = textInfo.NameKr;
                }

                queue.Enqueue(str);
            }
        };
    }

    private void Update()
    {
        if (queue.Count > 0)
        {
            if (curTime > toastDelay)
            {
                curTime = 0f;
            }

            if (curTime == 0f)
            {
                var str = queue.Dequeue();

                var slot = Instantiate(spawnerSlot, parent);
                slot.Init(str);
            }

            curTime += Time.deltaTime;
        }
    }

    public override void UpdateUI()
    {

    }
}
