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
                    var userName = "";
                    await Page.Locator("input[type=text]").FillAsync(userName);

                    //fill in email
                    var email = "";
                    await Page.Locator("input[type=email]").FillAsync(email);

                    //fill in password
                    var password = "";
                    await Page.Locator("input[type=password]").FillAsync(password);

                    //fill in confirm password
                    var confirmPassword = "";
                    await Page.Locator("input[type=password]").FillAsync(confirmPassword);

                    //submit registration
                    await Page.ClickAsync("button[type=submit]");

                    await Page.GotoAsync("https://localhost:7280/");

                    var urlAfterClick = "";
                    Assert.That(urlAfterClick, Is.EqualTo("https://localhost:7280/"));

                    Assert.Multiple(() =>
                    {
                        Assert.That(userName, Is.EqualTo("Patrik"));
                        Assert.That(email, Is.EqualTo("patrik@yahoo.com"));
                        Assert.That(password, Is.EqualTo("patrik123"));
                        Assert.That(confirmPassword, Is.EqualTo("patrik123"));
                        Assert.That(urlAfterClick, Is.EqualTo("https://localhost:7280/New_chat?txt=patrik&txt=Patte&isEncrypted=on"));
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