﻿using System.Windows.Input;
using DependencyPropertyGenerator;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace GalgameManager.Views.Control;

[DependencyProperty<string>("Title")]
[DependencyProperty<ICommand>("TitleClickCommand")]
[DependencyProperty<object>("ItemSource")]
[DependencyProperty<DataTemplate>("ItemTemplate")]
[DependencyProperty<double>("Spacing", DefaultValue = 8.0f, DefaultBindingMode = DefaultBindingMode.OneWay)]
public partial class MultiFlipView
{
    public MultiFlipView()
    {
        InitializeComponent();
    }

    private void scroller_ViewChanging(object sender, ScrollViewerViewChangingEventArgs e)
    {
        if (e.FinalView.HorizontalOffset < 1) ScrollBackBtn.IsEnabled = false;
        else if (e.FinalView.HorizontalOffset > 1) ScrollBackBtn.IsEnabled = true;

        if (e.FinalView.HorizontalOffset > Scroller.ScrollableWidth - 1) ScrollForwardBtn.IsEnabled = false;
        else if (e.FinalView.HorizontalOffset < Scroller.ScrollableWidth - 1) ScrollForwardBtn.IsEnabled = true;
    }

    private void ScrollBackBtn_Click(object sender, RoutedEventArgs e)
    {
        Scroller.ChangeView(Scroller.HorizontalOffset - Scroller.ViewportWidth, null, null);
        // Manually focus to ScrollForwardBtn since this button disappears after scrolling to the end.          
        ScrollForwardBtn.Focus(FocusState.Programmatic);
    }

    private void ScrollForwardBtn_Click(object sender, RoutedEventArgs e)
    {
        Scroller.ChangeView(Scroller.HorizontalOffset + Scroller.ViewportWidth, null, null);

        // Manually focus to ScrollBackBtn since this button disappears after scrolling to the end.    
        ScrollBackBtn.Focus(FocusState.Programmatic);
    }

    private void scroller_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        UpdateScrollButtonsVisibility();
    }

    private void UpdateScrollButtonsVisibility()
    {
        if (Scroller.ScrollableWidth > 0)
        {
            ScrollForwardBtn.Visibility = Visibility.Visible;
        }
        else
        {
            ScrollForwardBtn.Visibility = Visibility.Collapsed;
        }
    }
}