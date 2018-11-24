using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Challenge
{
	[TestFixture]
	public class WordsStatistics_Tests
	{
		public virtual IWordsStatistics CreateStatistics()
		{
			// меняется на разные реализации при запуске exe
			return new WordsStatistics();
		}

		private IWordsStatistics wordsStatistics;

		[SetUp]
		public void SetUp()
		{
			wordsStatistics = CreateStatistics();
		}

		[Test]
		public void GetStatistics_IsEmpty_AfterCreation()
		{
			wordsStatistics.GetStatistics().Should().BeEmpty();
		}

		[Test]
		public void GetStatistics_ContainsItem_AfterAddition()
		{
			wordsStatistics.AddWord("abc");
			wordsStatistics.GetStatistics().Should().Equal(new WordCount("abc", 1));
		}

		[Test]
		public void GetStatistics_ContainsManyItems_AfterAdditionOfDifferentWords()
		{
			wordsStatistics.AddWord("abc");
			wordsStatistics.AddWord("def");
			wordsStatistics.GetStatistics().Should().HaveCount(2);
		}

        [Test]
		public void GetStatistics_IgnoreWhiteSpaces()
		{
			wordsStatistics.AddWord(" ");
			wordsStatistics.GetStatistics().Should().BeEmpty();
		}

		[Test]
		public void GetStatistics_WordNull()
		{
			Action action = () => { wordsStatistics.AddWord(null); };
			action.ShouldThrow<ArgumentNullException>();
		}

        [Test]
		public void GEtStatistics_NotIgnore10Whitespaces()
		{
			wordsStatistics.AddWord("          a");
			wordsStatistics.GetStatistics().Select(word => word.Word).ShouldBeEquivalentTo(new []{"          "});
		}

		[Test]
		public void GEtStatistics_L3()
		{
			wordsStatistics.AddWord("123456789");
			wordsStatistics.GetStatistics().Select(word => word.Word).ShouldBeEquivalentTo(new[] { "123456789" });
		}

        //[Test]
        //public void GetStatistics_WordJoin()
        //{
        //	wordsStatistics.AddWord("aaaaaaaaaabb");
        //	wordsStatistics.AddWord("aaaaaaaaaacc");
        //	wordsStatistics.GetStatistics().Should().HaveCount(1);
        //}

        [Test]
		public void GetStatistics_WordOrder1()
		{
			wordsStatistics.AddWord("aaa");
			wordsStatistics.AddWord("bbb");
			wordsStatistics.AddWord("bbb");
			wordsStatistics.AddWord("ccc");
			wordsStatistics.GetStatistics().Select(word => word.Word).ShouldBeEquivalentTo(new[] { "bbb","aaa", "ccc" }, options=>options.WithStrictOrdering());
		}

		//[Test]
		//public void GetStatistics_WordOrder2()
		//{
		//	wordsStatistics.AddWord("ccc");
		//	wordsStatistics.AddWord("aaa");
		//	wordsStatistics.AddWord("bbb");
		//	wordsStatistics.GetStatistics().Select(word => word.Word).ShouldBeEquivalentTo(new[] { "aaa", "bbb", "ccc" }, options=>options.WithStrictOrdering());
		//}
		
  //      [Test]
		//public void GetStatistics_Register()
		//{
		//	wordsStatistics.AddWord("abc");
		//	wordsStatistics.AddWord("ABC");
  //          wordsStatistics.GetStatistics().Should().HaveCount(1);
  //      }

		[Test]
		public void GetStatistics_SecondGet()
		{
			wordsStatistics.AddWord("aaa");
			wordsStatistics.GetStatistics().Should().HaveCount(1);
			wordsStatistics.AddWord("bbb");
            wordsStatistics.GetStatistics().Should().HaveCount(2);
		}

		[Test]
		public void GetStatistics_AnotherInstance()
		{
			var statistics = CreateStatistics();
			wordsStatistics.AddWord("aaa");
			wordsStatistics.GetStatistics().Should().HaveCount(1);
			statistics.AddWord("bbb");
			wordsStatistics.GetStatistics().Should().HaveCount(1);
		}

		[Test, Timeout(1500)]
		public void GetStatistics_999()
		{
			for (int i = 0; i < 5000; i++)
			{
				wordsStatistics.AddWord(i.ToString());
			}
			for (int i = 0; i < 5000; i++)
			{
				wordsStatistics.AddWord("1");
			}
            wordsStatistics.GetStatistics().Should().HaveCount(5000);
		}

        [Test]
		public void GetStatistics_QWE()
		{
			int i = 0;
			for (char c = 'а'; c <= 'я'; c++)
			{
				wordsStatistics.AddWord(c.ToString().ToUpper());
                wordsStatistics.AddWord(c.ToString());
				i += 1;
			}
			wordsStatistics.GetStatistics().Should().HaveCount(i);
		}
        // Документация по FluentAssertions с примерами : https://github.com/fluentassertions/fluentassertions/wiki
    }
}