using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FCL.Misc
{
    public enum PhoneType
    {
        Mobile = 0
    }
    public class MiscUtil
    {
        public static string GenerateRandomNumberCode(int length)
        {
            // Step 1: Generate random number strings used to seek for one number as a number in the result.
            var rangeRandom = new Random((int)DateTime.Now.Ticks);
            string result = string.Empty;
            var rangeMaxs = new int[length];

            for (int index = 0; index < length; index++)
            {
                rangeMaxs[index] = rangeRandom.Next(10000 * index, int.MaxValue);
            }



            // Step 2: Seek for a number from the range number string as a number in the result.
            foreach (var range in rangeMaxs)
            {
                string rangeString = range.ToString();
                var positionRandom = new Random();
                int seekPosition = positionRandom.Next(rangeString.Length);

                result += rangeString.Substring(seekPosition, 1);
            }

            return result;
        }

        public static bool ValidatePhoneNumber(string phoneNumber, PhoneType phoneType = PhoneType.Mobile)
        {
            string pattern = string.Empty;
            switch (phoneType)
            {
                case PhoneType.Mobile:
                    pattern = "^1[0-9]{10}$";
                    break;
                default:
                    break;
            }

            return Regex.IsMatch(phoneNumber, pattern);
        }
    }
}
