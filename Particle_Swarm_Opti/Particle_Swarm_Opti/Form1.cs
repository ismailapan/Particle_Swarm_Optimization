using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Particle_Swarm_Opti
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            int numParticles = int.Parse(txtNumParticles.Text);
            int iterations = int.Parse(txtIterations.Text);
            float c1 = float.Parse(txtC1.Text);
            float c2 = float.Parse(txtC2.Text);
            float min = float.Parse(txtMinBound.Text);
            float max = float.Parse(txtMaxBound.Text);

            PSOptimization pso = new PSOptimization(numParticles, 2, c1, c2, min, max, iterations);
            pso.Run();

            float[] pos = pso.GetBestPosition();
            float bestVal = pso.GetBestValue();

            lblResult.Text = $"En iyi çözüm:\nX1 = {pos[0]:F4}\nX2 = {pos[1]:F4}\nf(x) = {bestVal:F4}";

            chartConvergence.Series.Clear();
            chartConvergence.Series.Add("gBest");
            chartConvergence.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            foreach (float val in pso.GetConvergenceData())
            {
                chartConvergence.Series[0].Points.AddY(val);
            }
        }
    }
}
