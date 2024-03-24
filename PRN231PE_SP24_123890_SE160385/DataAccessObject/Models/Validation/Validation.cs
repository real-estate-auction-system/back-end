using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataAccessObject.Models.Validation
{
    public class Validation
    {
        public class WatercolorsPaintingNameAttribute : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                string tattooStickerName = value as string;
                string pattern = @"^[A-Z][a-zA-Z0-9]";
                if (Regex.IsMatch(tattooStickerName, pattern))
                {
                    return true;
                }
                return false;
            }
        }
        public class PublishyearAttribute : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                if (value is int importYear)
                {

                    if (importYear >= 1000)
                    {
                        return true;
                    }
                }

                return false; 
            }
        }
        public class PriceAttribute : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                if (value is decimal price)
                {

                    if (price > 0)
                    {
                        return true;
                    }
                }

                return false;
            }
        }
    }
}
