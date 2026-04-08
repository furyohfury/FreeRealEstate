using System;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace Game
{
    public static class WebConfigLoader
    {
        // Метод возвращает десериализованный объект нужного типа
        public static async Awaitable<SessionParamsStorage> LoadConfigAsync(string url)
        {
            using var request = UnityWebRequest.Get(url);

            try
            {
                await request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.Success)
                {
                    string json = request.downloadHandler.text;
                    Debug.Log($"<color=green>Loaded storage</color>");

                    // Удаляем BOM, если он есть
                    if (json.StartsWith("\uFEFF"))
                    {
                        json = json.Substring(1);
                    }

                    if (TryParseStorage(json, out var storage))
                    {
                        return storage;
                    }
                }
                else
                {
                    Debug.LogError($"Network Error: {request.error}");
                    return null;
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                return null;
            }

            return null;
        }

        public static bool TryParseStorage(string sessionStorage, out SessionParamsStorage sessionParamsStorage)
        {
            try
            {
                JsonSerializerSettings settings = new JsonSerializerSettings
                                                  {
                                                      TypeNameHandling = TypeNameHandling.Auto
                                                  };
                var storage = JsonConvert.DeserializeObject<SessionParamsStorage>(sessionStorage, settings);
                Debug.Log($"<color=green>Successfully parsed SessionParamsStorage</color>");
                sessionParamsStorage = storage;
                return true;
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Json parse failure");
                Debug.Log(e);
                sessionParamsStorage = null;
                return false;
            }
        }
    }
}
