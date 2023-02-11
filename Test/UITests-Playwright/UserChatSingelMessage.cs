
using Microsoft.Playwright;


namespace Test.UITests_Playwright
{
    public class UserChatSingelMessage
    {
        [Test]
        public async Task TestSendingEncryptSingelChatMessage()
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

                //navigate to create singel chat
                await Page.GotoAsync("https://localhost:7280/New_chat");

                //fill in userName
                var userName = "";

                await Page.FillAsync("#singelChatMember", userName);

                //fill in chatName
                var chatName = "";
                await Page.FillAsync("input[type=text]", chatName);

                //fill i i want the chat to be encrypted
                await Page.IsCheckedAsync("#agree");


                //click start chat
                await Page.ClickAsync("button[text=Start Chat]");

                await Page.GotoAsync("https://localhost:7280/New_chat?txt=patrik&txt=Patte&isEncrypted=on");




                string urlAfterClick = Page.Url;
                Assert.Multiple(() =>
                {
                    Assert.That(userName, Is.EqualTo("Patrik"));
                    Assert.That(chatName, Is.EqualTo("Patte")); 
                    Assert.That(Page.IsCheckedAsync("#agree"), Is.True);
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
