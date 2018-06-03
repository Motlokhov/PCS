using System;
using System.Collections.ObjectModel;

namespace Core.Factory
{
    using Test;
    using Limitation;
    using Interpretation;
    

    public class Pav2TestCreator:TestCreator,ITestFactory
    {
        
        public Test[] Create() 
        {
            if (_testNumber < 0) 
            {
                _testNumber = 2;
            }
            if (_tests == null)
            {
                _tests = new Test[8];
                _tests[0] = FirstTest();
                _tests[1] = SecondTest();
                _tests[2] = ThirdTest();
                _tests[3] = FourthTest();
                _tests[4] = FifthTest();
                _tests[5] = SixthTest();
                _tests[6] = SeventhTest();
                _tests[7] = EighthTest();
                //tests[8] = NinthTest();
            }
            return _tests;
        }

        private Test FirstTest() 
        {
            //all parameters but last are same;
            var minMax = MinMax.Construct(0, 8);
            var single = SingleInterpretation.Construct();
            var p1 = Parameter.Conctruct(1, "Словесно-вербальная", 1, minMax, single, null, null);
            var p2 = Parameter.Conctruct(2, "Физическая", 1, minMax, single, null, null);
            var p3 = Parameter.Conctruct(3, "Предметная", 1, minMax, single, null, null);
            var p4 = Parameter.Conctruct(4, "Эмоциональная", 1, minMax, single, null, null);
            var p5 = Parameter.Conctruct(5, "Аутоагрессия", 1, minMax, single, null, null);

            minMax = MinMax.Construct(0, 40);
            var values = new ObservableCollection<Value>();
            values.Add(p1.Values[0]);
            values.Add(p2.Values[0]);
            values.Add(p3.Values[0]);
            values.Add(p4.Values[0]);
            values.Add(p5.Values[0]);
            var meaning = MeaningFromOtherValues.Construct(values);
            Parameter p6 = Parameter.Conctruct(6, "Адаптивность {Автозаполняемый}", 1, minMax, single, null, meaning);

            Test test = Test.Construct(1,"Агрессия",true, new Parameter[] { p1, p2, p3, p4, p5, p6 });
            return test;
        }

        private Test SecondTest() 
        {
            var minMax = MinMax.Construct(0, 13);
            var single = SingleInterpretation.Construct();
            var p1 = Parameter.Conctruct(1, "Искренность", 1, minMax, single, null, null);

            minMax = MinMax.Construct(0, 54);
            var criticalLimin = CriticalLimit.Construct(28);
            var p2 = Parameter.Conctruct(2, "Устойчивость",1,minMax,single,criticalLimin,null);

            var test = Test.Construct(2, "НПУ", true, new Parameter[] {p1,p2 });
            return test;
        }

        private Test ThirdTest() 
        {
            var minMax = MinMax.Construct(0, 24);
            var doubleInterpretation = DoubleInterpretation.Construct();
            var p1 = Parameter.Conctruct(1, "Внутри", 2, minMax, doubleInterpretation, null, null);

            var test = Test.Construct(3, "Характер", true, new Parameter[] {p1 });
            return test;
        }

        private Test FourthTest()
        {
            var minMax = MinMax.Construct(0, 40);
            var single = SingleInterpretation.Construct();
            var p1 = Parameter.Conctruct(1, "Личная", 1, minMax, single, null, null);

            var test = Test.Construct(4, "Тревожность", true, new Parameter[] { p1 });
            return test;
        }

        private Test FifthTest() 
        {
            var minMax = MinMax.Construct(0, 20);
            var single = SingleInterpretation.Construct();
            var p1 = Parameter.Conctruct(1, "Потенциал", 1, minMax, single, null, null);

            var test = Test.Construct(5, "Самоорганизация", true, new Parameter[] { p1 });
            return test;
        }

        private Test SixthTest()
        {
            var minMax = MinMax.Construct(0, 10);
            var single = SingleInterpretation.Construct();
            var p1 = Parameter.Conctruct(1, "Склонность", 1, minMax, single, null, null);

            var test = Test.Construct(6, "Поведение", true, new Parameter[] { p1 });
            return test;
        }

        private Test SeventhTest() 
        {
            var xor = Xor.Construct(new string[]{"П","п","Л","л"});
            var quadruple = QuadrupleInterpretation.Construct();
            var p1 = Parameter.Conctruct(1, "Мозг", 4, xor, quadruple, null, null);

            var test = Test.Construct(7, "Активность", true, new Parameter[] { p1 });
            return test;
        }

        private Test EighthTest()
        {
            var minMax = MinMax.Construct(0, 7);
            var doubleInterpretation = DoubleInterpretation.Construct();
            var p1 = Parameter.Conctruct(1, "Симпатия", 2, minMax, doubleInterpretation, null, null);
            var p2 = Parameter.Conctruct(2, "Антипатия", 2, minMax, doubleInterpretation, null, null);

            var test = Test.Construct(8, "Цвет", false, new Parameter[] { p1, p2 });
            return test;
        }

        private Test NinthTest() 
        {
            var minMax = MinMax.Construct(0, 500);
            
            var p1 = Parameter.Conctruct(1, "Квадраты", 6, minMax, null, null, null);

            var test = Test.Construct(9, "Теппинг", true, new Parameter[] { p1 });
            return test;
        }
    }
}
