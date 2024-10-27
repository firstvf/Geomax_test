using UnityEngine;

namespace Assets.Project.Scripts
{
    public class Candle : MonoBehaviour
    {
        [SerializeField] private Light _candleLight;
        private ParticleSystem _fireParticle;
        private float _fanZoneTimer;
        private bool _isCandleCovered;

        private void Awake()
        {
            _fireParticle = GetComponentInChildren<ParticleSystem>();
        }

        public void CoverCandle(bool isCovered) => _isCandleCovered = isCovered;

        public void ActivateCandle()
        {
            _fireParticle.gameObject.SetActive(true);
            _candleLight.gameObject.SetActive(true);
        }

        private void DeactivateCandle()
        {
            _fireParticle.gameObject.SetActive(false);
            _candleLight.gameObject.SetActive(false);
        }

        private void OnParticleCollision(GameObject other)
        {
            if (_isCandleCovered) return;

            DeactivateCandle();
        }
        private void OnTriggerStay(Collider other)
        {
            if (_isCandleCovered)
                _fanZoneTimer = 0;
            else
            {
                if (_fanZoneTimer >= 5)
                    DeactivateCandle();

                _fanZoneTimer += Time.deltaTime;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            _fanZoneTimer = 0;
        }
    }
}