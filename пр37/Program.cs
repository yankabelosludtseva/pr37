using пр37.Data.Interfaces;
using пр37.Data.Mocks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IItems, MockItems>();
builder.Services.AddTransient<ICategories, MockCategories>();

builder.Services.AddMvc(option => option.EnableEndpointRouting = false);

var app = builder.Build();

app.UseDeveloperExceptionPage();
app.UseStatusCodePages();
app.UseStaticFiles();
app.UseMvcWithDefaultRoute();

app.Run();