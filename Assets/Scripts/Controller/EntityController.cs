using System;
using UnityEngine;

public class EntityController : MonoBehaviour
{
    public Action<EntityObject> OnEntitySpawn; 

    /// <summary>
    /// 엔티티를 생성합니다.
    /// </summary>
    /// <typeparam name="T">엔티티</typeparam>
    /// <param name="id">아이디</param>
    /// <returns>엔티티를 리턴합니다.</returns>
    public T Spawn<T>(int id) where T : Entity
    {
        var entity = GameApplication.Instance.GameModel.RunTimeData.AddData(typeof(T).ToString(), id) as T;
        entity.Init();

        return entity;
    }

    /// <summary>
    /// 엔티티와 엔티티 오브젝트를 생성합니다.
    /// </summary>
    /// <typeparam name="T">엔티티</typeparam>
    /// <typeparam name="K">엔티티 오브젝트</typeparam>
    /// <param name="id">아이디</param>
    /// <param name="position">생성 위치</param>
    /// <param name="rotation">생성 회전</param>
    /// <param name="parent">부모에 생성 여부</param>
    /// <param name="value1">임시 데이터1</param>
    /// <returns>엔티티 오브젝트를 리턴합니다.</returns>
    public K Spawn<T, K>(int id, Vector3 position, Quaternion rotation, Transform parent = null) where T : Entity where K : EntityObject
    {
        var runTimeData = GameApplication.Instance.GameModel.RunTimeData;

        var entity = runTimeData.AddData(typeof(T).ToString(), id) as T;

        var prefabInfo = GameApplication.Instance.GameModel.PresetData.ReturnData<PrefabInfo>(nameof(PrefabInfo), id);

        var entityObj = PoolObjectContainer.CreatePoolObject<K>(prefabInfo.Path);
        entityObj.gameObject.SetActive(false);

        if (rotation != Quaternion.identity) entityObj.transform.rotation = rotation;

        entityObj.transform.position = position;

        if (parent != null) entityObj.transform.SetParent(parent, false);

        entityObj.gameObject.SetActive(true);

        entity.Init(entityObj.transform);
        entityObj.Init(entity);

        entity.OnDataRemove += (data) =>
        {
            RemoveEntity(data);
        };

        runTimeData.AddData($"{typeof(T).Name}Object", entity.InstanceId, entityObj);

        if (entityObj.transform.parent != null)
        {
            var layerName = LayerMask.LayerToName(entityObj.transform.parent.gameObject.layer);
            ChangeLayersRecursively(entityObj.transform, layerName);
        }

        OnEntitySpawn?.Invoke(entityObj);

        return entityObj;
    }

    public void RemoveEntity(IData data)
    {
        var runTimeData = GameApplication.Instance.GameModel.RunTimeData;

        var entityObjectTableName = $"{data.TableModel.TableName}Object";
        var entityObject = runTimeData.ReturnData<EntityObject>(entityObjectTableName, data.InstanceId);

        if (entityObject != null)
        {
            entityObject.OnRemoveEntity();

            runTimeData.RemoveData(data.TableModel.TableName, data.InstanceId);
            runTimeData.RemoveData(entityObjectTableName, data.InstanceId);

            if (entityObject.transform.parent != null) entityObject.transform.SetParent(null, false);
        }
    }

    public void ChangeLayersRecursively(Transform trans, string name)
    {
        trans.gameObject.layer = LayerMask.NameToLayer(name);
        foreach (Transform child in trans)
        {
            ChangeLayersRecursively(child, name);
        }
    }
}
