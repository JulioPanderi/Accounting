using Xunit.Abstractions;

namespace UnitTest
{
    public class TestClass
    {
        private ITestOutputHelper output;
        public TestClass(ITestOutputHelper output)
        {
            this.output = output;
        }

        public class ConsoleWriter : StringWriter
        {
            private ITestOutputHelper output;
            public ConsoleWriter(ITestOutputHelper output)
            {
                this.output = output;
            }

            public override void WriteLine(string m)
            {
                output.WriteLine(m);
            }
        }

        [Fact]
        public void TestName()
        {
            Console.SetOut(new ConsoleWriter(output));
            Assert.True(ToBeTested.Foo());
        }



        public class ToBeTested
        {
            public static bool Foo()
            {
                Console.WriteLine("Foo uses Console.WriteLine!!!");
                return true;
            }
        }

    }

}