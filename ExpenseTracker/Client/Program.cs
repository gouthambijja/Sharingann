using Blazored.LocalStorage;
using ExpenseTracker.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Authorization;
using BlazorApp2.Client;
using BlazorApp2.Client.handlers;
using ExpenseTracker.Client.ViewModels;
using Blazored.Toast;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddBlazoredLocalStorage();


builder.Services.AddHttpClient<ILoginViewModel, LoginViewModel>
               ("FetchDataViewModelClient", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
               .AddHttpMessageHandler<CustomAuthorizationHandler>();
builder.Services.AddHttpClient<ICounter, Counter>
               ("FetchDataViewModelClient", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
               .AddHttpMessageHandler<CustomAuthorizationHandler>();
builder.Services.AddHttpClient<IRegisterViewModel, RegisterViewModel>
               ("FetchDataViewModelClient", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
               .AddHttpMessageHandler<CustomAuthorizationHandler>();
builder.Services.AddHttpClient<IHomeViewModel, HomeViewModel>
               ("FetchDataViewModelClient", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
               .AddHttpMessageHandler<CustomAuthorizationHandler>();



object value = builder.Services.AddBlazoredToast();
builder.Services.AddTransient<CustomAuthorizationHandler>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddAuthorizationCore();
builder.Services.AddSingleton<StateContainerService>();
await builder.Build().RunAsync();
