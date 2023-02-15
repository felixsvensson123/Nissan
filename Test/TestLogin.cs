using Microsoft.Playwright;

namespace Test;

//test expected behaviour of the UI displayed by login 
public class TestLoginUI{
    [Test]
    public async Task TestNotExistingUserTriesToLogin(){
        //Playwright 
        using var playwright = await Playwright.CreateAsync();

        //Browser
        await using var browser = await playwright.Chromium.LaunchAsync();

        //Page
        var page = await browser.NewPageAsync();

        //stuff thats tested
        string urlAfterClick = "";

        try{
            await page.GotoAsync("https://nissanchat.azurewebsites.net/");

            await page.FillAsync("input[type=text]", "anna");

            await page.FillAsync("input[type=password]", "aaaaaa");


            //submit
            await page.ClickAsync("button[type=submit]");

            //get what url is displayed after submit is clicked.
            urlAfterClick = page.Url;
            Console.WriteLine("#### urlAfterClick = " + urlAfterClick);
        }
        catch (Exception e){
            Console.WriteLine("Exception caught!! Message: ");

            Console.WriteLine(e.Message);

            Console.WriteLine("StackTrace: " + e.StackTrace);
        }


        // Validate the navigation stays on the same page, url == url
        Assert.That(urlAfterClick, Is.EqualTo("https://nissanchat.azurewebsites.net/"));
    }

    
    

    [Test]
    public async Task TestUserLogin(){
        //Playwright 
        using var playwright = await Playwright.CreateAsync();

        //Browser
        await using var browser = await playwright.Chromium.LaunchAsync();

        //Page
        var page = await browser.NewPageAsync();

        //stuff thats tested
        var successfulLoginValue = "true";

        try{
            await page.GotoAsync("https://nissanchat.azurewebsites.net");
            
            //står i html:en att type är "Username".
            await page.FillAsync("input[type=text]", "anna");

            await page.FillAsync("input[type=password]", "aaaaaa");

            //submit
            await page.ClickAsync("button[type=submit]");
        }
        catch (Exception e){
            Console.WriteLine("Exception caught in TestUserLogin!! Message: ");

            Console.WriteLine(e.Message);

            Console.WriteLine("StackTrace: " + e.StackTrace);
        }

        //To retrieve the value of a key from local storage, we use the localStorage.getItem method in JavaScript, which is invoked from C# using the EvaluateAsync method in Playwright.

        /*The EvaluateAsync method execute the JavaScript code localStorage.getItem('isauthenticated') in the context of the page, which retrieves the value associated with the "isauthenticated" key from browser's local storage. The await keyword is used to wait for the completion of the EvaluateAsync method call and retrieve the resulting string value.*/

        //get value in browser's local storage API
        /* 
        var actualValue = await page.EvaluateAsync("localStorage.getItem('isauthenticated')"); 
        var actualValue = await page.EvaluateAsync("window.localStorage.getItem('isauthenticated');");
        /*
        var isAuthenticatedSet = await page.EvaluateAsync<bool>("localStorage.getItem('isauthenticated') !== null");
        Assert.IsTrue(isAuthenticatedSet, "!!! The 'isauthenticated' item is not set in localStorage");
        */

        string actualValue = await page.EvaluateAsync<string>("localStorage.getItem('isauthenticated')");

        Console.WriteLine("### key = isauthenticated, value = " + actualValue);

        //check the two strings are the same
        Assert.That(actualValue, Is.EqualTo(successfulLoginValue));
        
        /* Assert.AreEqual(successfulLoginValue, actualValue); */
    }
}