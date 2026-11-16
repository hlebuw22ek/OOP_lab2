using System;

namespace OOPLab2
{
    public class MyTime
    {
        private int hour, minute, second;

        public int Hour { get { return hour; } }
        public int Minute { get { return minute; } }
        public int Second { get { return second; } }

        public MyTime(int h, int m, int s)
        {
            if (h < 0 || h > 23 || m < 0 || m > 59 || s < 0 || s > 59)
            {
                throw new ArgumentException("Некоректне значення годин, хвилин або секунд!");
            }
            hour = h;
            minute = m;
            second = s;
        }

        public MyTime(int secondsSinceMidnight)
        {
            int secPerDay = 60 * 60 * 24;
            secondsSinceMidnight %= secPerDay;
            if (secondsSinceMidnight < 0)
            {
                secondsSinceMidnight += secPerDay;
            }

            hour = secondsSinceMidnight / 3600;
            minute = (secondsSinceMidnight / 60) % 60;
            second = secondsSinceMidnight % 60;
        }

        public override string ToString()
        {
            return $"{hour}:{minute:D2}:{second:D2}";
        }

        public int TimeSinceMidnight()
        {
            return hour * 3600 + minute * 60 + second;
        }

        public static MyTime TimeSinceMidnight(int t)
        {
            return new MyTime(t);
        }

        public MyTime AddSeconds(int s)
        {
            int currentSeconds = this.TimeSinceMidnight();
            return new MyTime(currentSeconds + s);
        }

        public MyTime AddOneSecond()
        {
            return AddSeconds(1);
        }

        public MyTime AddOneMinute()
        {
            return AddSeconds(60);
        }

        public MyTime AddOneHour()
        {
            return AddSeconds(3600);
        }

        public int Difference(MyTime other)
        {
            return this.TimeSinceMidnight() - other.TimeSinceMidnight();
        }

        public static int Difference(MyTime mt1, MyTime mt2)
        {
            return mt1.Difference(mt2);
        }

        public string WhatLesson()
        {
            int sec = this.TimeSinceMidnight();

            int[] startH = { 8,  9, 11, 13, 14, 16 };
            int[] startM = { 0, 40, 20,  0, 40, 20 };
            int[] endH   = { 9, 11, 12, 14, 16, 17 };
            int[] endM   = { 20, 0, 40, 20,  0, 40 };

            int firstLessonStart = startH[0] * 3600 + startM[0] * 60;
            int lastLessonEnd = endH[5] * 3600 + endM[5] * 60;

            if (sec < firstLessonStart)
            {
                return "пари ще не почалися";
            }
            if (sec >= lastLessonEnd)
            {
                return "пари вже скінчилися";
            }

            for (int i = 0; i < 6; i++)
            {
                int startSec = startH[i] * 3600 + startM[i] * 60;
                int endSec = endH[i] * 3600 + endM[i] * 60;

                if (sec >= startSec && sec < endSec)
                {
                    return (i + 1) + "-а пара".Replace("3-а", "3-я");
                }

                if (i < 5)
                {
                    int nextStartSec = startH[i + 1] * 3600 + startM[i + 1] * 60;
                    if (sec >= endSec && sec < nextStartSec)
                    {
                        return $"перерва між {i + 1}-ю та {i + 2}-ю парами";
                    }
                }
            }

            return "пари вже скінчилися";
        }
    }
}
