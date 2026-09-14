using System.Globalization;

namespace DiveDeep.Services
{
    public static class DateRangeParser
    {
        // Modtager "18/09/2026 til 21/09/2026" (eller bare "18/09/2026") fra flatpickr (den datepicker vi bruger).
        // Returnerer false, hvis teksten er tom eller i et forkert format.
        // Returnerer også startTime og endTime via "out" i parameterne
        public static bool TryParse(string? dateRange, out DateTime startTime, out DateTime endTime)
        {
            startTime = DateTime.MinValue;
            endTime = DateTime.MinValue;

            if (string.IsNullOrWhiteSpace(dateRange))
            {
                return false;
            }

            string[] parts = dateRange.Split(" til ");

            if (!DateTime.TryParseExact(parts[0], "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out startTime))
            {
                return false;
            }

            if (parts.Length == 2)
            {
                if (!DateTime.TryParseExact(parts[1], "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out endTime))
                {
                    return false;
                }
            }
            else
            {
                // Hvis der kun er valgt en dag regner vi med at lejen slutter dagen efter.
                // Leje er minimum 24 timer, ellers kan vi ikke markere en dag som optaget
                endTime = startTime.AddDays(1);
            }

            return true;
        }
    }
}