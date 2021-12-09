using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace SimpleDataViewerTest
{
    public static class FileHelper
    {
        public static (List<double> xValues, List<double> yValues) ParseFile(string filePath) //tuple
        {
            var xList = new List<double>();
            var yList = new List<double>();
            foreach (string line in File.ReadLines(filePath))
            {
                var s = line.Split(new []{ '\t', ',', ';', ' ' },StringSplitOptions.RemoveEmptyEntries);
                
                if (double.TryParse(s[0], NumberStyles.Float, CultureInfo.InvariantCulture, out  double x) && 
                    double.TryParse(s[1], NumberStyles.Float, CultureInfo.InvariantCulture, out double y))
                {
                    xList.Add(x);
                    yList.Add(y);
                }
            }
            return (xList, yList);
        }
    }
}
