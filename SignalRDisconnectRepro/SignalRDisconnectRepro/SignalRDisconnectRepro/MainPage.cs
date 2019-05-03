using System;
using System.ComponentModel;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xamarin.Forms;

namespace SignalRDisconnectRepro
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {

        private HubConnection Connection;

        public MainPage()
        {
            Connection = new HubConnectionBuilder()
                .WithUrl("https://s1.blauhaustechnology.com:5201")
                .ConfigureLogging(x => x.SetMinimumLevel(LogLevel.Debug))
                .AddMessagePackProtocol()
                .Build();

            var connect = new Button
            {
                Text = "CONNECT"
            };
            connect.Clicked += async (sender, e) => 
            {
                await Connection.StartAsync();
            };

            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Children =
                {
                    connect
                }
            };
        }

        

    }
}
