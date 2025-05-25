using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class LineRendererController : MonoBehaviour
{
	[SerializeField]
	private Transform[] _transforms;
	[SerializeField]
	private LineRenderer _lineRenderer;

	[Button]
	private void Setup()
	{
		_lineRenderer.positionCount = _transforms.Length;
		UpdatePositions();
	}

	private void LateUpdate()
	{
		UpdatePositions();
	}

	private void UpdatePositions()
	{
		for (int i = 0, count = _transforms.Length; i < count; i++)
		{
			var position = _transforms[i].position;
			_lineRenderer.SetPosition(i, position);
		}
	}
}