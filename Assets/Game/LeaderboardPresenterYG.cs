using System;
using UnityEngine;
using UnityEngine.Networking;
using YG;
using YG.Utils.LB;

namespace Game
{
    public sealed class LeaderboardPresenterYG : LeaderboardPresenter
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

        public override void ShowLeaderboard()
        {
            string sessionParamsId = GameParamsService.Instance.SessionParams.Id;
            sessionParamsId = SessionParamsToLeaderboardIdConverter.Convert(sessionParamsId);
            _leaderboardYG.gameObject.SetActive(true);
            _leaderboardYG.nameLB = sessionParamsId;
            _leaderboardYG.UpdateLB();
            YG2.GetLeaderboard(sessionParamsId);
        }

        public override void HideLeaderboard()
        {
            _leaderboardYG.gameObject.SetActive(false);
        }

        private async void OnGetLeaderboard(LBData data)
        {
            bool authed = YG2.player.auth;
            string currentPlayerName = "You";
            Sprite currentPlayerPic = _anonymousPlayerSprite;

            if (authed)
            {
                currentPlayerName = YG2.player.name;
                currentPlayerPic = await DownloadSpriteAsync(YG2.player.photo);
            }

            LBCurrentPlayerData currentPlayerData = data.currentPlayer;
            LBPlayerDataYG.TextMP textMp = _currentPlayerDataYG.textMP;
            textMp.name.text = currentPlayerName;
            textMp.rank.text = currentPlayerData.rank.ToString();
            string score = LBMethods.TimeTypeConvertStatic(currentPlayerData.score, _leaderboardYG.decimalSize);
            textMp.score.text = score;
            _currentPlayerDataYG.imageLoad.spriteImage.sprite = currentPlayerPic;
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
