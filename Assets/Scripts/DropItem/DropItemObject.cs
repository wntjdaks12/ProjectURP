using UnityEngine;
using System.Linq;

public class DropItemObject : EntityObject
{
    [SerializeField] private MeshRenderer meshRenderer;

    private float curTime;
    [SerializeField] private float pickupAvailableTime;
    [SerializeField] private float pickupSpeed;

    public bool IsPickupAvailable { get; private set; }

    public Transform Target { get; set; }

    private DropItemData dropItemData;
    public DropItemData DropItemData 
    {
        get { return dropItemData; }
        set 
        {
            if (dropItemData != value)
            {
                dropItemData = value;

                RefreshData();
            }
        }
    }

    public override void Init(Entity entity)
    {
        base.Init(entity);

        curTime = 0;

        IsPickupAvailable = false;

        Target = null;
    }

    private void Update()
    {
        transform.LookAt(Camera.main.transform); // 빌보드 효과

        if(!IsPickupAvailable) CheckPickupAvailable(); // 드랍 아이템을 주울 수 있는 상태인지 체크

        if (IsPickupAvailable && Target != null)
        {
            OnPickup(); // 아이템 줍기
            CheckRemove(); // 아이템 삭제
        }
    }

    private void RefreshData()
    {
        var iconInfo = GameApplication.Instance.GameModel.PresetData.ReturnData<IconInfo>(nameof(IconInfo), dropItemData.DropId);

        meshRenderer.material.SetTexture("_BaseMap", Resources.Load<Texture>(iconInfo.Path));
    }

    // 드랍 아이템을 주울 수 있는 상태인지 체크
    private void CheckPickupAvailable()
    {
        if (curTime >= pickupAvailableTime)
        {
            IsPickupAvailable = true;

            // 가장 가까운 타겟으로 설정
            var heroObjs = GameApplication.Instance.GameModel.RunTimeData.ReturnDatas<HeroObject>(nameof(HeroObject));

            var target = heroObjs.OrderBy(x => Vector3.Distance(x.transform.position, transform.position)).FirstOrDefault();
            if (target != null) Target = target.transform;
        }
        else
        {
            curTime += Time.deltaTime;
        }
    }

    private void OnPickup()
    {
        transform.position = Vector3.Lerp(transform.position, Target.transform.position, Time.deltaTime * pickupSpeed);
    }

    private void CheckRemove()
    {
        if (Vector3.Distance(transform.position, Target.transform.position) < 1f)
        {
            Entity.OnRemoveData();
        }
    }
}
