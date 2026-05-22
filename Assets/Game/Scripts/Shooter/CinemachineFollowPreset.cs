using System;
using TriInspector;
using Unity.Cinemachine;
using UnityEngine;
// Используй Unity.Cinemachine для новых версий или Cinemachine для старых

public class CinemachineFollowPreset : MonoBehaviour
{
    [SerializeField]
    private string _name;
    [Header("Component Reference")]
    [SerializeField]
    private CinemachineThirdPersonFollow _thirdPersonFollow;

    [Header("Stored Preset Data")]
    [ReadOnly] [SerializeField]
    private bool _hasSavedData = false;

    // Сюда будут копироваться все параметры из CinemachineThirdPersonFollow
    [SerializeField]
    private FollowData _savedData;

    [Serializable]
    public struct FollowData
    {
        public float DampingX;
        public float DampingY;
        public float DampingZ;

        public Vector3 ShoulderOffset;
        public Vector3 HandOffset;
        public float CameraDistance;

        public float CameraSide;
        public float CameraForward;

        public LayerMask ObstacleLayers;
        public float CameraRadius;
        public float VerticalArmLength;
    }

    [Button("Save Parameters")]
    public void Save()
    {
        if (_thirdPersonFollow == null)
        {
            Debug.LogError("CinemachineThirdPersonFollow component is not assigned!", this);
            return;
        }

        // Копируем значения из компонента Cinemachine в нашу сериализуемую структуру
        _savedData.DampingX = _thirdPersonFollow.Damping.x;
        _savedData.DampingY = _thirdPersonFollow.Damping.y;
        _savedData.DampingZ = _thirdPersonFollow.Damping.z;

        _savedData.ShoulderOffset = _thirdPersonFollow.ShoulderOffset;
        _savedData.VerticalArmLength = _thirdPersonFollow.VerticalArmLength;
        _savedData.CameraDistance = _thirdPersonFollow.CameraDistance;
        _savedData.CameraSide = _thirdPersonFollow.CameraSide;

        _hasSavedData = true;
        Debug.Log("Cinemachine Third Person Follow parameters successfully saved!", this);
    }

    [Button("Load Parameters")]
    public void Load()
    {
        if (_thirdPersonFollow == null)
        {
            Debug.LogError("CinemachineThirdPersonFollow component is not assigned!", this);
            return;
        }

        if (!_hasSavedData)
        {
            Debug.LogWarning("No saved data found to load!", this);
            return;
        }

        // Записываем сохраненные значения обратно в компонент Cinemachine
        _thirdPersonFollow.Damping = new Vector3(_savedData.DampingX, _savedData.DampingY, _savedData.DampingZ);

        _thirdPersonFollow.ShoulderOffset = _savedData.ShoulderOffset;
        _thirdPersonFollow.CameraDistance = _savedData.CameraDistance;
        _thirdPersonFollow.CameraSide = _savedData.CameraSide;
        _thirdPersonFollow.VerticalArmLength = _savedData.VerticalArmLength;

        Debug.Log("Cinemachine Third Person Follow parameters successfully loaded!", this);
    }
}
