using UnityEngine;

namespace Commons
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                // 1. _instance가 null인지 확인하고 null이면 존재하는지 검색해보고 없으면 새로 만들어 줌.
                if (_instance == null)
                {
                    // 1-1. _instance의 T타입을 가진 첫 번째 로드된 오브젝트를 가져옴.
                    _instance = FindObjectOfType<T>();

                    // 1-2. T타입의 오브젝트가 없을 때 새로 생성. 
                    // 오브젝트 생성되는 위치는 현재 기본값으로 되어있음. 
                    if (_instance == null)
                    {
                        GameObject obj = new GameObject
                        {
                            name = typeof(T).Name
                        };
                        _instance = obj.AddComponent<T>();
                    }
                }

                return _instance;
            }
        }


        public virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}