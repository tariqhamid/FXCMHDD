using System;

namespace HDD
{
    public class Data
    {
        private double myAskOpen;
        private double myAskHigh;
        private double myAskLow;
        private double myAskClose;
        private double myBidOpen;
        private double myBidHigh;
        private double myBidLow;
        private double myBidClose;
        private int myVolume;
        private DateTime myStartTime;

        public Data(double askopen, double askhigh, double asklow, double askclose, 
            double bidopen, double bidhigh, double bidlow, double bidclose, 
            DateTime startTime, int volume)
        {
            myAskOpen = askopen;
            myAskHigh = askhigh;
            myAskLow = asklow;
            myAskClose = askclose;
            myBidOpen = bidopen;
            myBidHigh = bidhigh;
            myBidLow = bidlow;
            myBidClose = bidclose;
            myStartTime = startTime;
            myVolume = volume;
        }
        ~Data()
        {
            //Console.WriteLine("Des");
        }
        public double AskOpen
        {
            get { return myAskOpen; }
            set { myAskOpen = value; }
        }
        public double AskHigh
        {
            get { return myAskHigh; }
            set { myAskHigh = value; }
        }
        public double AskLow
        {
            get { return myAskLow; }
            set { myAskLow = value; }
        }
        public double AskClose
        {
            get { return myAskClose; }
            set { myAskClose = value; }
        }
        public double BidOpen
        {
            get { return myBidOpen; }
            set { myBidOpen = value; }
        }
        public double BidHigh
        {
            get { return myBidHigh; }
            set { myBidHigh = value; }
        }
        public double BidLow
        {
            get { return myBidLow; }
            set { myBidLow = value; }
        }
        public double BidClose
        {
            get { return myBidClose; }
            set { myBidClose = value; }
        }
        public int Volume
        {
            get { return myVolume; }
            set { myVolume = value; }
        }
        public DateTime StartTime
        {
            get { return myStartTime; }
            set { myStartTime = value; }
        }
    }
}
