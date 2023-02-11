using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace Test
{
    public class TestLoginUI
    {
        [SetUp]
        public void Setup()
        {

        }



        [Test]
        public async Task TestUserLogin()
        {
            try
            {
                //Playwright 
                using var playwright = await Playwright.CreateAsync();

                //Browser
                await using var browser = await playwright.Chromium.LaunchAsync();

                //Page
                var Page = await browser.NewPageAsync();

                //navigate to homepage
                await Page.GotoAsync("https://localhost:7280/");

                var userName = "";
                //fill in username
                await Page.FillAsync("input[type=text]",userName);

                var password = "";
                //fill in password
                await Page.FillAsync("input[type=password]",password);

                //submit registration
                await Page.ClickAsync("button[type=submit]");

                var userLocalStorage=(userName,password);
                await Page.EvaluateAsync("() => window.localStorage.getItem(userLocalStorage)");

                //navigate to startpage
                await Page.GotoAsync("https://localhost:7280/startPage");


                            
                string urlAfterClick = Page.Url;

                Assert.Multiple(() =>
                {
                    Assert.That(userName,Is.EqualTo("Patrik"));
                    Assert.That(password, Is.EqualTo("patrik123"));
                    Assert.That(urlAfterClick, Is.EqualTo("https://localhost:7280/startPage"));
                    Assert.That(userLocalStorage,Is.True);
                });


            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught!! Message: ");

                Console.WriteLine(e.Message);

                Console.WriteLine("StackTrace: " + e.StackTrace);



            }

        }
    }
}
