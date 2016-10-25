using DecisionDealer.View;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace DecisionDealer
{
    public static class EntryPoint
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }

        private static List<string> captions = new List<string>();
        private static List<double> totals = new List<double>();
        private static List<int> counts = new List<int>();
        private static List<Stopwatch> watches = new List<Stopwatch>();

        public static void StartReporting(int watchIndex)
        {
            while (watchIndex >= watches.Count)
            {
                watches.Add(new Stopwatch());
            }

            watches[watchIndex] = new Stopwatch();
            watches[watchIndex].Start();
        }

        public static void ReportTime(int watchIndex, string caption)
        {
            watches[watchIndex].Stop();
            Debug.WriteLine(caption + " " + watches[watchIndex].Elapsed.TotalMilliseconds + " ms");
        }

        public static void SetCaption(int sectionIndex, string caption)
        {
            while (sectionIndex >= captions.Count)
            {
                captions.Add("");
            }

            captions[sectionIndex] = caption;
        }

        public static void Put(int watchIndex, int sectionIndex)
        {
            while (sectionIndex >= totals.Count)
            {
                totals.Add(0);
                counts.Add(0);
            }

            watches[watchIndex].Stop();
            totals[sectionIndex] += watches[watchIndex].Elapsed.TotalMilliseconds;
            counts[sectionIndex]++;
        }

        public static void ReportSections()
        {
            for (int i = 0; i < totals.Count; i++)
            {
                Debug.WriteLine("ID # " + i);

                if(i < captions.Count)
                {
                    Debug.WriteLine(captions[i]);
                }

                Debug.WriteLine("Total " + totals[i]);
                Debug.WriteLine("SS " + counts[i]);
                Debug.WriteLine("Avg. " + (totals[i] / (double)counts[i]) + " ms\n\n");
            }
        }
    }
}
