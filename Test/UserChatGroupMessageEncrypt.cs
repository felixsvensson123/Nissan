using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace Test
{
    public class UserChatGroupMessageEncrypt
    {
        [Test]
        public async Task TestSendingEncryptGroupChatMessage()
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
                await Page.GotoAsync("https://localhost:7280/startPage");

                //click creat chat button

                await Page.ClickAsync("link[text=Create Chat]");

                //navigate to create group chat
                await Page.GotoAsync("https://localhost:7280/New_chat");

                //locate to label Group and click
                await Page.Locator("label=Group Chat").ClickAsync();


                //fill in userName1
                var userName1 = "";
                await Page.Locator("input[id=singelChatMember1]").FillAsync(userName1);

                /*await Page.FillAsync("#singelChatMember1", userName1);*/

                //fill in userName2
                var userName2 = "";
               /* await Page.FillAsync("#singelChatMember2", userName2);*/
                await Page.Locator("input[id=singelChatMember2]").FillAsync(userName2);

                //fill i i want the chat to be encrypted
                await Page.IsCheckedAsync("#input");


                //click start groupchat
                await Page.ClickAsync("button[text=Start Group Chat]");

                await Page.GotoAsync("https://localhost:7280/New_chat?txt=calin&txt=felix&isEncrypted=on");




                string urlAfterClick = Page.Url;
                Assert.Multiple(() =>
                {
                    Assert.That(userName1, Is.EqualTo("Calin"));
                    Assert.That(userName2, Is.EqualTo("Felix")); ;
                    Assert.That(Page.IsCheckedAsync("#input"), Is.True);
                    Assert.That(urlAfterClick, Is.EqualTo("https://localhost:7280/New_chat?txt=calin&txt=felix&isEncrypted=on"));
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
