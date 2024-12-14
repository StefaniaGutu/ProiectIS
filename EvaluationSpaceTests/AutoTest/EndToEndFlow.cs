using Azure;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Newtonsoft.Json.Serialization;
using NUnit.Framework;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using System.Xml.Linq;

namespace EvaluationSpaceTests.AutoTest
{
    public class EndToEndFlow
    {
        IWebDriver driver = new ChromeDriver();

        private static Random random = new Random();
        private static readonly string teacherUser = "teacher" + RandomString(3);
        private static readonly string studentUser = "student" + RandomString(3);
        private static readonly string deployedUrl = "http://localhost:4200";
        private static readonly string teacherUserEmail = teacherUser + "@test.local";
        private static readonly string studentUserEmail = studentUser + "@test.local";
        private static readonly string pass = "12wq!@WQ12wq!@WQ";
        private static readonly string quizName = "quiz" + RandomString(3);

        [Test, Order(1)]
        public void RegisterTeacher()
        {

            driver.Navigate().GoToUrl(deployedUrl);
            driver.Manage().Window.Maximize();

            TimeSpan ts = TimeSpan.FromSeconds(20);
            WebDriverWait wait = new WebDriverWait(driver, ts);

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath("/html/body/app-root/app-navbar/nav/div/a/b")));
            driver.FindElement(By.XPath("//*[@id=\"navbarSupportedContent\"]/div/ul/li[2]/a")).Click();

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath("/html/body/app-root/app-register/div/form/input[1]")));
            driver.FindElement(By.XPath("/html/body/app-root/app-register/div/form/input[1]")).SendKeys("TeacherTestUserFirstName");
            driver.FindElement(By.XPath("/html/body/app-root/app-register/div/form/input[2]")).SendKeys("TeacherTestUserLastName");
            driver.FindElement(By.XPath("/html/body/app-root/app-register/div/form/input[3]")).SendKeys(teacherUserEmail);
            driver.FindElement(By.XPath("/html/body/app-root/app-register/div/form/input[4]")).SendKeys(pass);
            driver.FindElement(By.XPath("/html/body/app-root/app-register/div/form/input[5]")).SendKeys(pass);
            driver.FindElement(By.XPath("/html/body/app-root/app-register/div/div[2]/div[2]/input")).Click();

            driver.FindElement(By.XPath("//*[@id=\"mat-select-2\"]/div/div[2]")).Click();
            driver.FindElement(By.XPath("//*[@id=\"mat-option-4\"]")).Click();
            driver.FindElement(By.XPath("//*[@id=\"mat-option-4\"]")).SendKeys(Keys.Escape);

            driver.FindElement(By.XPath("/html/body/app-root/app-register/div/div[3]/div[1]/button")).Click();
        }

        [Test, Order(2)]
        public void RegisterStudent()
        {

            TimeSpan ts = TimeSpan.FromSeconds(20);
            WebDriverWait wait = new WebDriverWait(driver, ts);

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath("/html/body/app-root/app-navbar/nav/div/a/b")));
            driver.FindElement(By.XPath("//*[@id=\"navbarSupportedContent\"]/div/ul/li[2]/a")).Click();

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath("/html/body/app-root/app-register/div/form/input[1]")));
            driver.FindElement(By.XPath("/html/body/app-root/app-register/div/form/input[1]")).SendKeys("StudentTestUserFirstName");
            driver.FindElement(By.XPath("/html/body/app-root/app-register/div/form/input[2]")).SendKeys("StudentTestUserLastName");
            driver.FindElement(By.XPath("/html/body/app-root/app-register/div/form/input[3]")).SendKeys(studentUserEmail);
            driver.FindElement(By.XPath("/html/body/app-root/app-register/div/form/input[4]")).SendKeys(pass);
            driver.FindElement(By.XPath("/html/body/app-root/app-register/div/form/input[5]")).SendKeys(pass);

            driver.FindElement(By.XPath("/html/body/app-root/app-register/div/mat-form-field/div/div[1]")).Click();
            driver.FindElement(By.XPath("/html/body/div[2]/div[2]/div/div/div/mat-option[2]/span")).Click();

            driver.FindElement(By.XPath("/html/body/app-root/app-register/div/div[3]/div[1]/button")).Click();
        }

        [Test, Order(3)]
        public void LoginTeacher()
        {

            TimeSpan ts = TimeSpan.FromSeconds(20);
            WebDriverWait wait = new WebDriverWait(driver, ts);

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath("/html/body/app-root/app-login/div/form/input[1]")));
            driver.FindElement(By.XPath("/html/body/app-root/app-login/div/form/input[1]")).SendKeys(teacherUserEmail);
            driver.FindElement(By.XPath("/html/body/app-root/app-login/div/form/input[2]")).SendKeys(pass);
            driver.FindElement(By.XPath("/html/body/app-root/app-login/div/div[2]/div[1]/button")).Click();

        }

        [Test, Order(4)]
        public void CreateQuiz()
        {
            TimeSpan ts = TimeSpan.FromSeconds(20);
            WebDriverWait wait = new WebDriverWait(driver, ts);
            Thread.Sleep(1000);

            driver.FindElement(By.XPath("/html/body/app-root/app-navbar/nav/div/div[1]/ul/li")).Click();
            driver.FindElement(By.XPath("/html/body/app-root/app-navbar/nav/div/div[1]/ul/li/ul/li[1]/a")).Click();
            driver.FindElement(By.XPath("/html/body/app-root/app-quiz-teacher/div/mat-card/form/mat-form-field[1]/div/div[1]/div/input")).SendKeys(quizName);
            driver.FindElement(By.XPath("/html/body/app-root/app-quiz-teacher/div/mat-card/form/mat-form-field[2]/div/div[1]/div/input")).SendKeys(DateTime.Now.ToString((DateTime.Now.Date).ToString("dd/MM/yyyy")));
            driver.FindElement(By.XPath("/html/body/app-root/app-quiz-teacher/div/mat-card/form/mat-form-field[2]/div/div[1]/div/input")).SendKeys(Keys.ArrowRight);
            driver.FindElement(By.XPath("/html/body/app-root/app-quiz-teacher/div/mat-card/form/mat-form-field[2]/div/div[1]/div/input")).SendKeys(DateTime.Now.ToString((DateTime.Now).ToString("HH:mm")));

            driver.FindElement(By.XPath("/html/body/app-root/app-quiz-teacher/div/mat-card/form/mat-form-field[3]/div/div[1]/div/mat-select/div/div[1]/span")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("/html/body/div[2]/div[2]/div/div/div/mat-option/span")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("/html/body/app-root/app-quiz-teacher/div/mat-card/form/div[1]/div/ol/li/div/div/mat-form-field[1]/div/div[1]/div/input")).SendKeys(Keys.Escape);
            driver.FindElement(By.XPath("/html/body/app-root/app-quiz-teacher/div/mat-card/form/div[1]/div/ol/li/div/div/mat-form-field[1]/div/div[1]/div/input")).SendKeys("test");
            driver.FindElement(By.XPath("/html/body/app-root/app-quiz-teacher/div/mat-card/form/div[1]/div/ol/li/div/div/mat-form-field[2]/div/div[1]/div/input")).SendKeys("3");
            driver.FindElement(By.XPath("/html/body/app-root/app-quiz-teacher/div/mat-card/form/d iv[1]/div/ol/li/div/div/mat-form-field[3]/div/div[1]/div/mat-select/div/div[1]/span")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("/html/body/div[2]/div[2]/div/div/div/mat-option[4]/span")).Click();
            driver.FindElement(By.XPath("/html/body/app-root/app-quiz-teacher/div/mat-card/form/div[1]/div/ol/li/div/div/div/div/div/ul/li[1]/div/mat-form-field/div/div[1]/div/mat-radio-button/label/span[1]")).Click();
            Thread.Sleep(500);
            driver.ExecuteJavaScript("window.scrollBy(0, 400);");
            Thread.Sleep(500);
            driver.FindElement(By.XPath("/html/body/app-root/app-quiz-teacher/div/mat-card/form/div[2]/button")).Click();
        }

        [Test, Order(5)]
        public void LogoutTeacher()
        {
            TimeSpan ts = TimeSpan.FromSeconds(20);
            WebDriverWait wait = new WebDriverWait(driver, ts);
            Thread.Sleep(1000);
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath("/html/body/app-root/app-navbar/nav/div/div[2]/div/ul/li[1]/a")));
            driver.FindElement(By.XPath("/html/body/app-root/app-navbar/nav/div/div[2]/div/ul/li[1]/a")).Click();
        }

        [Test, Order(6)]
        public void LoginStudent()
        {

            TimeSpan ts = TimeSpan.FromSeconds(20);
            WebDriverWait wait = new WebDriverWait(driver, ts);

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath("/html/body/app-root/app-login/div/form/input[1]")));
            driver.FindElement(By.XPath("/html/body/app-root/app-login/div/form/input[1]")).SendKeys(studentUserEmail);
            driver.FindElement(By.XPath("/html/body/app-root/app-login/div/form/input[2]")).SendKeys(pass);
            driver.FindElement(By.XPath("/html/body/app-root/app-login/div/div[2]/div[1]/button")).Click();

            driver.Quit();
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}
