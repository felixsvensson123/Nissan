using Microsoft.Playwright;

namespace Test;

//test expected behaviour of the UI displayed by login 
public class TestUserLogInUI{
    
    
    [Test]
    public async Task TestNotExistingLogin(){
        //Playwright 
        using var playwright = await Playwright.CreateAsync();

        //Browser
        await using var browser = await playwright.Chromium.LaunchAsync();

        //Page
        var page = await browser.NewPageAsync();

        //stuff thats tested
        string urlAfterClick = "";

        try{
            await page.GotoAsync("https://localhost:7280/");

            await page.FillAsync("input[type=text]", "anna");

            await page.FillAsync("input[type=password]", "aaaaaa");

            /*//click checkbox remember me. IS COMMENTED OUT BZ NOT DISPLAYED ANYMORE (it was nice feature for user, did it work? did it not? why is it not here?) 
           await page.ClickAsync("input[type=checkbox]");*/

            //submit
            await page.ClickAsync("button[type=submit]");

            //get what url is displayed after submit is clicked.
            urlAfterClick = page.Url;
            Console.WriteLine("urlAfterClick = " + urlAfterClick);
        }
        catch (Exception e){
            Console.WriteLine("Exception caught!! Message: ");

            Console.WriteLine(e.Message);

            Console.WriteLine("StackTrace: " + e.StackTrace);
        }

        //BELOW CAN'T BE DONE SINCE IS NOT SAVED, BUT IS NOT NEEDED SINCE WE ARE NOT LOGGED IN ANYWAY.
        /*// Validate the login is NOT successful by checking that the local storage item "isauthenticated" has a value of "false"
        var localStorageValue =
            await page.EvaluateAsync<string>(@"async () => window.localStorage.getItem('isauthenticated')");
        Assert.That(localStorageValue, Is.EqualTo("false"));
        Assert.AreEqual("false", localStorageValue);*/

        // Validate the navigation stays on the same page, url == url
        Assert.That(urlAfterClick, Is.EqualTo("https://localhost:7280/"));
        /* Assert.AreEqual("https://localhost:7280/", urlAfterClick); */
    }


    [Test]
    public async Task TestAnExistingLogin(){
        //Playwright 
        using var playwright = await Playwright.CreateAsync();

        //Browser
        await using var browser = await playwright.Chromium.LaunchAsync();

        //Page
        var page = await browser.NewPageAsync();

        //stuff thats tested
        string urlAfterClick = "";

        try{

            await page.GotoAsync("https://localhost:7280/");

            await page.FillAsync("input[type=text]", "felix");

            await page.FillAsync("input[type=password]", "qwe123");

            /*//click checkbox remember me. IS COMMENTED OUT BZ NOT DISPLAYED ANYMORE (it was nice feature for user, did it work? did it not? why is it not here?) 
          await page.ClickAsync("input[type=checkbox]");*/

            //submit
            await page.ClickAsync("button[type=submit]");

           
        }
        catch (Exception e){
            Console.WriteLine("Exception caught!! Message: ");

            Console.WriteLine(e.Message);

            Console.WriteLine("StackTrace: " + e.StackTrace);
        }

        //get what url is displayed after submit is clicked.
        urlAfterClick = page.Url;
        /* Console.WriteLine("urlAfterClick = " + urlAfterClick); */

        // Validate the navigation stays the same
        Assert.That(urlAfterClick, Is.EqualTo("https://localhost:7280/"));


        //THE TEST ITSELF DOES NOT WORK YET
        //validate the page title (the HTML element title).
        var title = await page.TitleAsync(); //visar N_chat
        Console.WriteLine("title = " + title);
       Assert.That(title, Does.Contain("About Nchat"));

        
        
         //THE TEST ITSELF DOES NOT WORK YET
        // Validate that after successful login the page our user sees contains the h3 "StartPage/ About Nchat"
         var h3 = await page.WaitForSelectorAsync("h3");   
         Console.WriteLine("h3 = " + h3);
        var text = await page.EvaluateAsync<string>("h3");
        Console.WriteLine("text = " + text);
        Assert.That(text, Is.EqualTo("StartPage/ About Nchat"));
        /*  Assert.AreEqual(text, "StartPage/ About Nchat"); */


        
        //THE TEST ITSELF DOES NOT WORK YET
        // Validate the login is successful by checking the Key and Value
        var localStorageKey =
            await page.EvaluateAsync<string>(@"async () => window.localStorage.key(0)");
        Console.WriteLine("localStorageKey = " + localStorageKey);
        Assert.That(localStorageKey, Is.EqualTo("isauthenticated"));

        

        //THE TEST ITSELF DOES NOT WORK YET
        // Validate the login is successful by checking that the local storage item "isauthenticated" has a value of "true"
        var localStorageValue =
            await page.EvaluateAsync<string>(@"async () => window.localStorage.getItem('isauthenticated')");
        Console.WriteLine("localStorageValue = " + localStorageValue);
        Assert.That(localStorageValue, Is.EqualTo("true"));
    }
        
        
}