using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    public enum NumberSystemType { bin, oct, dec, hex };

    public class SystemNum
    {
        private string value;
        private NumberSystemType type;

        public SystemNum(string value, NumberSystemType type)
        {
            this.value = value;
            this.type = type;
        }

        public string Verbose()
        {
            string typeVerbose = "";
            switch (this.type)
            {
                case NumberSystemType.bin:
                    typeVerbose = "(2)";
                    break;
                case NumberSystemType.oct:
                    typeVerbose = "(8)";
                    break;
                case NumberSystemType.dec:
                    typeVerbose = "(10)";
                    break;
                case NumberSystemType.hex:
                    typeVerbose = "(16)";
                    break;
            }
            return this.value + typeVerbose;
        }

        public string GetValue()
        {
            return this.value;
        }

        public static string DecimalAny(string value, int num)
        {
            int CoolValue = int.Parse(value);
            bool negative = false;

            if (CoolValue < 0)
            {
                CoolValue = Math.Abs(CoolValue);
                negative = true;
            }
            if (CoolValue == 0)
            {
                return "0";
            }

            StringBuilder result = new StringBuilder();
            const string digits = "0123456789ABCDEF";

            while (CoolValue > 0)
            {
                int digit = CoolValue % num;
                result.Insert(0, digits[digit]);
                CoolValue /= num;
            }

            if (negative)
            {
                result.Insert(0, "-");
            }

            return result.ToString();
        }

        public static string AnyDecimal(string value, int num)
        {
            int CoolValue = 0;
            bool negative = false;

            if (value.StartsWith("-"))
            {
                value = value.Substring(1);
                negative = true;
            }
            if (value == "0")
            {
                return "0";
            }

            const string digits = "0123456789ABCDEF";

            foreach (char c in value)
            {
                int digit = digits.IndexOf(char.ToUpper(c));
                if (digit == -1 || digit >= num)
                    return "Неправильный ввод";

                CoolValue = CoolValue * num + digit;
            }

            StringBuilder result = new StringBuilder(CoolValue.ToString());

            if (negative)
            {
                result.Insert(0, "-");
            }

            return result.ToString();
        }

        public SystemNum To(NumberSystemType newType)
        {
            var newValue = this.value;
            if (this.type == NumberSystemType.dec)
            {
                switch (newType)
                {
                    case NumberSystemType.dec:
                        newValue = this.value;
                        break;
                    case NumberSystemType.bin:
                        newValue = DecimalAny(this.value, 2);
                        break;
                    case NumberSystemType.oct:
                        newValue = DecimalAny(this.value, 8);
                        break;
                    case NumberSystemType.hex:
                        newValue = DecimalAny(this.value, 16);
                        break;
                }
            }
            else if (newType == NumberSystemType.dec)
            {
                switch (this.type)
                {
                    case NumberSystemType.dec:
                        newValue = this.value;
                        break;
                    case NumberSystemType.bin:
                        newValue = AnyDecimal(this.value, 2);
                        break;
                    case NumberSystemType.oct:
                        newValue = AnyDecimal(this.value, 8);
                        break;
                    case NumberSystemType.hex:
                        newValue = AnyDecimal(this.value, 16);
                        break;
                }
            }
            else
            {
                newValue = this.To(NumberSystemType.dec).To(newType).value;
            }
            return new SystemNum(newValue, newType);
        }

        public static SystemNum operator +(SystemNum instance, int number)
        {
            int result;
            if (instance.type == NumberSystemType.dec)
            {
                result = int.Parse(instance.value) + number;
            }
            else
            {
                result = int.Parse(instance.To(NumberSystemType.dec).value) + number;
            }

            string final = result.ToString();
            SystemNum goal = new SystemNum(final, NumberSystemType.dec);

            return new SystemNum(goal.To(instance.type).value, instance.type);
        }

        public static SystemNum operator +(int number, SystemNum instance)
        {
            return instance + number;
        }

        public static SystemNum operator *(SystemNum instance, int number)
        {
            int result;
            if (instance.type == NumberSystemType.dec)
            {
                result = int.Parse(instance.value) * number;
            }
            else
            {
                result = int.Parse(instance.To(NumberSystemType.dec).value) * number;
            }

            string final = result.ToString();
            SystemNum goal = new SystemNum(final, NumberSystemType.dec);

            return new SystemNum(goal.To(instance.type).value, instance.type);
        }

        public static SystemNum operator *(int number, SystemNum instance)
        {
            return instance * number;
        }

        public static SystemNum operator -(SystemNum instance, int number)
        {
            int result;
            if (instance.type == NumberSystemType.dec)
            {
                result = int.Parse(instance.value) - number;
            }
            else
            {
                result = int.Parse(instance.To(NumberSystemType.dec).value) - number;
            }

            string final = result.ToString();
            SystemNum goal = new SystemNum(final, NumberSystemType.dec);

            return new SystemNum(goal.To(instance.type).value, instance.type);
        }

        public static SystemNum operator -(int number, SystemNum instance)
        {
            return instance - number;
        }

        public static SystemNum operator /(SystemNum instance, int number)
        {
            int result;
            if (instance.type == NumberSystemType.dec)
            {
                result = int.Parse(instance.value) / number;
            }
            else
            {
                result = int.Parse(instance.To(NumberSystemType.dec).value) / number;
            }

            string final = result.ToString();
            SystemNum goal = new SystemNum(final, NumberSystemType.dec);

            return new SystemNum(goal.To(instance.type).value, instance.type);
        }

        public static SystemNum operator /(int number, SystemNum instance)
        {
            return instance / number;
        }

        public static SystemNum operator +(SystemNum instance1, SystemNum instance2)
        {
            int result = int.Parse(instance1.To(NumberSystemType.dec).value) + int.Parse(instance2.To(NumberSystemType.dec).value);
            SystemNum goal = new SystemNum(result.ToString(), NumberSystemType.dec);
            return new SystemNum(goal.To(instance1.type).value, instance1.type);
        }

        public static SystemNum operator -(SystemNum instance1, SystemNum instance2)
        {
            int result = int.Parse(instance1.To(NumberSystemType.dec).value) - int.Parse(instance2.To(NumberSystemType.dec).value);
            SystemNum goal = new SystemNum(result.ToString(), NumberSystemType.dec);
            return new SystemNum(goal.To(instance1.type).value, instance1.type);
        }

        public static SystemNum operator *(SystemNum instance1, SystemNum instance2)
        {
            int result = int.Parse(instance1.To(NumberSystemType.dec).value) * int.Parse(instance2.To(NumberSystemType.dec).value);
            SystemNum goal = new SystemNum(result.ToString(), NumberSystemType.dec);
            return new SystemNum(goal.To(instance1.type).value, instance1.type);
        }

        public static SystemNum operator /(SystemNum instance1, SystemNum instance2)
        {
            int result = int.Parse(instance1.To(NumberSystemType.dec).value) / int.Parse(instance2.To(NumberSystemType.dec).value);
            SystemNum goal = new SystemNum(result.ToString(), NumberSystemType.dec);
            return new SystemNum(goal.To(instance1.type).value, instance1.type);
        }
    }
}
