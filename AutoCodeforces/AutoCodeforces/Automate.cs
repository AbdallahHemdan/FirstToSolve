using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace AutoCodeforces
{
    class Program
    {
        static void Main(string[] args)
        {
            Automate e = new Automate();
            //e.OpenCodeforces();
        }
    }

    [TestClass]
    public class Automate
    {
        public IWebDriver chromeDriver = new ChromeDriver();
        [TestMethod]
        public void OpenCodeforces(string code, string ProblemID, string handle = "A4A_A4A", 
            string password = "team_A4A", string LangValue = "54")
        {
            chromeDriver.Navigate().GoToUrl("http://codeforces.com/enter");
            chromeDriver.Manage().Window.Maximize();
            chromeDriver.FindElement(By.Id("handleOrEmail")).SendKeys(handle);
            chromeDriver.FindElement(By.Id("password")).SendKeys(password);
            chromeDriver.FindElement(By.ClassName("submit")).Click();

            for (int i = 0; i < 1000000000; i++) { }

            chromeDriver.Url = "https://codeforces.com/problemset/submit";
            chromeDriver.Navigate();

            chromeDriver.FindElement(By.Name("submittedProblemCode")).SendKeys(ProblemID);

            IWebElement selecElement = chromeDriver.FindElement(By.Name("programTypeId"));
            SelectElement language = new SelectElement(selecElement);
            language.SelectByValue(LangValue);

            chromeDriver.FindElement(By.Id("sourceCodeTextarea")).SendKeys(code);

            chromeDriver.FindElement(By.ClassName("submit")).Click();

            //chromeDriver.Close();
            //chromeDriver.Quit();
        }


        public string SubmissionJsonFile(string handle = "A4A_A4A", int ContestID = 102, int SumbissionCounter = 1)
        {
            //Dummy wait
            for (Int64 i = 0; i < 10000000000; i++) { }
            string ApiUrl =
                string.Format("https://" + "codeforces.com/api/contest.status?contestId={0}&handle={1}&from=1&count={2}", ContestID, handle, SumbissionCounter);
            chromeDriver.Navigate().GoToUrl(ApiUrl);
            string s = chromeDriver.FindElement(By.TagName("pre")).Text;

            chromeDriver.Close();
            //chromeDriver.Quit();

            return s;
        }

    }
}
