using Microsoft.EntityFrameworkCore;
using N_Chat.Server.Controllers;
using N_Chat.Server.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using N_Chat.Shared;
using N_Chat.Client.Services;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;

namespace Test
{
    //public class AdminPgaeTester
    //{
    //    // Test expectet behavior of method of method get all usersn in Class UserController.cs


    //    private DataContext _context;
    //    private IUserService userService;
    //    private new List<UserModel> UserList;

    //    [SetUp]
    //    public void Setup()
    //    {
    //        var Options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase(databaseName: "UsercontrollerTest").Options;
    //        _context = new DataContext(Options);
    //       // _userController = new UserController(_context);
    //    }


    //    [TearDown]
    //    public void TearDown()
    //    {
    //        _context.Dispose();
    //    }

    //    [Test]
    //    public async Task Test_GetAllUsers()
    //    {
    //        // making an instance of class Usercontroller

    //        if (UserList != null)
    //        {
    //            UserList = await userService.GetAllUsers();
    //            Assert.That(UserList, Is.Not.Empty);
    //        }
    //        else Console.WriteLine("User is not found");


    //    }











    //}
}
