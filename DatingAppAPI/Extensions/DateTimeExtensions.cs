using API.Extensions;

namespace API.Extensions
{
    public static class DateTimeExtensions
    {
        public static int CalculateAge(this DateOnly dob)
        {
            var today = DateOnly.FromDateTime(DateTime.Now);

            var age = today.Year - dob.Year;

            ////if they haven't had their birthday this year yet
            //if (dob > today.AddYears(-age))
            //{
            //    age--;
            //}

            return age;
        }
    }
}