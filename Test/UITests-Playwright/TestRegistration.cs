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
                await using var browser = await playwright.Chromium.LaunchAsync();

                //Page
                var Page = await browser.NewPageAsync();



                //navigate to homepage
                await Page.GotoAsync("https://localhost:7280/");

                //locate to label Signup and click
                await Page.Locator("label=Sign Up").ClickAsync();

                //fill in username
                string userName = "Patrik";
                await Page.Locator("input[type=text]").FillAsync(userName);

                //fill in email
                string email = "patrik@yahoo.com";
                await Page.Locator("input[type=email]").FillAsync(email);

                //fill in password
                string password = "patrik123";
                await Page.Locator("input[type=password]").FillAsync(password);

                //fill in confirm password
                string confirmPassword = "patrik123";
                await Page.Locator("input[type=password]").FillAsync(confirmPassword);

                //submit registration
                await Page.ClickAsync("button[type=submit]");

                var urlAfterClick = "https://localhost:7280/StartPage";
                await Page.GotoAsync(urlAfterClick);

                





                Assert.Multiple(() =>
                {
                    Assert.That(userName, Is.EqualTo("Patrik"));
                    Assert.That(email, Is.EqualTo("patrik@yahoo.com"));
                    Assert.That(password, Is.EqualTo("patrik123"));
                    Assert.That(confirmPassword, Is.EqualTo("patrik123"));
                    Assert.That(urlAfterClick, Is.EqualTo("https://localhost:7280/StartPage"));
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
