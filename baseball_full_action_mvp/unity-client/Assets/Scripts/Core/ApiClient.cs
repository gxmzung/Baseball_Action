using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace BaseballGame.Core
{
    public class ApiClient : MonoBehaviour
    {
        public string baseUrl = "http://localhost:8080";

        public IEnumerator GetPlayers()
        {
            using (UnityWebRequest request = UnityWebRequest.Get(baseUrl + "/api/players"))
            {
                yield return request.SendWebRequest();
                Debug.Log(request.result == UnityWebRequest.Result.Success ? request.downloadHandler.text : request.error);
            }
        }
    }
}
