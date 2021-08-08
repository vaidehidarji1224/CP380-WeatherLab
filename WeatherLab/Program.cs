using System;
using System.Linq;


namespace WeatherLab
{
    class Program
    {
        static string dbfile = @".\data\climate.db";

        static void Main(string[] args)
        {
            var measurements = new WeatherSqliteContext(dbfile).Weather;

            var total_2020_precipitation = measurements.Where(val => val.year == 2020).Select(s => s.precipitation).Sum();
            Console.WriteLine($"Total precipitation in 2020: {total_2020_precipitation} mm\n");

            //
            // Heating Degree days have a mean temp of < 18C
            //   see: https://en.wikipedia.org/wiki/Heating_degree_day
            //

            // ?? TODO ??

            //
            // Cooling degree days have a mean temp of >=18C
            //

            var degree = measurements.GroupBy(r => r.year).Select(d => new
            {
                year = d.Key,
               
            });

            var degree1 = measurements.GroupBy(r => r.year >2015).Select(d => new
            {
               
                hdd = d.Where(val => val.meantemp < 18).Count(),
               
            });

            // Console.WriteLine("Year\tHDD\tCDD");
            var degree2 = measurements.GroupBy(r => r.year > 2015).Select(d => new
            {

                cdd = d.Where(val => val.meantemp >= 18).Count()
            });
           
          
            Console.WriteLine("Year\tHDD\tCDD");


            // if(degree = j )
           //year
           foreach (var j in degree)
            {
                Console.WriteLine($"{ j.year }");
            }
           //hdd
            foreach (var j in degree1)
            {
                Console.WriteLine($"{ j.hdd }");
            }
            //cdd
            foreach (var j in degree2)
            {
                Console.WriteLine($"{ j.cdd }");
            }
            // To display as "0002" then:
            //      $"{x:d4}"
            //

            Console.WriteLine("\nTop 5 Most Variable Days");
            Console.WriteLine("YYYY-MM-DD\tDelta");

            //TODO
            var vdays = measurements  .Select(v => new
                    {
                        date = $"{v.year}-{v.month:d2}-{v.day:d2}",
                        delta = (v.maxtemp - v.mintemp)
                    })
                    .OrderByDescending(r => r.delta);

            int count = 0;
            foreach (var i in vdays)
            {
                if (count < 5)
                {
                    Console.WriteLine($"{i.date}\t{i.delta}");
                    count++;
                }
                else
                {
                    break;
                }
                //
                // Most Variable days are the days with the biggest temperature
                // range. That is, the largest difference between the maximum and
                // minimum temperature
                //
                // Oh: and number formatting to zero pad.
                // 
                // For example, if you want:
                //      var x = 2;
                // To display as "0002" then:
                //      $"{x:d4}"
                //        
            }
        }

    }
}
