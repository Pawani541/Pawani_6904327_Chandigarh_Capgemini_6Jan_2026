using System;

namespace StudentRegistrationSystem
{
    internal class Fees
    {
        // Encapsulation: private fields
        private double baseFee;
        private double concession;
        private double transportFee;
        private double lateFee;
        private double gstAmount;
        private double finalFee;

        // Setter: receives all fee components & calculates final fee
        public void SetFees(double baseFee, double concession, double transportFee, double lateFee, double gstPercent)
        {
            // Validation
            if (baseFee < 0) baseFee = 0;
            if (concession < 0) concession = 0;
            if (transportFee < 0) transportFee = 0;
            if (lateFee < 0) lateFee = 0;
            if (concession > baseFee) concession = baseFee;

            this.baseFee = baseFee;
            this.concession = concession;
            this.transportFee = transportFee;
            this.lateFee = lateFee;

            // GST calculate (on base fee)
            gstAmount = (baseFee * gstPercent) / 100;

            // Final Fee Calculation
            finalFee = baseFee - concession + transportFee + lateFee + gstAmount;
        }

        // Getters to get values
        public double GetBaseFee() => baseFee;
        public double GetConcession() => concession;
        public double GetTransportFee() => transportFee;
        public double GetLateFee() => lateFee;
        public double GetGstAmount() => gstAmount;
        public double GetFinalFee() => finalFee;
    }
}
