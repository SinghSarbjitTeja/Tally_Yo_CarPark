using Carpark_Project.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carpark_Project.Services
{
    public class PriceCalculator
    {
        public PriceDTO GetPrice(DateTime start, DateTime end)
        {
            try
            {
                var model = new PriceDTO();

                //Night Rate
                TimeSpan nightRateEntryStart = new TimeSpan(18, 0, 0);
                TimeSpan nightRateEntryEnd = new TimeSpan(24, 0, 0);
                TimeSpan nightRateExitEnd = new TimeSpan(6, 0, 0);
                if (((start.TimeOfDay > nightRateEntryStart) && (start.TimeOfDay < nightRateEntryEnd))
                    && ((end.TimeOfDay < nightRateExitEnd)))
                {
                    model = new PriceDTO()
                    {
                        Name = "Night Rate",
                        Price = "$6.50"
                    };
                    return model;
                }

                //Weekend Rate
                if ((start.DayOfWeek == DayOfWeek.Saturday || start.DayOfWeek == DayOfWeek.Sunday)
                    && (end.DayOfWeek == DayOfWeek.Saturday || end.DayOfWeek == DayOfWeek.Sunday))
                {
                    TimeSpan weekend = new TimeSpan(24, 0, 0);
                    model = new PriceDTO()
                    {
                        Name = "Weekend Rate",
                        Price = "$10"
                    };

                    return model;
                }

                //For Early Bird
                TimeSpan earlyBirdEntryStart = new TimeSpan(6, 0, 0);
                TimeSpan earlyBirdEntryEnd = new TimeSpan(9, 0, 0);
                TimeSpan earlyBirdExitStart = new TimeSpan(15, 30, 0);
                TimeSpan earlyBirdExitEnd = new TimeSpan(23, 30, 0);

                //For Standard Rate
                TimeSpan oneHour = new TimeSpan(1, 0, 0);
                TimeSpan twoHour = new TimeSpan(2, 0, 0);
                TimeSpan threeHours = new TimeSpan(3, 0, 0);
                var timeFrame = (end.TimeOfDay - start.TimeOfDay);

                if (start.Day == end.Day)
                {
                    //Early Bird
                    if (((start.TimeOfDay > earlyBirdEntryStart) && (start.TimeOfDay < earlyBirdEntryEnd))
                        && ((end.TimeOfDay > earlyBirdExitStart) && (end.TimeOfDay < earlyBirdExitEnd)))
                    {
                        model = new PriceDTO()
                        {
                            Name = "Early Bird",
                            Price = "$13"
                        };
                        return model;
                    }

                    //Standard Rate
                    if (timeFrame < oneHour)
                    {
                        model = new PriceDTO()
                        {
                            Name = "Standard Rate",
                            Price = "$5"
                        };
                        return model;
                    }
                    else if (timeFrame > oneHour && timeFrame < twoHour)
                    {
                        model = new PriceDTO()
                        {
                            Name = "Standard Rate",
                            Price = "$10"
                        };
                        return model;

                    }
                    else if ((timeFrame > twoHour) && (timeFrame < threeHours))
                    {
                        model = new PriceDTO()
                        {
                            Name = "Standard Rate",
                            Price = "$15"
                        };
                        return model;
                    }
                    else
                    {
                        model = new PriceDTO()
                        {
                            Name = "Standard Rate",
                            Price = "$20"
                        };
                        return model;
                    }
                }
                else
                {
                    var numberOfDays = (end.Day - start.Day);
                    model = new PriceDTO()
                    {
                        Name = "Standard Rate",
                        Price = ("$" + (20 + 20 * numberOfDays).ToString())
                    };
                    return model;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
