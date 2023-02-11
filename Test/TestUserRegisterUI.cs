using Microsoft.Playwright;



namespace Test
{
    public class TestUserRegisterUI
    {
         [SetUp]
         public void Setup()
         {

         }

        
       
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
                    await Page.Locator("input[type=text]").FillAsync("felix");

                    //fill in email
                    await Page.Locator("input[type=email]").FillAsync("Css@live.se");

                    //fill in password
                    await Page.Locator("input[type=password]").FillAsync("qwe123");

                    //fill in confirm password
                    await Page.Locator("input[type=password]").FillAsync("qwe123");

                    //submit registration
                    await Page.ClickAsync("button[type=submit]");

                    await Page.GotoAsync("https://localhost:7280/");

                    var urlAfterClick = "";
                    Assert.That(urlAfterClick, Is.EqualTo("https://localhost:7280/"));


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