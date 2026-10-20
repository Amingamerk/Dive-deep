using DiveDeep.Lib.Models;

namespace DiveDeep.Services
{
    // Der ligger en metode magen til i API'et, men vi har også en her så vi ikke skal vente på kald til API
    public static class BookingRules
    {
        public static BookingValidationResult ValidateDates(DateTime startTime, DateTime endTime)
        {
            BookingValidationResult result = new();
            result.IsSuccessful = false;

            // tjek at startdato er før slut dato
            if (startTime >= endTime)
            {
                result.Key = "EndTime";
                result.ErrorMessage = "Slut dato skal være efter start dato";
                return result;
            }

            // tjek at startdato ikke er i fortiden
            if (startTime < DateTime.Today)
            {
                result.Key = "StartTime";
                result.ErrorMessage = "Start dato kan ikke være i fortiden";
                return result;
            }

            result.IsSuccessful = true;
            return result;
        }
    }
}
