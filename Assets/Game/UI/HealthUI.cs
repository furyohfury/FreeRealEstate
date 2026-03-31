using TMPro;
using TriInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public sealed class HealthUI : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _healthText;
        [SerializeField]
        private Health _health;
        [SerializeField]
        private Image _healthBar;
        [Header("Parameters")]
        [SerializeField]
        private float _maxHeight = 0.4f;
        [SerializeField]
        private float _maxSpeed = 0.5f;
        private Material _healthBarMaterial;
        private static readonly int _fillAmount = Shader.PropertyToID("_FillAmount");
        private static readonly int _boilIntensity = Shader.PropertyToID("_BoilIntensity");
        private static readonly int _boilSpeed = Shader.PropertyToID("_BoilSpeed");

        private void Awake()
        {
            _healthBarMaterial = _healthBar.material;
        }

        private void OnEnable()
        {
            _health.OnHealthChanged += HealthOnOnHealthChanged;
        }

        private void Start()
        {
            HealthOnOnHealthChanged(_health.CurrentHealth);
        }

        [Button]
        private void HealthOnOnHealthChanged(float hp)
        {
            _healthText.text = hp.ToString();

            float reverseRatio = 1 - hp / _health.MaxHealth;
            _healthBarMaterial.SetFloat(_fillAmount, reverseRatio);
            _healthBarMaterial.SetFloat(_boilIntensity, Mathf.Lerp(0, _maxHeight, reverseRatio));
            _healthBarMaterial.SetFloat(_boilSpeed, Mathf.Lerp(0, _maxSpeed, reverseRatio));
        }

        private void OnDisable()
        {
            _health.OnHealthChanged -= HealthOnOnHealthChanged;
        }
    }
}
