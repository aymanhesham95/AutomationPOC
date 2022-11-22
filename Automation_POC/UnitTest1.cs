using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using WebDriverManager.DriverConfigs.Impl;

namespace Automation_POC
{
    public class Tests
    {
        public IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();

            //Global Implicit Wait 
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [TearDown]
        public void CloseBrowser()
        {
            //driver.Close();
        }


        [Test]
        public void Main()
        {
            //Go to app 
            driver.Url = "https://www.techlistic.com/p/selenium-practice-form.html";

            //Explicit Wait
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Name("firstname")));

            //Enter First Name
            driver.FindElement(By.Name("firstname")).SendKeys("Ayman");

            //Enter Last Name
            driver.FindElement(By.Name("lastname")).SendKeys("Hesham");

            //Enter Gender
            driver.FindElement(By.Id("sex-0")).Click();

            //Enter Years of Experience
            driver.FindElement(By.Id("exp-4")).Click();

            //Enter Date
            driver.FindElement(By.Id("datepicker")).SendKeys("29/01/1995");

            //Select Professions
            driver.FindElement(By.Id("profession-0")).Click();
            driver.FindElement(By.Id("profession-1")).Click();

            //Select Automation Tools
            driver.FindElement(By.Id("tool-2")).Click();

            //Select Continents
            SelectElement ContinentsDropdown = new SelectElement(driver.FindElement(By.Id("continents")));
            ContinentsDropdown.SelectByText("Africa");

            //Select Selenium Commands
            SelectElement SeleniumCommandsDropdown = new SelectElement(driver.FindElement(By.Id("selenium_commands")));
            SeleniumCommandsDropdown.SelectByText("Browser Commands");
            SeleniumCommandsDropdown.SelectByText("Navigation Commands");


            //By _uploadInputFile = XPathMaker("input", "type", "file");
            By _uploadInputFile = By.XPath("//input[contains(@type, 'file')]");
            IWebElement uploadImage = driver.FindElement(_uploadInputFile);
            var imgAttachmentPath = SetDir("Attachments\\AymanTestImage.png");
            uploadImage.SendKeys(imgAttachmentPath);

            //C:\\Users\\Ayman.Hesham\\Downloads\\New folder\\AymanTestImage.PNG
            IWebElement Link =  driver.FindElement(By.LinkText("Click here to Download File"));
            string Actual_URL = Link.GetAttribute("href");
            string Expected_URL = "https://github.com/stanfy/behave-rest/blob/master/features/conf.yaml";
            //Assert.AreEqual(Expected_URL, Actual_URL);
            Assert.That(Actual_URL, Is.EqualTo(Expected_URL));

           
            driver.FindElement(By.Id("submit")).Click();
        }

        [Test]
        public void TestRadioButton()
        {
            driver.Url = "https://qc.neuro-ace.net/";
            driver.FindElement(By.Name("username")).SendKeys("AymanAdmin");
            driver.FindElement(By.Name("password")).SendKeys("P@ssw0rd1995");
            driver.FindElement(By.CssSelector("button[type='Submit']")).Click();

            //Radio Button (1st Method)
            IWebElement radiob =  driver.FindElement(By.XPath("(//input[@id='2'])[1]"));
            radiob.Click();

            //Radio Button (2nd Method)
            IList<IWebElement> radioOptions = driver.FindElements(By.CssSelector("input[type='radio']"));
            foreach (IWebElement radioButton in radioOptions)
            {
                if (radioButton.GetAttribute("id").Equals("2"))
                {
                    radioButton.Click();
                }
            }
        }

        public By XPathMaker(string htmlTag, string attribute, string attributeValue)
        {
            //var thisLock = new object();
            //lock (thisLock)
            //{
            // Example: "//HTMLTag[contains(@Attribute, 'AttributeValue')]"
            // Example: "//input[contains(@id, 'AttributeValue')]"
            // Example: "//input[contains(text(), 'AttributeValue')]"
            var elementBuilder = "//";
            elementBuilder += htmlTag.ToLower().Trim();
            elementBuilder += "[contains(" + (!attribute.ToLower().Contains("text") ? "@" : "");
            elementBuilder += attribute.ToLower().Trim();
            if (attribute.ToLower().Contains("text") && !attribute.ToLower().Contains("()"))
                elementBuilder += "()";
            elementBuilder += ", '";
            elementBuilder += attributeValue;
            elementBuilder += "')]";

            return By.XPath(elementBuilder);
            //}
        }

        /// <summary>
        /// set Directory: Get the full path of certain Folder inside the Project
        /// </summary>
        /// <param name="folderName">Container Folder inside the Project (e.g. Drivers or Attachments)</param>
        /// <returns></returns>
        public static string SetDir(string folderName)
        {
            var thisLock = new object();
            lock (thisLock)
            {
                return Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())) + "\\" + folderName;
            }
        }

        public void CommentedCode()
        {
            /* 
            TestContext.Progress.WriteLine(driver.Title);
            TestContext.Progress.WriteLine(driver.Url);

            xPath --> //tagname[@Attribute= 'Value']
             Ex: driver.FindElement(By.XPath("//input[@id='firstname']"));
                                             //label[@Class='text-info']/span/input

            CSS --> tagname[Attribute = 'Value'] or #id 
             Ex: driver.FindElement(By.CssSelector("input[id='firstname']"));
                                           .text-info span:nth-child(1) input
            */
        }

    }
}