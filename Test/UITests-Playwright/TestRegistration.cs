using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace Test.UITests_Playwright
{
    public class TestRegistration
    {
        [Test]
        public async Task TestUserRegistration()
        {
            try
            {
                //Playwright 
                using var playwright = await Playwright.CreateAsync();

                //Browser
                await using var browser = await playwright.Chromium.LaunchAsync((new BrowserTypeLaunchOptions
                {
                    Headless = false // set headless mode to true

                }));

                //Page
                var Page = await browser.NewPageAsync();



                //navigate to homepage
                await Page.GotoAsync("https://nissanchat.azurewebsites.net/");

                //locate to label Signup and click
                var Selector = "label:has-text('Sign Up')";
                await Page.WaitForSelectorAsync(Selector);
                await Page.ClickAsync(Selector);
                //fill in username
                string userName = "Bob";
                await Page.WaitForSelectorAsync("input[type=text]");
                await Page.FillAsync("input[type=text]", userName);

                //fill in email
                string email = "bob@yahoo.com";
                await Page.WaitForSelectorAsync("input[type=email]");
                await Page.FillAsync("input[type=email]", email);

                //fill in password
                string password = "bob123";
                await Page.WaitForSelectorAsync("input[type=password]");
                await Page.FillAsync("input[type=password]", password);

                //fill in confirm password
                string confirmPassword = "bob123";
                await Page.WaitForSelectorAsync("input[type=password]");
                await Page.FillAsync("input[type=password]",confirmPassword);

                //submit registration


                // await Page.WaitForSelectorAsync("button[type=submit]");
                //await Page.ClickAsync("button[type=submit]");
                var buttonSelector = "button:has-text('Sign Up')";
                await Page.WaitForSelectorAsync(buttonSelector);
                await Page.ClickAsync(buttonSelector);

                //go to startpage

                var startPage = "h3:has-text('Start Page')";
                await Page.WaitForSelectorAsync(startPage);
                // var pageSite = await Page.QuerySelectorAsync(page);
               // var newPage=await Page.WaitForSelectorAsync("h3:has-text('StartPage')", new WaitForSelectorOptions { Visible = true });







                Assert.Multiple(() =>
                {
                    Assert.That(userName, Is.EqualTo("Bob"));
                    Assert.That(email, Is.EqualTo("bob@yahoo.com"));
                    Assert.That(password, Is.EqualTo("bob123"));
                    Assert.That(confirmPassword, Is.EqualTo("bob123"));
                    Assert.That(startPage, Is.Not.Null);
                });




            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught!! Error! ");

                Console.WriteLine(e.Message);

                Console.WriteLine("StackTrace: " + e.StackTrace);



            }

        }
    }

    internal class WaitForSelectorOptions : PageWaitForSelectorOptions
    {
        public bool Visible { get; set; }
    }
}
