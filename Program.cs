using System.Collections.Generic;
using System.Threading;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;


namespace SeleniumBot
{
    class Program
    {
        static void Main(string[] args)
        {
            InstaBot bot = new InstaBot();
            bot.createAcct();
        }
    }



    class InstaBot
    {

        IWebDriver driver;
        Dictionary<int, string> acctList;
        Dictionary<string, string> passwordStorage;
        int accountNumber = 0;

        public InstaBot()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            acctList = new Dictionary<int, string>();
            passwordStorage = new Dictionary<string, string>();
        }

        public void createAcct()
        {
            createEmail();
            string username = "user" + new Random().Next().ToString();
            acctList.Add(accountNumber, username);
            accountNumber++;
            driver.FindElement(By.Name("fullName")).SendKeys("David Purdy"); // TODO: ADD A CSV FILE READER WITH A WHOLE BUNCH OF RANDOM NAMES
            driver.FindElement(By.Name("username")).SendKeys(username);
            string password = "password" + new Random().Next().ToString();
            passwordStorage.Add(username, password);
            driver.FindElement(By.Name("password")).SendKeys(password);
            Thread.Sleep(1000);
            driver.FindElement(By.Name("password")).SendKeys(Keys.Enter);

            var list = driver.FindElement(By.CssSelector("select[title='Year:']"));
            SelectElement dropDown = new SelectElement(list);
            dropDown.SelectByText("1988");
            driver.FindElement(By.CssSelector("button[class='sqdOP  L3NKy _4pI4F  y3zKF     '")).Click();

            driver.FindElement(By.TagName("body")).SendKeys(Keys.Control + "t");

        }
        public void createEmail()
        {

            driver.Navigate().GoToUrl("https://temp-mail.org/en/");
            driver.FindElement(By.Id("click-to-refresh"));
            Thread.Sleep(5000);
            string s = driver.FindElement(By.Id("mail")).GetAttribute("value");
            driver.Navigate().GoToUrl("https://www.instagram.com/accounts/emailsignup/");
            driver.FindElement(By.Name("emailOrPhone")).SendKeys(s);
        }
    }
}
