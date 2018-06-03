using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.ObjectModel;
using Core.Interpretation;
using Core.Limitation;
using Core.Test;
using Core.Person;
using Core.Factory;

namespace CoreTest
{
    [TestClass]
    public class CreateTest
    {
        [TestMethod]
        public void Pav2Create()
        {
            Test[] tests = new Pav2TestCreator().Create();
            Assert.AreEqual(8, tests.Length);
            Assert.AreEqual(1, tests[0].ID);
            Assert.IsFalse(tests[7].ShouldValuesBeRepeated);
        }

       
    }
}
