using System;
using System.ComponentModel;
using System.Reflection;
using System.Collections.Generic;

namespace Core.Enums
{
    public static class EnumUtils
    {
        public static string ValueOf(Enum e)
        {
            FieldInfo fieldInfo = e.GetType().GetField(e.ToString());
            DescriptionAttribute attribute = (DescriptionAttribute)fieldInfo.GetCustomAttribute(typeof(DescriptionAttribute));
            return attribute.Description;
        }

        public static object EnumValueOf(string value, Type enumType)
        {
            string[] names = Enum.GetNames(enumType);
            foreach (string name in names)
            {
                if (ValueOf((Enum)Enum.Parse(enumType, name)).Equals(value))
                    return Enum.Parse(enumType, name);
            }
            throw new ArgumentException("Строка не описана или не задана.");
        }

        public static string[] CollectionValueOf(Type enumType)
        {
            Array enums = Enum.GetValues(enumType);
            string[] values = new string[enums.Length];
            int i = 0;
            foreach (object val in enums)
            {
                values[i] = EnumUtils.ValueOf((Enum)val);
                i++;
            }
            return values;
        }

    }

   /// <summary>
   /// Образование 
   /// </summary>
    public enum Education
    {
        [DescriptionAttribute("Неполное среднее")]
        junior_secondary,
        [DescriptionAttribute("Среднее")]
        high,
        [DescriptionAttribute("Среднее специальное")]
        high_special,
        [DescriptionAttribute("Среднее профессиональное")]
        high_technical,
        [DescriptionAttribute("Неполное высшее")]
        undergraduate,
        [DescriptionAttribute("Высшее")]
        higher
    }

    /// <summary>
    /// Состав семьи
    /// </summary>
   public enum CompositionOfFamily
    {
        [DescriptionAttribute("Полная")]
        full,
        [DescriptionAttribute("Без отца")]
        no_father,
        [DescriptionAttribute("Без мамы")]
        no_mother,
        [DescriptionAttribute("Сирота")]
        orphan
    }

    /// <summary>
    /// Приводы в милицию
    /// </summary>
    public enum Detained
    {
        [DescriptionAttribute("Нет")]
        no,
        [DescriptionAttribute("Мелкое хулиганство")]
        tough,
        [DescriptionAttribute("Алкоголь")]
        alcohol,
        [DescriptionAttribute("Разбой")]
        criminal,
        [DescriptionAttribute("Другое")]
        other
    }

    /// <summary>
    /// Выявленные дефекты
    /// </summary>
    public enum Defect
    {
        [DescriptionAttribute("Нет")]
        no,
        [DescriptionAttribute("Речь")]
        speech,
        [DescriptionAttribute("Тело")]
        body,
        [DescriptionAttribute("Речь и тело")]
        speechAndBody
    }

    /// <summary>
    /// Суициды в семье
    /// </summary>
    public enum SuicideInFamily 
    {
        [DescriptionAttribute("Нет")]
        no,
        [DescriptionAttribute("Были")]
        yes,
        [DescriptionAttribute("Пытался сам")]
        try_himself
    }

    /// <summary>
    /// Методика вычисления
    /// </summary>
    public enum Method
    {
        //[DescriptionAttribute("Pav-1")]
        //pav1 =1,
        [DescriptionAttribute("Pav-2")]
        pav2 = 2
        
    }

    

    /// <summary>
    /// Правила вычисления параметра
    /// </summary>
    public enum RuleInterpretationParameter {none,single, group}

    /// <summary>
    /// Тип интерпретируемого параметра
    /// </summary>
    public enum TypeOfValue { numerical, str }

    /// <summary>
    /// Тип вывода интерпритации теста
    /// </summary>
    public enum ReportType { asString, asChart }

}
