using System.Collections;
using UnityEngine;

public class MANAGER : MonoBehaviour
{
    public static MANAGER Instance = null;
    public static Pool_Mng POOL;
    public static Database_Mng DB;
    public static Session_Mng SESSION;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        POOL = GetComponentInChildren<Pool_Mng>();
        DB = GetComponentInChildren<Database_Mng>();
        SESSION = GetComponentInChildren<Session_Mng>();
    }

    public void Run(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }
}
