using System;
using UnityEngine;
using UnityEngine.Networking;
using YG;
using YG.Utils.LB;

namespace Game
{
    public sealed class LeaderboardViewYG : LeaderboardView
    {
        [SerializeField]
        private LeaderboardYG _leaderboardYG;
        [SerializeField]
        private LBPlayerDataYG _currentPlayerDataYG;
        [SerializeField]
        private Sprite _anonymousPlayerSprite;

        private void Start()
        {
            YG2.onGetLeaderboard += OnGetLeaderboard;
        }

        public override void UpdateLeaderboard()
        {
            string sessionParamsId = GameParamsService.Instance.SessionParams.Id;
            var lbName = SessionParamsToLeaderboardIdConverter.Convert(sessionParamsId);
            _leaderboardYG.gameObject.SetActive(true);
            _leaderboardYG.nameLB = lbName;
            _leaderboardYG.UpdateLB();
        }

        private async void OnGetLeaderboard(LBData data)
        {
            bool authed = YG2.player.auth;
            string currentPlayerName = "You";
            string score = "--:--";
            string rank = "-";
            Sprite currentPlayerPic = _anonymousPlayerSprite;
            LBPlayerDataYG.TextMP currentPlayerView = _currentPlayerDataYG.textMP;

            if (authed)
            {
                currentPlayerName = YG2.player.name;
                currentPlayerPic = await DownloadSpriteAsync(YG2.player.photo);
                LBCurrentPlayerData currentPlayerData = data.currentPlayer;
                Debug.Log($"<color=green>LeaderboardViewYG: is authed, trying to get currentPlayerData</color>");

                if (currentPlayerData != null)
                {
                    Debug.Log($"<color=green>LeaderboardViewYG: is authed, trying to get currentPlayerData</color>");
                    rank = currentPlayerData.rank.ToString();
                    score = LBMethods.TimeTypeConvertStatic(currentPlayerData.score, _leaderboardYG.decimalSize);
                }
                else
                {
                    Debug.Log($"<color=red>LeaderboardViewYG currentPlayerData == null</color>");
                }
            }
            else
            {
                Debug.Log($"<color=red>LeaderboardViewYG wasnt authed</color>");
            }

            _currentPlayerDataYG.imageLoad.spriteImage.sprite = currentPlayerPic;
            currentPlayerView.rank.text = rank;
            currentPlayerView.name.text = currentPlayerName;
            currentPlayerView.score.text = score;
        }

        private async Awaitable<Sprite> DownloadSpriteAsync(string url)
        {
            Sprite result = null;

            using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(url))
            {
                try
                {
                    // Ожидаем завершения запроса
                    await request.SendWebRequest();

                    if (request.result != UnityWebRequest.Result.Success)
                    {
                        Debug.LogError($"Ошибка WebRequest: {request.error}");
                    }
                    else
                    {
                        // Извлекаем текстуру
                        Texture2D texture = DownloadHandlerTexture.GetContent(request);

                        // Создаем спрайт на основе текстуры
                        // Rect определяет область текстуры, Vector2(0.5f, 0.5f) — это Pivot (центр)
                        result = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError($"Исключение при загрузке спрайта: {e.Message}");
                    return null;
                }

                return result;
            }
        }

        private void OnDestroy()
        {
            YG2.onGetLeaderboard -= OnGetLeaderboard;
        }
    }
}
