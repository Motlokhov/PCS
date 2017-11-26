using System;
using System.ComponentModel;
using System.Reflection;
namespace PCS_Forms
{
    public static class EnumUtils
    {
        public static string ValueOf(Enum value)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            DescriptionAttribute attribute = (DescriptionAttribute)fieldInfo.GetCustomAttribute(typeof(DescriptionAttribute));
            return attribute.Description;
        }

        public static object EnumValueOf(string value, Type enum_type)
        {
            string[] names = Enum.GetNames(enum_type);
            foreach (string name in names)
            {
                if(ValueOf((Enum)Enum.Parse(enum_type,name)).Equals(value))
                    return Enum.Parse(enum_type,name);
            }
            throw new ArgumentException("Строка не описана или не задана.");
        }

        public static string[] CollectionValueOf(Type enum_type)
        {
            Array enums= Enum.GetValues(enum_type);
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
   public enum Composition_of_family
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
    public enum Suicide_in_family 
    {
        [DescriptionAttribute("Нет")]
        no,
        [DescriptionAttribute("Были")]
        yes,
        [DescriptionAttribute("Пытался сам")]
        try_itself
    }

    /// <summary>
    /// Методика вычисления
    /// </summary>
    public enum Method
    {
        [DescriptionAttribute("Pav-1")]
        pav1 =1,
        [DescriptionAttribute("Pav-2")]
        pav2
        
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

    /// <summary>
    /// Как происходит загрузка данных
    /// </summary>
    public enum LoadDataAs { PastTesting, NewTesting }
}
