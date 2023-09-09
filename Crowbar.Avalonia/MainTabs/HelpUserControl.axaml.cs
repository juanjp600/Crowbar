using System;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using BackwardsCompatibility;

namespace Crowbar.Avalonia.MainTabs;

public sealed partial class HelpUserControl : UserControl
{
    public HelpUserControl()
    {
        InitializeComponent();
    }

    public void LearnToUseCrowbarButtonClicked(object source, RoutedEventArgs args)
    {
        WebUtil.OpenUrl("https://steamcommunity.com/sharedfiles/filedetails/?id=791755353");
    }
}