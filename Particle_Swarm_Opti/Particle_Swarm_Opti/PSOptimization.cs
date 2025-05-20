using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Particle_Swarm_Opti
{
    public class PSOptimization
    {
        public List<Particle> Particles { get; set; }
        public float[] GBestPosition { get; set; }
        public float GBestValue { get; set; }

        private int numParticles, dimension, maxIterations;
        private float c1, c2;
        private float minBound, maxBound;
        private Random rnd = new Random();

        public List<float> convergenceHistory;

        public PSOptimization(int numParticles, int dimension, float c1, float c2, float minBound, float maxBound, int maxIterations)
        {
            this.numParticles = numParticles;
            this.dimension = dimension;
            this.c1 = c1;
            this.c2 = c2;
            this.minBound = minBound;
            this.maxBound = maxBound;
            this.maxIterations = maxIterations;

            Particles = new List<Particle>();
            GBestPosition = new float[dimension];
            GBestValue = float.MaxValue;
            convergenceHistory = new List<float>();

            InitializeParticles();
        }
        private float RandomFloat(float min, float max)
        {
            return (float)(rnd.NextDouble() * (max - min) + min);
        }

        private void InitializeParticles()
        {
            for (int i = 0; i < numParticles; i++)
            {
                Particle p = new Particle(dimension);
                for (int d = 0; d < dimension; d++)
                {
                    p.Position[d] = RandomFloat(minBound, maxBound);
                    p.Velocity[d] = 0f;
                    p.PBestPosition[d] = p.Position[d];
                }
                p.PBestValue = FitnessFunction(p.Position);
                Particles.Add(p);

                //gBEST değeri güncelleme kısmı
                if (p.PBestValue < GBestValue)// Minimizasyon olduğundan kaynaklı şart bu şekil sunuldu.
                {
                    GBestValue = p.PBestValue;
                    Array.Copy(p.Position, GBestPosition, dimension);
                }
            }
        }
        public float FitnessFunction(float[] pos) // Sunumda verilen fitness Fonk.
        {
            float x1 = pos[0];
            float x2 = pos[1];

            return 4 * x1 * x1 - 2.1f * (float)Math.Pow(x1, 4) + (1.0f / 3) * (float)Math.Pow(x1, 6)
                 + x1 * x2 - 4 * x2 * x2 + 4 * (float)Math.Pow(x2, 4);
        }
        public void UpdateParticles()
        {
            foreach (Particle p in Particles)
            {
                for (int d = 0; d < dimension; d++)
                {
                    // Random sayılar oluşturmak
                    float r1 = (float)rnd.NextDouble();
                    float r2 = (float)rnd.NextDouble();

                    // Hız güncelleme formül
                    p.Velocity[d] = p.Velocity[d]
                      + c1 * r1 * (p.PBestPosition[d] - p.Position[d])
                      + c2 * r2 * (GBestPosition[d] - p.Position[d]);
                    // KOnum güncelleyen förmül
                    p.Position[d] += p.Velocity[d];

                    if (p.Position[d] < minBound)
                        p.Position[d] = minBound;
                    if (p.Position[d] > maxBound)
                        p.Position[d] = maxBound;
                }
                float newFitness = FitnessFunction(p.Position);

                if (newFitness < p.PBestValue)
                {
                    p.PBestValue = newFitness;
                    Array.Copy(p.Position,p.PBestPosition,dimension);
                }
                if (p.PBestValue < GBestValue)
                {
                    GBestValue = p.PBestValue;
                    Array.Copy(p.PBestPosition, GBestPosition, dimension);  
                }
            }
            convergenceHistory.Add(GBestValue); // GRAFİKTE KAYIT TUTMAK İÇİN VAR.
        }
        public void Run()
        {
            for (int i = 0; i < maxIterations; i++)
            {
                UpdateParticles();
            }
        }
        public float[] GetBestPosition() => GBestPosition;
        public float GetBestValue() => GBestValue;
        public List<float> GetConvergenceData() => convergenceHistory;
    }
}
