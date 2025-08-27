using System.Xml;
using UnityEngine;

public class GenericPartFactory<T> : IFactory<T> where T : Component
{
    private PartDB db;

    public GenericPartFactory(PartDB database)
    {
        db = database;
    }
    public void Build(T entity, string id)
    {
        for(int i = 0; i < entity.transform.childCount; i++)
        {
            entity.transform.GetChild(i).gameObject.SetActive(false);
        }

        Transform existing = entity.transform.Find(id);
        if(existing != null)
        {
            existing.gameObject.SetActive(true);
            return;
        }

        GameObject prefab = db.Get(id);
        if(prefab == null)
        {
            Debug.LogWarning("���忡 �������� �������� �ʽ��ϴ�.");
            return;
        }

        GameObject part = GameObject.Instantiate(prefab, entity.transform);
        part.name = id;
    }
}