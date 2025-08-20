using System;
using System.Linq;
using UnityEngine;

namespace Game.ElementHandle
{
	[CreateAssetMenu(fileName = nameof(JudgementSettings), menuName = nameof(ElementHandle) + "/" + nameof(JudgementSettings))]
	public class JudgementSettings : ScriptableObject
	{
		[SerializeField]
		private JudgementSetting[] _settings;

		private void OnValidate()
		{
			_settings = _settings.OrderBy(setting => setting.ClickWindowFraction).ToArray();
		}

		public JudgementResult GetScore(float offset, float clickWindow)
		{
			var fraction = offset / clickWindow;
			for (int i = 0, count = _settings.Length; i < count; i++)
			{
				if (fraction <= _settings[i].ClickWindowFraction)
				{
					var judgementResult = new JudgementResult(_settings[i].Type);
					return judgementResult;
				}
			}

			throw new ArgumentException();
		}
	}
}