using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestfulApiVisualCode.Controllers;
using RestfulApiVisualCode.DataBaseContext;
using RestfulApiVisualCode.Models;
using RestfulApiVisualCode.Security;

namespace RestfulApiVisualCode.Tests;

public class ControllerBehaviorTests
{
    [Fact]
    public async Task Events_GetById_WhenMissing_ReturnsNotFound()
    {
        await using var context = CreateContext();
        var controller = new EventsController(context);

        var result = await controller.Get(9999);

        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task Events_Post_WhenDateInFuture_ReturnsBadRequest()
    {
        await using var context = CreateContext();
        var controller = new EventsController(context);
        var newEvent = new Event
        {
            Dateofevent = DateTime.UtcNow.AddDays(1).ToString("yyyy-MM-dd"),
            Nameofasb = 1,
            Nameofdevice = "Device",
            Isserios = "Обычное происшествие",
            Discribeevent = "Description",
            Fixevent = "Fix",
            EventCreator = "tester"
        };

        var result = await controller.Post(newEvent);

        Assert.IsType<BadRequestObjectResult>(result.Result);
    }

    [Fact]
    public async Task Events_Delete_WhenMissing_ReturnsNotFound()
    {
        await using var context = CreateContext();
        var controller = new EventsController(context);

        var result = await controller.Delete(5000);

        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task Users_Login_WithInvalidCredentials_ReturnsBadRequest()
    {
        await using var context = CreateContext();
        var controller = new UsersController(context);

        var result = await controller.Login(new LoginRequest
        {
            Login = "missing",
            Password = "missing"
        });

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Pages_GetOne_WithoutSubheader_ReturnsBadRequest()
    {
        await using var context = CreateContext();
        var controller = new PagesController(context);

        var result = await controller.GetOne(string.Empty);

        Assert.IsType<BadRequestObjectResult>(result.Result);
    }

    [Fact]
    public async Task Images_Create_WhenEventMissing_ReturnsBadRequest()
    {
        await using var context = CreateContext();
        var controller = new ImagesController(context);
        IFormFileCollection files = new FormFileCollection
        {
            CreateFile("image1.png", "fake-image-content")
        };

        var result = await controller.ImageCreate(files, eventId: 12345);

        Assert.IsType<BadRequestObjectResult>(result.Result);
    }

    [Fact]
    public void PasswordHasher_HashAndVerify_WorksForValidPassword()
    {
        var password = "StrongPassword!123";
        var hash = PasswordHasher.Hash(password);

        Assert.True(PasswordHasher.Verify(password, hash));
    }

    [Fact]
    public void PasswordHasher_Verify_ReturnsFalseForInvalidPassword()
    {
        var hash = PasswordHasher.Hash("CorrectPassword");

        Assert.False(PasswordHasher.Verify("WrongPassword", hash));
    }

    private static EventsContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<EventsContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N"))
            .Options;
        var context = new EventsContext(options);
        context.Database.EnsureCreated();
        return context;
    }

    private static IFormFile CreateFile(string fileName, string content)
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes(content);
        var stream = new MemoryStream(bytes);
        return new FormFile(stream, 0, bytes.Length, "imageFiles", fileName);
    }
}
