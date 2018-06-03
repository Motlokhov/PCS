using System;


namespace Core.Factory
{
    using Test;
    public abstract class TestCreator
    {
        protected static Test[] _tests;
        protected int _testNumber;

        public Test[] GetTests() 
        {
            return _tests;
        }

        public int GetTestNumber() 
        {
            return _testNumber;
        }
    }
}
