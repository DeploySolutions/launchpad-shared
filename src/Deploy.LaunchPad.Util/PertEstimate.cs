using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Util
{
    /// <summary>
    /// Encapsulates PERT estimation (Program Evaluation and Review Technique), which requires three core estimates:
    /// Optimistic estimate(O) – the best-case scenario.
    /// Most likely estimate(M) – the normal or expected scenario.
    /// Pessimistic estimate(P) – the worst-case scenario.

    /// From these, the classic PERT formula calculates:
    /// Expected value(E):  E=O+4M+P6E=6O+4M+P​
    /// Standard deviation(SD) : SD=P−O6SD=6P−O​
    /// </summary>
    public partial class PertEstimate
    {
        public virtual double Optimistic { get; }
        
        public virtual double MostLikely { get; }
        
        public virtual double Pessimistic { get; }
        
        public virtual int OptimisticWeight { get; } = 1;
        
        public virtual int MostLikelyWeight { get; } = 4;
        
        public virtual int PessimisticWeight { get; } = 1;

        public virtual int TotalWeight => OptimisticWeight + MostLikelyWeight + PessimisticWeight;

        public virtual double ExpectedValue =>
        (Optimistic * OptimisticWeight + MostLikely * MostLikelyWeight + Pessimistic * PessimisticWeight)
        / TotalWeight;

        public virtual double StandardDeviation =>
            (Pessimistic - Optimistic) / TotalWeight;  // Only valid if using standard PERT-like assumption

        public virtual double Variance => Math.Pow(StandardDeviation, 2);

        public PertEstimate(
            double optimistic,
            double mostLikely,
            double pessimistic,
            int optimisticWeight = 1,
            int mostLikelyWeight = 4,
            int pessimisticWeight = 1
        )
        {
            if (optimistic < 0 || mostLikely < 0 || pessimistic < 0)
                throw new ArgumentOutOfRangeException("Estimates must be non-negative.");

            if (!(optimistic <= mostLikely && mostLikely <= pessimistic))
                throw new ArgumentException("Expected ordering is: Optimistic <= MostLikely <= Pessimistic");

            Optimistic = optimistic;
            MostLikely = mostLikely;
            Pessimistic = pessimistic;

            OptimisticWeight = optimisticWeight;
            MostLikelyWeight = mostLikelyWeight;
            PessimisticWeight = pessimisticWeight;

            if (TotalWeight <= 0)
                throw new ArgumentOutOfRangeException("Sum of weights must be positive.");
        }

        public virtual (double LowerBound, double UpperBound) GetConfidenceInterval(double zScore = 2.0)
        {
            double margin = zScore * StandardDeviation;
            return (ExpectedValue - margin, ExpectedValue + margin);
        }

        public override string ToString()
        {
            return $"PERT Estimate: ExpectedValue = {ExpectedValue:F2}, SD = {StandardDeviation:F2}, Range = [{Optimistic} .. {Pessimistic}]";
        }
    }

}
