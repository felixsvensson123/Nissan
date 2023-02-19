using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace Test.UITests_Playwright
{
    public class SingelChatEncrypted
    {
        [Test]
        public async Task TestSendingSingelChatMessageNotEncrypted()
        {

            try
            {
                //Playwright
                using var playwright = await Playwright.CreateAsync();

                //Browser
                await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
                {
                    Headless = false // set headless mode to false

                });

                //Page
                var Page = await browser.NewPageAsync();


                //Navigate to startpage
                await Page.GotoAsync("https://enchat.azurewebsites.net/");

                var userName = "Patrik";
                //fill in username
                await Page.WaitForSelectorAsync("input[type=Username]");
                await Page.FillAsync("input[type=Username]", userName);

                var password = "patrik123";
                //fill in password
                await Page.WaitForSelectorAsync("input[type=password]");
                await Page.FillAsync("input[type=password]", password);

                //submit 
                await Page.WaitForSelectorAsync("button[type=submit]");
                await Page.ClickAsync("button[type=submit]");

                //navigate to start page
                var pageSelector = "h3:has-text('Start Page')";
                await Page.WaitForSelectorAsync(pageSelector);
                // var startPageElement = await Page.QuerySelectorAsync(pageSelector);

                //click create chat
                var linkSelector = "a.nav-link:has-text('Create Chat')";
                await Page.WaitForSelectorAsync(linkSelector);
                await Page.ClickAsync(linkSelector);

                //navigate to start singel chat page
                var pageCreateSingelChat = "label:has-text('Singel Chat')";
                await Page.WaitForSelectorAsync(pageCreateSingelChat);
                // var startSingelchatElement = await Page.QuerySelectorAsync(pageCreateSingelChat);

                //fill in userName
                var userName1 = "hanna";
                await Page.WaitForSelectorAsync("#singelChatMember");
                await Page.FillAsync("#singelChatMember", userName1);

                //fill in chatName
                var chatName = "Chat with hanna";
                await Page.WaitForSelectorAsync("input[type=text]");
                await Page.FillAsync("input[type=text]", chatName);



                // Hitta elementet med value = false attributet
                await Page.WaitForSelectorAsync("input[type='radio'][value='true']");
                var radioButton = await Page.QuerySelectorAsync("input[type='radio'][value='true']");

                // Klicka på radioknappen
                await radioButton.ClickAsync();

                //click start chat
                var buttonSelector = "label:has-text('Start Chat')";
                await Page.WaitForSelectorAsync(buttonSelector);
                await Page.ClickAsync(buttonSelector);

                //navigate to conversationpage
                var pageConversationSelector = "p:has-text('Welcome')";
                await Page.WaitForSelectorAsync(pageConversationSelector);
                //var pageConversationElement = await Page.QuerySelectorAsync(pageConversationSelector);







                //string urlAfterClick = Page.Url;
                Assert.Multiple(() =>
                {
                    Assert.That(userName1, Is.EqualTo("hanna"));
                    Assert.That(chatName, Is.EqualTo("Chat with hanna"));
                    Assert.That(Page.IsCheckedAsync("#input"), Is.True);
                    Assert.That(pageConversationSelector, Is.Not.Null);
                    Assert.That(pageCreateSingelChat, Is.Not.Null);
                    Assert.That(pageSelector, Is.Not.Null);
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
