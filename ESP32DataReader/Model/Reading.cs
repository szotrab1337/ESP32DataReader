using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Migrations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESP32DataReader.Model
{
    public class Reading
    {
        public Reading(string rawData)
        {
            TryParse(rawData);
        }

        public Reading()
        {
        }

        [Key]
        public int ReadingId { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public DateTime ReadAt { get; set; }

        [NotMapped]
        public bool IsGood { get; set; }

        public void TryParse(string rawData)
        {
            try
            {
                string[] arrayData = rawData.Split(';');
                double temperature, humidity;
                IsGood = true;

                NumberStyles style = NumberStyles.AllowDecimalPoint;
                CultureInfo culture = CultureInfo.CreateSpecificCulture("pl-PL");

                if (Double.TryParse(arrayData[0].Replace('.', ','), style, culture, out temperature))
                    Temperature = temperature;
                else
                    IsGood = false;

                if (Double.TryParse(arrayData[1].Replace('.', ','), style, culture, out humidity))
                    Humidity = humidity;
                else
                    IsGood = false;

                ReadAt = DateTime.Now;
            }
            catch (Exception ex)
            {
                Logger.LogMessage("Error occured while parsing raw message.", 3);
                Logger.LogMessage(ex.ToString(), 3);
            }
        }

        public void InsertToDB()
        {
            try
            {
                if (!IsGood)
                    return;

                Logger.LogMessage($"Trying to save new reading in DB. Temperature: {Temperature}, humidity: {Humidity}.");
                using (Context context = new Context())
                {
                    context.Readings.AddOrUpdate(this);
                    context.SaveChanges();
                }
                Logger.LogMessage("Successfuly added new reading to DB.");
            }
            catch (Exception ex)
            {
                Logger.LogMessage("Error occured while parsing raw message.", 3);
                Logger.LogMessage(ex.ToString(), 3);
            }
        }
    }
}
