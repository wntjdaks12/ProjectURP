using UnityEngine;

public class EntityObject : PoolableObject
{
    [field : SerializeField] public Animator animator { get; private set; }
    [field: SerializeField] public AnimationHandler animationHandler { get; private set; }
    [field: SerializeField] public Rigidbody Rigidbody { get; private set; }

    private MaterialUtility materialUtility;

    public Entity Entity { get; private set; }

    public GameObject localInstance { get; set; }

    private Coroutine changeMatAsync;

    public virtual void Init(Entity entity)
    {
        Entity = entity;

        if (materialUtility == null)
        {
            materialUtility = new MaterialUtility();
            materialUtility.Init(transform);
        }

        if (Entity.Lifetime != 0 && gameObject.activeInHierarchy) StartCoroutine(Entity.StartLifeTime());
    }

    public void ChangeMaterial(float time)
    {
        if (gameObject.activeInHierarchy)
            changeMatAsync = StartCoroutine(materialUtility.ChangeMaterial(time));
    }

    public void OnRemoveEntity()
    {
        ReturnPoolableObject();
    }

    private void OnDisable()
    {
        if (changeMatAsync != null)
        {
            StopCoroutine(changeMatAsync);

            changeMatAsync = null;
        }
    }
}
