using UnityEngine;
using Random = System.Random;

namespace Project.Scripts.Fractures
{
    public class FractureThis : MonoBehaviour
    {
        [SerializeField] private Anchor anchor = Anchor.Bottom;
        [SerializeField] private int chunks = 500;
        [SerializeField] private float density = 50;
        [SerializeField] private float internalStrength = 100;

        [SerializeField] private Material insideMaterial;
        [SerializeField] private Material outsideMaterial;
        [SerializeField] private ParticleSystem destructionParticleSystem; // New field for the particle system

        private Random rng = new Random();

        private void Start()
        {
            FractureGameobject();
            gameObject.SetActive(false);
        }

        public ChunkGraphManager FractureGameobject()
        {
            var seed = rng.Next();
            var chunkGraphManager = Fracture.FractureGameObject(
                gameObject,
                anchor,
                seed,
                chunks,
                insideMaterial,
                outsideMaterial,
                internalStrength,
                density
            );

            if (destructionParticleSystem != null)
            {
                // Instantiate and enable the particle system at the position of the fractured object
                ParticleSystem particleInstance = Instantiate(destructionParticleSystem, transform.position, Quaternion.identity);
                particleInstance.Play();
            }

            return chunkGraphManager;
        }
    }
}
