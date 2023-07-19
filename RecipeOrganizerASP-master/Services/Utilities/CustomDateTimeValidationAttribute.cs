using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Utilities
{
	public class CustomDateTimeValidationAttribute : ValidationAttribute
	{
		private readonly DateTime _minDate;
		private readonly DateTime _maxDate;

		public CustomDateTimeValidationAttribute(int minYear)
		{
			int maxYear = DateTime.Now.Year;
			_minDate = new DateTime(minYear, 1, 1);
			_maxDate = DateTime.Now;
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			DateTime dateTime;
			if (value is DateTime)
			{
				dateTime = (DateTime)value;
			}
			else if (value is string)
			{
				if (!DateTime.TryParse((string)value, out dateTime))
				{
					return new ValidationResult("Invalid date and time format.");
				}
			}
			else
			{
				return new ValidationResult("Invalid date and time value.");
			}

			// Perform validation
			if (dateTime > DateTime.Now)
			{
				return new ValidationResult("Date and time cannot be in the future.");
			}

			if (dateTime < _minDate || dateTime > _maxDate)
			{
                //return new ValidationResult($"Date and time must be between {_minDate.ToShortDateString()} to {_maxDate.ToShortDateString()}.");
                return new ValidationResult($"Date and time must be between {_minDate.Year} to {_maxDate.Year}.");
            }

			return ValidationResult.Success;
		}
	}
}
