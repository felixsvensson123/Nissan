using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace Test.UITests_Playwright
{
    public class SingelChatNotEncrypt
    {
        [Test]
        public async Task TestSendingSingelChatMessageNotEncrypted()
        {

            try
            {
                //Playwright
                using var playwright = await Playwright.CreateAsync();

                //Browser
                await using var browser = await playwright.Chromium.LaunchAsync();

                //Page
                var Page = await browser.NewPageAsync();


                //Navigate to startpage
                await Page.GotoAsync("https://localhost:7280/StartPage");

                //click creat chat button

                await Page.ClickAsync("link[text=Create Chat]");

                //navigate to create singel chat
                await Page.GotoAsync("https://localhost:7280/New_chat");

                //fill in userName
                var userName = "hanna";

                await Page.FillAsync("#singelChatMember", userName);

                //fill in chatName
                var chatName = "Chat with hanna";
                await Page.FillAsync("input[type=text]", chatName);

              

                // Hitta elementet med value = false attributet
                var radioButton = await Page.QuerySelectorAsync("input[type='radio'][value='false']");

                // Klicka på radioknappen
                await radioButton.ClickAsync();





                //click start chat
                await Page.ClickAsync("button[text=Start Chat]");

                await Page.GotoAsync("https://localhost:7280/conversations");




                string urlAfterClick = Page.Url;
                Assert.Multiple(() =>
                {
                    Assert.That(userName, Is.EqualTo("hanna"));
                    Assert.That(chatName, Is.EqualTo("Chat with hanna"));
                    Assert.That(Page.IsCheckedAsync("#isEncrypted"), Is.False);
                    Assert.That(urlAfterClick, Is.EqualTo("https://localhost:7280/conversations"));
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
