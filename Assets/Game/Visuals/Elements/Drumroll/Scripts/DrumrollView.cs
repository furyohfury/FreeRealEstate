using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Visuals
{
	public sealed class DrumrollView : ElementView
	{
		[SerializeField]
		private Transform _leftBorder;
		[SerializeField]
		private Transform _rightBorder;
		[SerializeField]
		private Transform _middle;
		[SerializeField]
		private Transform _scorePointsContainer;
		[SerializeField]
		private GameObject _scorePointPrefab;

		private const float PPU = 100;

		[Button]
		public void SetLength(float length)
		{
			SetPositions(length);
			SetMiddleScale();
		}

		private void SetPositions(float length)
		{
			_rightBorder.position = _leftBorder.position + transform.right * length;
			_middle.position = (_rightBorder.position + _leftBorder.position) / 2;
		}

		[Button]
		public void SetScorePoints(int number)
		{
			if (number < 2)
			{
				throw new ArgumentException("Drumroll cant have less than 2 scorepoints");
			}
			Debug.Log($"Set {number} of points to drumroll");

			var distance = Vector3.Distance(_rightBorder.position, _leftBorder.position);
			var distanceBetweenPoints = distance / (number - 1);
			for (int i = 1; i < number - 1; i++)
			{
				Instantiate(
					_scorePointPrefab,
					_leftBorder.position + transform.right * distanceBetweenPoints * i,
					Quaternion.identity,
					_scorePointsContainer
				);
			}
		}

		private void SetMiddleScale()
		{
			var distance = Vector3.Distance(_rightBorder.position, _leftBorder.position);
			var middleScale = _middle.localScale;
			middleScale.x = PPU * distance;
			_middle.localScale = middleScale;
		}

		[Button]
		private void ClearScorePoints()
		{
			Transform[] scorepoints = _scorePointsContainer.GetComponentsInChildren<Transform>();
			for (int i = 0, count = scorepoints.Length; i < count; i++)
			{
				Destroy(scorepoints[i]);
			}
		}
	}
}