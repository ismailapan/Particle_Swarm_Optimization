using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Particle_Swarm_Opti
{
    public class Particle
    {
        public float[] Position {  get; set; }
        public float[] Velocity { get; set; }
        public float[] PBestPosition { get; set; }
        public float PBestValue { get; set; } // En iyi konumdaki değeri
        //Problem boyutu birden fazla parçacıka ait değerler 1 den fazla olduğu için dizi kullandım.
        public Particle(int dimension) 
        {
            Position = new float[dimension];
            Velocity = new float[dimension];
            PBestPosition = new float[dimension];
            PBestValue = float.MaxValue;
        }
    }
}
